import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef, Directive, Input, Sanitizer, SimpleChanges, SecurityContext, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, LichSuLamViecDto, LichSuLamViecsServiceProxy, SessionServiceProxy, UngVienDto, TemplatesServiceProxy, TemplateDto, HostSettingsEditDto, ComboboxItemDto, SettingScopes, SendTestEmailInput, HostSettingsServiceProxy, ConfigEmailsServiceProxy, ConfigEmailDto, GetConfigEmailForViewDto, CreateOrEditLichSuLamViecDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, HoSosServiceProxy, CreateOrEditUngVienInput } from '@shared/service-proxies/service-proxies'; import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { formatDate } from '@angular/common';
import { DxHtmlEditorModule, DxCheckBoxModule } from 'devextreme-angular';
import * as $ from 'jquery';
import 'jquery-ui/ui/widgets/dialog.js';
import { CKEditorModule } from 'ng2-ckeditor';


import { ToolbarModule } from 'primeng/primeng';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { AppSessionService } from '@shared/common/session/app-session.service';

@Component({
    selector: 'viewTuongTacUngVienModal',
    // encapsulation: ViewEncapsulation.None,
    templateUrl: './view-tuongTacUngVien-modal.component.html',
    styleUrls: ['./view-tuongTacUngVien-modal.component.less']
})
export class ViewTuongTacUngVienModalComponent extends AppComponentBase implements OnInit {

    // @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    maxLength = null;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    listComment: LichSuLamViecDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    chuDeGsoft: TemplateDto[] = [];
    chuDeGoB: TemplateDto[] = [];

    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    congty: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    tinhThanh: TinhThanhDto[] = [];
    noiDaoTao: NoiDaoTaoDto[] = [];

    ungVien: CreateOrEditUngVienDto = new CreateOrEditUngVienDto();
    ngaySinh: Date;
    ngayCap: Date;
    approvE_DT: Date;
    ungVienId: number;
    data: any;
    num: string;
    rootUrl = AppConsts.remoteServiceBaseUrl;
    link: string;
    selectedRows = [];
    userId: number;
    currentDate = new Date();
    nameArr: any[] = [];
    filedinhkem: any;
    loading = false;
    hostSettings: HostSettingsEditDto;
    editions: ComboboxItemDto[] = undefined;
    testEmailAddress: string = undefined;
    showTimezoneSelection = abp.clock.provider.supportsMultipleTimezone;
    defaultTimezoneScope: SettingScopes = SettingScopes.Application;
    i: number;
    currentTime: any;
    tenChuDe: any;
    uploadUrl: string;
    value: any[] = []; comment = '';
    currentUserId = this.getCurrentUser().id;
    currentUserFullName = this.getFullNameCurrentUser();
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    tepDinhKemSave = '';
    isMultiline: boolean = true;
    dataDisplay = [];
    valueContent: string;
    editorValueType: string;
    mailTo: string;
    mailFrom: string;
    subject: string;
    body: string;
    fileDinhkem: string;
    editorConfig = {
        filebrowserBrowseUrl: '/assets/fileman/index.html?integration=tinymce4',
        filebrowserImageBrowseUrl: '/assets/fileman/index.html?integration=ckeditor&type=image',
        filebrowserUploadUrl: '/assets/fileman/core/connector/php/connector.php?command=QuickUpload&type=Files',
        removeDialogTabs: 'link:upload;image:upload'
    };

    onValueTypeChanged({ addedItems }) {
        this.editorValueType = addedItems[0].text.toLowerCase();
    }

    valueChange(value) {
        this.valueContent = value;
    }
    chude: any;
    ungVienForCheckCMND: UngVienDto[] = [];
    chude_: TemplateDto[] = [];
    listYear: any;
    years: any;
    email: any;
    static $this: any;
    // noiDungCD = 'Loading...'; 
    noiDungCD: any;
    tenFile: any;
    mail_From: any;
    config_Emnail: any;
    configEmnail: ConfigEmailDto;
    tenTruyCap: any;
    matKhau: any;
    tenCTY: any;
    congSMTP: any;
    diaChiIP: any;
    selected_Rows: any;
    id: any;
    lichsu: any;
    tepDinhKem_Save: any;
    popupVisible = false;
    totalUnitCount = 0;
    danhSachCV: any;
    idDV: any;
    treeData: any;
    nam: any;
    createOrEditUngVienInput: CreateOrEditUngVienInput = new CreateOrEditUngVienInput();
    danhSachDV: any;
    viewIsLoading = true;

    constructor(
        injector: Injector,

        private elementRef: ElementRef,
        private sanitizer: Sanitizer,
        private _hostSettingService: HostSettingsServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _configEmailsServiceProxy: ConfigEmailsServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _templatesServiceProxy: TemplatesServiceProxy,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _lichSuLamViecsServiceProxy: LichSuLamViecsServiceProxy
    ) {
        super(injector);

        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.appSession = injector.get(AppSessionService);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
        // (window.parent as any).closeCustomRoxy = this.closeCustomRoxy;
        // (window.parent as any).onSelectCustomRoxy = this.onSelectCustomRoxy;
        // EditUngVienComponent.$this = this;
    }

 
    pheDuyet() {

        this.saving = true;
        //console.log(this.appSession.userId)
        this.ungVien.maNguoiPheDuyet = this.appSession.userId
        this.ungVien.ngayPheDuyet = moment(this.currentDate).utc(true);
        //console.log(this.ungVien.ngayPheDuyet)
        this.ungVien.trangThaiCode = 'DD';
        this.createOrEditUngVienInput.ungVien = this.ungVien;
        this._ungViensServiceProxy.createOrEdit(this.createOrEditUngVienInput)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.show();
            });

    }
    troVe() {
        this.router.navigate(['/app/main/qlns/tuongtacungvien']);
    }
    //  ghep chuoi ten file 
    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        this.value.forEach((ele) => {
            this.dataDisplay.push({ tepDinhKem: cValue + "/" + this.currentTime + "/" + ele.name });
        });

        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);
        this.tepDinhKemSave = this.selectedRows.map(x => x.tepDinhKem.toString()).join(';')
    }

    changeCTY(e) {
        this.danhSachDV = this.treeData.filter(x => x.label == e.value)[0].children;
    }


    private getTreeDataFromServer(): void {
        let self = this;
        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;
            this.treeData = this._arrayToTreeConverterService.createTree(result.items,
                'parentId',
                'id',
                null,
                'children',
                [
                    {
                        target: 'label',
                        targetFunction(item) {
                            return item.displayName;
                        }
                    }, {
                        target: 'expandedIcon',
                        value: 'fa fa-folder-open m--font-warning'
                    },
                    {
                        target: 'collapsedIcon',
                        value: 'fa fa-building-o m--font-warning'
                    },
                    {
                        target: 'selectable',
                        value: true
                    },
                    {
                        target: 'memberCount',
                        targetFunction(item) {
                            return item.memberCount;
                        }
                    },
                    {
                        target: 'roleCount',
                        targetFunction(item) {
                            return item.roleCount;
                        }
                    }
                ]);
                self.show();
        });
    }
    selectItem(e) {
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {
            this.danhSachCV = res;
        })
    }

    uploadTDK(e: any) {
        this.filedinhkem = e.file.name;
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        this.tenFile = this.filedinhkem;
        this.tepDinhKem_Save = cValue + "/" + this.currentTime + "/" + e.file.name;
    }



    linkDownloadFile(tenfle: string) {
        return this.rootUrl + "/" + tenfle;
    }

    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.ngaySinh = null;
        this.ngayCap = null;
        this.approvE_DT = null;
        if (Id != null) {
            this.ungVienId = Number.parseInt(Id);
            this._ungViensServiceProxy.getUngVienForUpdate(this.ungVienId).subscribe(result => {
                this.ungVien = result.ungVien;
                this.email = result.ungVien.email;

                this.danhSachCV = result.congViecList;
                this.chude = result.templateList;
                this.tinhThanh = result.tinhThanhList;
                this.noiDaoTao = result.noiDaoTaoList;
                this.lichsu = result.lichSuLamViecList;
                this.viTriCongViec = result.viTriCongViec;
                this.tienDoTuyenDung = result.tienDoTuyenDung;
                this.trangThai = result.trangThai;
                this.kenhTuyenDung = result.kenhTuyenDung;
                this.congty = result.congty;
                this.gioiTinh = result.gioiTinh;
                this.tinhTrangHonNhan = result.tinhTrangHonNhan;
                this.trinhDoDaoTao = result.trinhDoDaoTao;
                this.xepLoaiHocLuc = result.xepLoaiHocLuc;

                if(result.configEmail && result.configEmail.configEmail){
                    this.mail_From = result.configEmail.configEmail.diaChiEmail;
                    this.tenTruyCap = result.configEmail.configEmail.tenTruyCap;
                    this.matKhau = result.configEmail.configEmail.matKhau;
                    this.congSMTP = result.configEmail.configEmail.congSMTP;
                    this.diaChiIP = result.configEmail.configEmail.diaChiIP;
                }
                if (this.ungVien.tepDinhKem && this.ungVien.tepDinhKem.length > 0) {

                    this.num = this.ungVien.tepDinhKem;
                    this.num.split(';').forEach(element => {
                        this.selectedRows.push({ tepDinhKem: element });

                    });
                }
                if(result.organizationUnitList && result.organizationUnitList.items){
                    var treeData = this._arrayToTreeConverterService.createTree(result.organizationUnitList.items,
                        'parentId',
                        'id',
                        null,
                        'children',
                        [
                            {
                                target: 'label',
                                targetFunction(item) {
                                    return item.displayName;
                                }
                            }, {
                                target: 'expandedIcon',
                                value: 'fa fa-folder-open m--font-warning'
                            },
                            {
                                target: 'collapsedIcon',
                                value: 'fa fa-building-o m--font-warning'
                            },
                            {
                                target: 'selectable',
                                value: true
                            },
                            {
                                target: 'memberCount',
                                targetFunction(item) {
                                    return item.memberCount;
                                }
                            },
                            {
                                target: 'roleCount',
                                targetFunction(item) {
                                    return item.roleCount;
                                }
                            }
                        ]);
                    this.danhSachDV = treeData.filter(x => x.label == this.ungVien.tenCTY)[0].children;
                }
                this.viewIsLoading = false;
            });
        }
    }

    chonDV() {
        this.popupVisible = true;
    }

    trangChuUngVien() {
        this.viewIsLoading = true;
        setTimeout(() => {
            this._lichSuLamViecsServiceProxy.getLichSuLamViecByUngVien(this.ungVienId).subscribe(res => {
                this.lichsu = res.filter(x => x.chuDe);
                this.viewIsLoading = false;
            });
        }, 0);
    }

    getDaTa(e: any) {

        this._templatesServiceProxy.getTemplateForView(e.value).subscribe(res => {
            this.tenChuDe = res.template.tenTemplate;
            this.noiDungCD = res.template.noiDung;
        })
    }
    xoaFile(e: any) {
        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');
    }


    thayDoiTenCTY(e) {
        if (e.value == 'GSOFT') {
            this._templatesServiceProxy.getAllTemplate().subscribe((res) => {
                this.chuDeGsoft = this.chude_.filter(x => x.maTemplate == 'GSOFT');
                this.chude = this.chuDeGsoft;
            })
            this.id = 1;
        }
        else {
            this._templatesServiceProxy.getAllTemplate().subscribe((res) => {
                this.chuDeGoB = this.chude_.filter(x => x.maTemplate == 'GOBRANDING');
                this.chude = this.chuDeGoB;

            })
            this.id = 2;
        }
    }
    showDetail(e: any) {
        this.link = this.rootUrl + "/" + e.row.data.tepDinhKem;
        window.open(this.link, '_blank');
    }

    ngOnInit() {
        // this.getTreeDataFromServer();
        this.show();
        this.years = function (startYear) {
            var currentYear = new Date().getFullYear(), years = [];
            startYear = startYear || 1980;
            while (startYear <= currentYear) {
                years.push({ tennam: startYear++ });
            }
            return years;
        }
        this.listYear = this.years(2019 - 20);
        // this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
        //     this.totalUnitCount = result.items.length;

        //     this.treeData = this._arrayToTreeConverterService.createTree(result.items,
        //         'parentId',
        //         'id',
        //         null,
        //         'items',
        //         [
        //             {
        //                 target: 'text',
        //                 targetFunction(item) {
        //                     return item.displayName;
        //                 }
        //             }

        //         ]);
        // });
    }

    sendEmail(): void {
        const input = new SendTestEmailInput();
        input.filedinhkem = this.tenFile;
        input.curentTime = this.currentTime;
        input.mailForm = this.mail_From;       
        if( !this.email)
        {
            this.message.warn(" Email không được bỏ trống!");
            return;
        }
        else
        {
            input.emailAddress = this.email || this.ungVien.email;
        }
        if( !this.tenChuDe)
        {
            this.message.warn(" Chủ đề không được bỏ trống!");
            return;
        }
        else
        {
            input.subject = this.tenChuDe;
        }
        if( !this.noiDungCD)
        {
            this.message.warn(" Nội dung gửi mail không được bỏ trống!");
            return;
        }
        else
        {
            input.body = this.noiDungCD;
        }
        input.tenTruyCap = this.tenTruyCap;
        input.diaChiIP = this.diaChiIP;
        input.congSMTP = this.congSMTP;
        input.matKhau = this.matKhau;
        this._ungViensServiceProxy.sendEmailKH(input).subscribe(result => {
            this.notify.info(this.l('Đã gửi mail thành công'));
            const input1 = new CreateOrEditLichSuLamViecDto();
            input1.noiDung = this.noiDungCD;
            input1.tepDinhKem = this.tepDinhKem_Save;
            input1.chuDe = this.tenChuDe;
            input1.ungVienId = this.ungVienId;
            this._lichSuLamViecsServiceProxy.createOrEdit(input1).pipe(finalize(() => { this.saving = false; })).subscribe(res => {
    
            })
            this.trangChuUngVien();
        });
    }
}
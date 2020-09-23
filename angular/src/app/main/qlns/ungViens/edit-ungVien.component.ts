import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef, Directive, Input, Sanitizer, SimpleChanges, SecurityContext } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, LichSuLamViecDto, LichSuLamViecsServiceProxy, SessionServiceProxy, UngVienDto, TemplatesServiceProxy, TemplateDto, HostSettingsEditDto, ComboboxItemDto, SettingScopes, SendTestEmailInput, HostSettingsServiceProxy, ConfigEmailsServiceProxy, ConfigEmailDto, GetConfigEmailForViewDto, CreateOrEditLichSuLamViecDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, HoSosServiceProxy, CreateOrEditHoSoDto, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy, CreateOrEditUngVienInput, LichSuUploadNewDto } from '@shared/service-proxies/service-proxies'; import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { formatDate } from '@angular/common';
import { DxHtmlEditorModule, DxCheckBoxModule, DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as $ from 'jquery';
import 'jquery-ui/ui/widgets/dialog.js';
import { CKEditorModule } from 'ng2-ckeditor';
import * as _ from 'lodash';

import { ToolbarModule } from 'primeng/primeng';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
@Component({
    selector: 'editUngVien',
    styleUrls: ['./edit-ungVien.component.less'],
    templateUrl: './edit-ungVien.component.html'

})
export class EditUngVienComponent extends AppComponentBase implements OnInit {

    // @ViewChild("dataGridUpload") uploadGird: DxDataGridComponent;
    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
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
    createOrEditUngVienInput: CreateOrEditUngVienInput = new CreateOrEditUngVienInput();
    // stripeIsLoading = true;

    ungVien: CreateOrEditUngVienDto = new CreateOrEditUngVienDto();
    ngaySinh: Date;
    ngayCap: Date;
    approvE_DT: Date;
    ungVienId: number;
    data: any;
    num: string;
    rootUrl = AppConsts.remoteServiceBaseUrl;
    link: string;
    selectedRows: any[];
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
    lichSu: any;
    uploadUrlImage: any;
    danhSachDV: any;
    hoSo: CreateOrEditHoSoDto = new CreateOrEditHoSoDto();
    fileDinhKemComment: any;
    fileComment: string;
    dungLuongSave: any;
    lichSu_Upload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    lichSuUpload: LichSuUploadNewDto = new LichSuUploadNewDto();
    lichSuUploadList: any[] = [];
    tieuDeSave: any;
    time1: any;
    time2: any;
    time3: any;

    dayTime2: any;
    dayTime1: any;
    dayTime3: any;

    ngayPV1: any;
    ngayPV2: any;
    ngayPV3: any;
    constructor(
        injector: Injector,

        private elementRef: ElementRef,
        private sanitizer: Sanitizer,
        private _hostSettingService: HostSettingsServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
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
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
        // (window.parent as any).closeCustomRoxy = this.closeCustomRoxy;
        this.uploadUrlImage = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_fileImage';
        // (window.parent as any).onSelectCustomRoxy = this.onSelectCustomRoxy;
        // EditUngVienComponent.$this = this;
    }

    // importFileFromServer(event: any): void {
    //     ($('#roxyCustomPanel') as any).dialog({ modal: true, width: 875, height: 600 });
    // }

    // closeCustomRoxy(): void {
    //     ($('#roxyCustomPanel') as any).dialog('close');
    // }

    // onSelectCustomRoxy(fileSelected: any): void {
    //     EditUngVienComponent.$this.ungVien.noiDung = fileSelected;
    // }
    troVe() {
        this.router.navigate(['/app/main/qlns/ungViens']);
    }
    //  ghep chuoi ten file
    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        //console.log(this.currentTime)
        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                }
            }
        });
        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);
        //console.log(this.selectedRows)
        this.lichSu_Upload.type = "UV";
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("UV", Id).subscribe(res => {

        })
    }



    // changeCTY(e) {
    //     //console.log(e.value)
    //     this.danhSachDV = _.orderBy(this.treeData.filter(x => x.label == e.value)[0].children, ['value'], ['asc']);
    //     //console.log(this.danhSachDV)

    // }


    private getTreeDataFromServer(): void {
        let self = this;
        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;
            //console.log(result)
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

        });
    }


    selectItem(e) {

        //console.log(e)
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {
            this.danhSachCV = res;
            //console.log(this.danhSachCV)

        })
    }


    deleteComment(id: number) {
        this.message.confirm(
            '', this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.listComment = this.listComment.filter(x => x.id !== id);
                    this._lichSuLamViecsServiceProxy.delete(id).subscribe((res) => {
                        //console.log(res);
                    }, (err) => {
                        //console.log(err);
                    }
                    );
                }
            }
        );
    }

    sendComment() {
        if (this.comment.trim().length == 0)
            return;
        else {
            let l = new LichSuLamViecDto();
            l.noiDung = this.comment;
            l.fullName = this.currentUserFullName;
            l.ungVienId = this.ungVienId;
            l.creatorUserId = this.currentUserId;
            l.tepDinhKem = this.fileComment;
            l.creationTime = moment(new Date());
            this.ungVien.lastModificationTime = moment(new Date());
            this._lichSuLamViecsServiceProxy.createAndGetIdComment(l).subscribe(res => {
                l.id = res;
                this.listComment.push(l);
                this.notify.info(this.l("Bình luận thành công"));
                // l = new LichSuLamViecDto();
                this.fileComment = null;

            }, (err) => {
                //console.log(err);
            });
            this.comment = '';
            this.fileDinhKemComment = '';
        }
    }


    // sendEmail(): void {

    //     const input = new SendTestEmailInput();
    //     input.filedinhkem = this.tenFile;
    //     input.curentTime = this.currentTime;
    //     // input.emailAddress = "hungnguyen23031996@gmail.com";
    //     input.mailForm = this.mail_From;
    //     if (!this.email) {
    //         this.message.warn(" Email không được bỏ trống!");
    //         return;
    //     }
    //     else {
    //         input.emailAddress = this.email;
    //     }
    //     //console.log(input.emailAddress)
    //     // input.body = this.noiDungCD;
    //     if (!this.tenChuDe) {
    //         this.message.warn(" Chủ đề không được bỏ trống!");
    //         return;
    //     }
    //     else {
    //         input.subject = this.tenChuDe;
    //     }
    //     if (!this.noiDungCD) {
    //         this.message.warn(" Nội dung gửi mail không được bỏ trống!");
    //         return;
    //     }
    //     else {
    //         input.body = this.noiDungCD;
    //     }


    //     input.tenTruyCap = this.tenTruyCap;
    //     input.diaChiIP = this.diaChiIP;
    //     input.congSMTP = this.congSMTP;
    //     input.matKhau = this.matKhau;
    //     //console.log(input)
    //     this._ungViensServiceProxy.sendEmailKH(input).subscribe(result => {
    //         this.notify.info(this.l('Đã gửi mail thành công'));
    //         const input1 = new CreateOrEditLichSuLamViecDto();
    //         input1.noiDung = this.noiDungCD;
    //         //console.log(input1.noiDung)
    //         input1.tepDinhKem = this.tepDinhKem_Save;
    //         //console.log(input1.tepDinhKem)
    //         input1.chuDe = this.tenChuDe;
    //         //console.log(input1.chuDe)
    //         input1.ungVienId = this.ungVienId;
    //         this._lichSuLamViecsServiceProxy.createOrEdit(input1).pipe(finalize(() => { this.saving = false; })).subscribe(res => {

    //         })
    //     });



    // }

    uploadFileComment() {
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                    this.fileDinhKemComment = ele.name;
                    this.fileComment = cValue + "\\" + ele.name;
                }
            }
        });
        // this.value.forEach((x) => {
        //     this.fileDinhKemComment = x.name;
        //     this.fileComment = cValue + "\\" + x.name;
        // });
        this.value.length = 0;
    }

    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.ngaySinh = null;
        this.ngayCap = null;
        this.approvE_DT = null;
        if (Id != null) {
            this.ungVienId = Number.parseInt(Id);
            this._ungViensServiceProxy.getUngVienForEdit(this.ungVienId).subscribe(result => {
                this.ungVien = result.ungVien;
                console.log(this.ungVien)
                this.email = result.ungVien.email;

                this.dayTime1 = this.ungVien.day1;
                this.dayTime2 = this.ungVien.day2;
                this.dayTime3 = this.ungVien.day3;
                this.ngayPV1 = this.ungVien.day1 + this.ungVien.time1;
                this.ngayPV2 = this.ungVien.day2 + this.ungVien.time2;
                this.ngayPV3 = this.ungVien.day3 + this.ungVien.time3;
                console.log(this.ngayPV1)

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
                this.selectedRows = result.lichSuUploadList;
                this.listComment = result.lichSuLamViecList;

                if (result.organizationUnitList && result.organizationUnitList.items) {
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
            });
        }

    }

    chonDV() {
        this.popupVisible = true;
    }

    trangChuUngVien() {
        window.location.reload();
    }

    getDaTa(e: any) {

        this._templatesServiceProxy.getTemplateForView(e.value).subscribe(res => {
            //console.log(res);
            this.tenChuDe = res.template.tenTemplate;
            this.noiDungCD = res.template.noiDung;

            //console.log(this.noiDungCD)
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
    showDetailTieuDe(e: any) {
        //console.log(e);
    }

    showDetailFile(tenFile: string) {
        //console.log(tenFile)
        this.link = this.rootUrl + "/" + tenFile;
        window.open(this.link, '_blank');

    }
    showDetail(e: any) {
        //console.log(e);
        this.link = this.rootUrl + "/" + e.displayValue;
        window.open(this.link, '_blank');

    }

    ngOnInit() {
        //this.getTreeDataFromServer();
        this.show();

        this.years = function (startYear) {
            var currentYear = new Date().getFullYear(), years = [];
            startYear = startYear || 2000;
            while (startYear <= currentYear) {
                years.push({ tennam: startYear++ });
            }
            return years;
        }
        this.listYear = this.years(2019 - 20);

    }

    save(): void {
        let result = this.documentForm.instance.validate();
        if (result.isValid && this.validateNgayPV()) {
            this.lichSuUploadList.length = 0;

            const Id = this._activatedRoute.snapshot.paramMap.get('id');
            this.createOrEditUngVienInput.ungVien = this.ungVien;
            this.lichSu_Upload.type = "UV"
            this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("UV", Id).subscribe(res => {

            })
            if (this.selectedRows)
                this.selectedRows.forEach(ele => {
                    this.lichSu_Upload.tenFile = ele.tenFile;
                    this.lichSu_Upload.tieuDe = ele.tieuDe;
                    this.lichSu_Upload.dungLuong = ele.dungLuong;
                    this.lichSu_Upload.type = "UV";
                    this.lichSu_Upload.typeID = Id;
                    this.lichSuUploadList.push(this.lichSu_Upload);
                    this._lichSuUploadsServiceProxy.createOrEdit(this.lichSu_Upload).pipe(finalize(() => { this.saving = false; }))
                        .subscribe(() => {

                        });
                });

            this._ungViensServiceProxy.getAllCMND().subscribe((res) => {
                if (this.ungVien.soCMND) {
                    this.ungVienForCheckCMND = res;
                    if (this.ungVienForCheckCMND.findIndex(x => x.soCMND == this.ungVien.soCMND && x.id !== this.ungVienId) > -1) {
                        this.message.warn("Số CMND đã bị trùng!");
                        return;
                    }
                }

                // this.ungVien.tepDinhKem = this.lichSuUpload.type;
                this.saving = true;
                if (!this.ungVien.hoVaTen) {
                    this.message.warn(" Họ và tên không được bỏ trống!");
                    return;
                }
                if (!this.ungVien.ngaySinh) {
                    this.message.warn(" Ngày sinh không được bỏ trống!");
                    return;
                }

                if (!this.ungVien.gioiTinhCode) {
                    this.message.warn(" Giới tính không được bỏ trống!");
                    return;
                }
                if (!this.ungVien.dienThoai) {
                    this.message.warn(" Điện thoại không được bỏ trống!");
                    return;
                }

                if (this.dayTime1) {
                    this.ungVien.day1 = moment(this.dayTime1).utc(true);
                    this.ungVien.time1 = formatDate(this.dayTime1, 'hh-mm a', 'en-US');
                }
                else {
                    this.ungVien.day1 = null;
                    this.ungVien.time1 = "";
                }
                if (this.dayTime2) {
                    this.ungVien.day2 = moment(this.dayTime2).utc(true);
                    this.ungVien.time2 = formatDate(this.dayTime2, 'hh-mm a', 'en-US');
                }
                else {
                    this.ungVien.day2 = null;
                    this.ungVien.time2 = "";
                }
                if (this.dayTime3) {
                    this.ungVien.day3 = moment(this.dayTime3).utc(true);
                    this.ungVien.time3 = formatDate(this.dayTime3, 'hh-mm a', 'en-US');
                }
                else {
                    this.ungVien.day3 = null;
                    this.ungVien.time3 = "";
                }

                if (!this.ungVien.email) {
                    this.message.warn(" Email không được bỏ trống!");
                    return;
                }

                if (!this.ungVien.viTriUngTuyenCode) {
                    this.message.warn(" Vị trí ứng tuyển không được bỏ trống!");
                    return;
                }


                if (!this.ungVien.kenhTuyenDungCode) {
                    this.message.warn(" Kênh tuyển dụng không được bỏ trống!");
                    return;
                }
                // this.ungVien.ngaySinh = moment(this.ungVien.ngaySinh).utc(true);
                // this.ungVien.ngayCap = moment(this.ungVien.ngayCap).utc(true);


                this.createOrEditUngVienInput.ungVien = this.ungVien;
                console.log(this.ungVien);

                this.createOrEditUngVienInput.lichSuUpLoad = this.lichSuUploadList;
                this._ungViensServiceProxy.createOrEdit(this.createOrEditUngVienInput)
                    .pipe(finalize(() => { this.saving = false; }))
                    .subscribe(() => {
                        this.notify.info(this.l('SavedSuccessfully'));
                        //this.show();
                        this.router.navigate(['/app/main/qlns/ungViens']);
                    });

            });
        }
    }

    //-------------Validation Ngày phỏng vấn -------------------

    validateNgayPV() {
        var dateNow = moment().startOf('day').utc(true);
        var ngaySinhDate = moment(this.ungVien.ngaySinh).utc(true);
        if(dateNow.year() - ngaySinhDate.year() <= 18){
            this.notify.error(this.l('Ứng viên lớn hơn 18 tuổi'));
            return false;
        }

        if (this.dayTime2 && this.dayTime3) {
            var datePV2 = moment(this.dayTime2).utc(true);
            var datePV3 = moment(this.dayTime3).utc(true);
            if (datePV2 <= datePV1){
                this.notify.error(this.l('Ngày phỏng vấn lần 2 phải lớn hơn ngày phỏng vấn lần 1'));
                return false;
            }
        }
        if (this.dayTime1 && this.dayTime2) {
            var datePV1 = moment(this.dayTime1).utc(true);
            var datePV2 = moment(this.dayTime2).utc(true);
            if (datePV3 <= datePV2){
                this.notify.error(this.l('Ngày phỏng vấn lần 3 phải lớn hơn ngày phỏng vấn lần 2'));
                return false;
            }
        }

        return true;
    }

    validateNgaySinh(event: any){
        var dateNow = moment().startOf('day').utc(true);
        var ngaySinhDate = moment(event).utc(true);
        if(dateNow.year() - ngaySinhDate.year() <= 18)
            this.notify.error(this.l('Ứng viên lớn hơn 18 tuổi'));
    }

    validateNgayPV2(event: any) {
        var datePV1 = moment(this.dayTime1).utc(true);
        var datePV2 = moment(event).utc(true);
        if (datePV2 <= datePV1)
            this.notify.error(this.l('Ngày phỏng vấn lần 2 phải lớn hơn ngày phỏng vấn lần 1'));
    }

    validateNgayPV3(event: any) {
        var datePV2 = moment(this.dayTime2).utc(true);
        var datePV3 = moment(event).utc(true);
        if (datePV3 <= datePV2)
            this.notify.error(this.l('Ngày phỏng vấn lần 3 phải lớn hơn ngày phỏng vấn lần 2'));
    }
}




import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef, Directive, Input, Sanitizer, SimpleChanges, SecurityContext, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, LichSuLamViecDto, LichSuLamViecsServiceProxy, SessionServiceProxy, UngVienDto, TemplatesServiceProxy, TemplateDto, HostSettingsEditDto, ComboboxItemDto, SettingScopes, SendTestEmailInput, HostSettingsServiceProxy, ConfigEmailsServiceProxy, ConfigEmailDto, GetConfigEmailForViewDto, CreateOrEditLichSuLamViecDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, HoSosServiceProxy, CreateOrEditHoSoDto, CreateOrEditUngVienInput, LichSuUploadsServiceProxy, CreateOrEditHoSoInput } from '@shared/service-proxies/service-proxies'; import { AppComponentBase } from '@shared/common/app-component-base';
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
    selector: 'viewUngVienModal',
    // encapsulation: ViewEncapsulation.None,
    templateUrl: './view-ungVien-modal.component.html'
})
export class ViewUngVienModalComponent extends AppComponentBase implements OnInit {

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
    createOrEditHoSoInput: CreateOrEditHoSoInput = new CreateOrEditHoSoInput();
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
    hoSo: CreateOrEditHoSoDto = new CreateOrEditHoSoDto();
    idDV: any;
    treeData: any;
    nam: any;
    a = false;
    createOrEditUngVienInput: CreateOrEditUngVienInput = new CreateOrEditUngVienInput();

    danhSachDV: any;

    dayTime2: any;
    dayTime1: any;
    dayTime3: any;
    constructor(
        injector: Injector,

        private elementRef: ElementRef,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
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

    // importFileFromServer(event: any): void {
    //     ($('#roxyCustomPanel') as any).dialog({ modal: true, width: 875, height: 600 });
    // }

    // closeCustomRoxy(): void {
    //     ($('#roxyCustomPanel') as any).dialog('close');
    // }

    // onSelectCustomRoxy(fileSelected: any): void {
    //     EditUngVienComponent.$this.ungVien.noiDung = fileSelected;
    // }

    pheDuyet() {

        this.saving = true;
        console.log(this.appSession.userId)
        this.ungVien.maNguoiPheDuyet = this.appSession.userId
        this.ungVien.ngayPheDuyet = moment(this.currentDate).utc(true);
        console.log(this.ungVien.ngayPheDuyet)
        this.ungVien.trangThaiCode = 'DD';
        this.createOrEditUngVienInput.ungVien = this.ungVien;


        console.log(this.createOrEditUngVienInput)
        if (this.createOrEditUngVienInput) {
            this.hoSo.maNhanVien = this.ungVien.maUngVien;
            this.hoSo.hoVaTen = this.ungVien.hoVaTen;
            console.log(this.ungVien.tenCTY)
            this.hoSo.tenCty = this.ungVien.tenCTY;
            this.hoSo.donViCongTacID = this.ungVien.donViCongTacID;
            this.hoSo.viTriCongViecCode = this.ungVien.viTriUngTuyenCode;
            this.hoSo.gioiTinhCode = this.ungVien.gioiTinhCode;
            this.hoSo.ngaySinh = this.ungVien.ngaySinh;
            this.hoSo.soCMND = this.ungVien.soCMND;

            this.hoSo.trinhDoVanHoa = this.ungVien.trinhDoVanHoa;
            this.hoSo.emailCaNhan = this.ungVien.email;
            this.hoSo.sdt = this.ungVien.dienThoai;
            if (this.ungVien.soCMND) {
                this.hoSo.soCMND = this.ungVien.soCMND;
            }
            else {
                this.hoSo.soCMND = null;
            }
            if (this.ungVien.ngayCap) {
                this.hoSo.ngayCap = this.ungVien.ngayCap;
            }
            else {
                this.hoSo.ngayCap = null;
            }
            if (this.ungVien.noiCap) {
                this.hoSo.noiCap = this.ungVien.noiCap;
            }
            else {
                this.hoSo.noiCap = null;
            }

            if (this.ungVien.diaChi) {
                this.hoSo.diaChiHN = this.ungVien.diaChi;
            }
            else {
                this.hoSo.diaChiHN = null;
            }
            if (this.ungVien.tinhThanhID) {
                this.hoSo.tinhThanhID = this.ungVien.tinhThanhID;
            }
            else {
                this.hoSo.tinhThanhID = null;
            }

            if (this.ungVien.khoa) {
                this.hoSo.khoa = this.ungVien.khoa;
            }
            else {
                this.hoSo.khoa = null;
            }
            if (this.ungVien.chuyenNganh) {
                this.hoSo.chuyenNganh = this.ungVien.chuyenNganh;
            }
            else {
                this.hoSo.chuyenNganh = null;
            }
            if (this.ungVien.xepLoaiCode) {
                this.hoSo.xepLoaiCode = this.ungVien.xepLoaiCode;
            }
            else {
                this.hoSo.xepLoaiCode = null;
            }
            if (this.ungVien.namTotNghiep) {
                this.hoSo.namTotNghiep = this.ungVien.namTotNghiep;
            }
            else {
                this.hoSo.namTotNghiep = 0;
            }

            this.createOrEditHoSoInput.hoSo = this.hoSo;
            console.log(this.hoSo)
            this._hoSosServiceProxy.createOrEdit(this.createOrEditHoSoInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {

                });

        }

        this._ungViensServiceProxy.createOrEdit(this.createOrEditUngVienInput)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe((res) => {

                console.log(res);

                this.notify.info(this.l('Đã duyệt thành công'));
                this.a = !this.a;
            });
    }
    troVe() {
        this.router.navigate(['/app/main/qlns/ungViens']);
    }
    //  ghep chuoi ten file
    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        // this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        console.log(this.currentTime)
        this.value.forEach((ele) => {
            this.dataDisplay.push({ tepDinhKem: cValue + "/" + this.currentTime + "/" + ele.name });

        });


        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);

        console.log(this.selectedRows)

        this.tepDinhKemSave = this.selectedRows.map(x => x.tepDinhKem.toString()).join(';')
    }

    changeCTY(e) {
        console.log(e.value)




        this.danhSachDV = this.treeData.filter(x => x.label == e.value)[0].children;
        console.log(this.danhSachDV)

    }


    private getTreeDataFromServer(): void {
        let self = this;
        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;
            console.log(result)
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

        console.log(e)
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {

            this.danhSachCV = res;
            console.log(this.danhSachCV)

        })
        // // this.danhSachCV = this.danhSachDV.filter(x => x.label == e.value)[0].children;
        // // let DVID = this.danhSachCV.
        //  co
    }

    uploadTDK(e: any) {

        this.filedinhkem = e.file.name;
        // const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        // this.tenFile = this.filedinhkem;
        // console.log(this.tenFile)


        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        // this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.tenFile = this.filedinhkem;
        console.log(this.currentTime)
        this.tepDinhKem_Save = cValue + "/" + this.currentTime + "/" + e.file.name;
        // this.value.forEach((ele) => {
        //     this.dataDisplay.push({ tepDinhKem: cValue + "/" + this.currentTime + "/" + ele.name });

        // });


        // this.value.length = 0;
        // this.selected_Rows = this.selectedRows.concat(this.dataDisplay);

        // console.log(this.selected_Rows)

        // this.tepDinhKem_Save = this.selected_Rows.map(x => x.tepDinhKem.toString()).join(';')


    }



    click(tenfle: string) {
        console.log(tenfle)
        this.link = this.rootUrl + "/" + tenfle;
        window.open(this.link, '_blank');
    }

    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.ngaySinh = null;
        this.ngayCap = null;
        this.approvE_DT = null;
        // this._noiDaoTaosServiceProxy.getAllNoiDaoTao().subscribe((res) => {
        //     this.noiDaoTao = res;
        // });
        // this._tinhThanhsServiceProxy.getAllTinhThanh().subscribe((res) => {
        //     this.tinhThanh = res;
        // });


        // this.getAllTruongGiaoDich().subscribe((res) => {
        //     this.truongGiaoDich = res;
        //     this.viTriCongViec = this.truongGiaoDich.filter(x => x.code == 'VTUT');
        //     console.log(this.viTriCongViec)
        //     this.tienDoTuyenDung = this.truongGiaoDich.filter(x => x.code == 'TDTD');
        //     this.trangThai = this.truongGiaoDich.filter(x => x.code == 'TRTH');
        //     this.kenhTuyenDung = this.truongGiaoDich.filter(x => x.code == 'KTD');
        //     this.congty = this.truongGiaoDich.filter(x => x.code == 'CT');
        //     this.gioiTinh = this.truongGiaoDich.filter(x => x.code == 'GT');
        //     this.tinhTrangHonNhan = this.truongGiaoDich.filter(x => x.code == 'TTHN');
        //     this.trinhDoDaoTao = this.truongGiaoDich.filter(x => x.code == 'TDDT');
        //     this.xepLoaiHocLuc = this.truongGiaoDich.filter(x => x.code == 'XLHL');
        // });

        if (Id != null) {
            this.ungVienId = Number.parseInt(Id);
            this._ungViensServiceProxy.getUngVienForEdit(this.ungVienId).subscribe(result => {
                this.ungVien = result.ungVien;

                if (this.ungVien.trangThaiCode == "DD") {
                    this.a = !this.a;
                }
                else {
                    this.a = this.a;
                }
                this.email = result.ungVien.email;
                this.dayTime1 = this.ungVien.day1;
                this.dayTime2 = this.ungVien.day2;
                this.dayTime3 = this.ungVien.day3;
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

                if (result.configEmail && result.configEmail.configEmail) {
                    this.mail_From = result.configEmail.configEmail.diaChiEmail;
                    this.tenTruyCap = result.configEmail.configEmail.tenTruyCap;
                    this.matKhau = result.configEmail.configEmail.matKhau;
                    this.congSMTP = result.configEmail.configEmail.congSMTP;
                    this.diaChiIP = result.configEmail.configEmail.diaChiIP;
                }
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
        // if (Id != null) {
        //     this.ungVienId = Number.parseInt(Id);


        //     this._ungViensServiceProxy.getUngVienForEdit(this.ungVienId).subscribe(result => {
        //         this.ungVien = result.ungVien;
        //          console.log(this.ungVien)
        //         if(this.ungVien.trangThaiCode == "DD")
        //         {
        //             this.a = !this.a;
        //         }
        //         else
        //         {
        //             this.a = this.a;
        //         }


        //         this.danhSachDV = this.treeData.filter(x => x.label == this.ungVien.tenCTY)[0].children;
        //         console.log(this.danhSachDV)

        //         this._hoSosServiceProxy.getAllCongViec(this.ungVien.donViCongTacID).subscribe(res => {
        //             this.danhSachCV = res;
        //         })
        //         this._lichSuUploadsServiceProxy.getListLichSuUploadDto('UV', Id).subscribe(res => {
        //             this.selectedRows = res;

        //             console.log(this.selectedRows)
        //         })

        //         this.dayTime1 = this.ungVien.day1 ;
        //         this.dayTime2 =this.ungVien.day2;
        //         this.dayTime3 =this.ungVien.day3 ;

        //     });
        //     this._lichSuLamViecsServiceProxy.getLichSuLamViecByUngVien(this.ungVienId).subscribe(res => {

        //         console.log(res)
        //         this.lichsu = res;


        //     })
        // }
    }

    chonDV() {
        this.popupVisible = true;
    }



    trangChuUngVien() {
        window.location.reload();
    }
    getDaTa(e: any) {

        this._templatesServiceProxy.getTemplateForView(e.value).subscribe(res => {
            console.log(res);
            this.tenChuDe = res.template.tenTemplate;
            this.noiDungCD = res.template.noiDung;

            console.log(this.noiDungCD)
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
        console.log(e);
        this.link = this.rootUrl + "/" + e.displayValue;
        window.open(this.link, '_blank');

    }


    ngOnInit() {
        this.getTreeDataFromServer();
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
        // console.log(this.listYear)

        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;

            this.treeData = this._arrayToTreeConverterService.createTree(result.items,
                'parentId',
                'id',
                null,
                'items',
                [
                    {
                        target: 'text',
                        targetFunction(item) {
                            return item.displayName;
                        }
                    }

                ]);

            console.log(this.treeData)
        });

    }




}




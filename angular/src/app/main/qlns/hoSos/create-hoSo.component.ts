import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { HoSosServiceProxy, CreateOrEditHoSoDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, OrganizationUnitDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, DangKyKCBsServiceProxy, HopDongsServiceProxy, UngViensServiceProxy, TemplatesServiceProxy, CreateOrEditTemplateDto, TemplateDto, CreateOrEditHoSoInput, LichSuUploadNewDto, CreateOrEditQuyTrinhCongTacDto, DangKyKCBDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { OrganizationUnitsTreeComponent, IOrganizationUnitsTreeComponentData } from '@app/admin/shared/organization-unit-tree.component';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { CustomInputDropdownComponent } from '@app/shared/common/customControl/custom-inputDropdown.component';
import { AppConsts } from '@shared/AppConsts';
import { formatDate } from '@angular/common';
import { Router } from '@angular/router';
import { AppMenuItem } from '@app/shared/layout/nav/app-menu-item';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { DxTabPanelModule, DxCheckBoxModule, DxTemplateModule, DxFormComponent } from 'devextreme-angular';
import * as _ from 'lodash';
@Component({
    selector: 'createHoSo',
    templateUrl: './create-hoSo.component.html'
})
export class CreateHoSoComponent extends AppComponentBase implements OnInit {


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('organizationUnitTree', { static: false }) organizationUnitTree: OrganizationUnitsTreeComponent;
    @ViewChild('inputDropdown', { static: true }) private inputDropdown: CustomInputDropdownComponent;
    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    active = false;
    currentTime: any;
    uploadUrl: any;

    saving = false;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    tinhThanh: TinhThanhDto[] = [];
    noiDaoTao: NoiDaoTaoDto[] = [];
    giongHKTT = false;
    allOrganizationUnits: OrganizationUnitDto[];

    templateLHD: TemplateDto[] = [];
    memberedOrganizationUnits: string[];
    totalUnitCount = 0;
    treeData: any;
    uploadUrlImage: any;
    currentDate = new Date();
    rootUrl: string;
    profileImage: any;
    link: string;
    nameArr: any[] = [];
    dataDisplay = [];
    idDV: any;
    pathFileTemplate: any;
    value: any[] = [];
    cValue: any;
    name: any;
    selectedRows = [];
    popupVisible = false;
    valueImage: any;
    noiDangKy: any;
    hopDong: any;
    tepDinhKemSave = '';
    angular: any;
    publisherPopupVisible = false;
    tinhThanhIdSelected: number;
    noiDaoTaoIdSelected: number;
    defaultCbbOption: TruongGiaoDichDto[] = [];
    hoSo: CreateOrEditHoSoDto = new CreateOrEditHoSoDto();
    yearArr: any[];
    ngaySinh: Date;
    ngayCap: Date;
    // ngay_Sinh : any;
    ngayHetHan: Date;
    list_year: any[];
    ngayTapSu: Date;
    ngayThuViec: Date;
    ngayChinhThuc: Date;
    ngayThamGiaBH: Date;
    ngayHetHanBHYT: Date;
    ngay_Sinh: Date;
    approvE_DT: Date;
    dataRowDetail: any;
    nameKCB: any;
    idKCB: any;
    popupVisibleKCB = false;
    popupVisibleLHD = false;
    loaiHopDong: any;

    selected: any;
    linkHD: any;
    nameHopDong: any;
    idHopDong: any;
    danhSachPB: any;
    currentItem: any;
    congTy = [
        { key: 1, type: 'GSOFT' },
        { key: 2, type: 'GOBRANDING' },
    ];
    // labels_: AppMenuItem[] = [];
    fullMenu: AppMenuItem[] = [];
    years: any;
    danhSachCV: any;
    danhSachDV: any;
    tenCongTy: any;
    danhSach_KCB: any;
    tenNoiKCB: any;
    createOrEditHoSoInput: CreateOrEditHoSoInput = new CreateOrEditHoSoInput();
    listYear: [] = [];
    lichSuUploadList: any[] = [];
    quytrinhcongtac: CreateOrEditQuyTrinhCongTacDto = new CreateOrEditQuyTrinhCongTacDto();


    quatrinhcongtac = [
        { key: 1, tenCty: 'GSOFT', tungay: '2/7/2019 - 2/9/2019', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Thực tập sinh', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Bán thời gian' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/9/2019 - 2/11/2019', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Thử việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/11/2019 - 2/11/2020', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Đang làm việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/11/2020 - 2/5/2021', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 2', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Đang làm việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/5/2021', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 2', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Nghỉ việc' },
    ];


    noiDaoTaoList: NoiDaoTaoDto[]= [];
    tinhThanhList: TinhThanhDto[]= [];
    congty: TruongGiaoDichDto[] = [];
    noiKhamBenhList :DangKyKCBDto [] =[];
    templateList : TemplateDto [] = [];
    constructor(
        injector: Injector,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,

        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _hopDongsServiceProxy: HopDongsServiceProxy,
        private _templatesServiceProxy: TemplatesServiceProxy,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private _dangKyKCBsServiceProxy: DangKyKCBsServiceProxy,
        private router: Router,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _hoSosServiceProxy: HoSosServiceProxy
    ) {
        super(injector);
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
        this.uploadUrlImage = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_fileImage';
        this.appSession = injector.get(AppSessionService);

    }

    ngOnInit(): void {
        this.getTreeDataFromServer();
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



    chonThongTinKCB() {
        this.popupVisibleKCB = true;
    }



    uploadImage() {
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        this.value.forEach((x) => {
            this.profilePicture = AppConsts.remoteServiceBaseUrl + "\\" + cValue + "\\" + x.name;
        });
        this.value.length = 0;
    }


    changeCTY(e) {
        console.log(e.value)

        this._templatesServiceProxy.getListTemplate(e.value).subscribe(res => {

            this.loaiHopDong = res;
        })


        this.danhSachDV = this.treeData.filter(x => x.label == e.value)[0].children;
        console.log(this.danhSachDV)

    }


    selectItem(e) {

        console.log(e)
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {

            this.danhSachCV = res;
            console.log(this.danhSachCV)
        })

    }


    chonDonVi() {
        this.popupVisible = true
    }


    chonLoaiHopDong(e: any) {
        console.log(e)

        this.hoSo.hopDongHienTai = e.data.tenTemplate;
        this.linkHD = e.data.linkTemplate;
        this.popupVisibleLHD = false;
    }
    chonLoaiHD() {
        this.popupVisibleLHD = true;
    }


    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        console.log(this.currentTime)
        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                }
            }

        });
        console.log(this.value)

        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);
        console.log(this.selectedRows)

    }


    // chức năng  xóa file
    xoaFile(e: any) {

        console.log(this.selectedRows)
        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        console.log(this.selectedRows)
        this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    }
    // chức năng xem file chi tiết
    showDetail(e: any) {
        console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.value;
        console.log(this.link)
        window.open(this.link, '_blank');
    }


    showPublisherPopup() {
        this.publisherPopupVisible = true;
    }

    startEdit(e: any) {

        console.log(e)
        this.dataRowDetail = e.data;
        this.hoSo.maSoNoiKCB = this.dataRowDetail.maNoiKCB;

        this.hoSo.noiDangKyKCBID = this.dataRowDetail.id;
        this.tenNoiKCB = this.dataRowDetail.tenNoiKCB;
        this.hoSo.maTinhCap = this.dataRowDetail.tinhThanhID;
        this.popupVisibleKCB = false;
    }

    onChangeGiongHKTTCheckBox(e) {
        console.log(e)
        if (e.value == true) {
            this.hoSo.diaChiHN = this.hoSo.diaChiHKTT;
            this.hoSo.tinhThanhIDHN = this.hoSo.tinhThanhIDHKTT;
            this.hoSo.quocGiaHN = this.hoSo.quocGiaHKTT;
        }

        else {
            this.hoSo.diaChiHN = "";
            this.hoSo.tinhThanhIDHKTT = null;
            this.hoSo.quocGiaHN = "";
        }

    }
    troVe() {
        this.router.navigate(['/app/main/qlns/hoSos']);
    }


    show(): void {

        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {
            this.noiDaoTaoList = result.noiDaoTaoList;
            this.tinhThanhList = result.tinhThanhList;
            this.templateList = result.templateList;
            this.noiKhamBenhList=result.noiKhamBenhList;
            this.viTriCongViec = result.viTriCongViec;
            this.tienDoTuyenDung = result.tienDoTuyenDung;
            this.trangThai = result.trangThai;
            this.kenhTuyenDung = result.kenhTuyenDung;
            this.gioiTinh = result.gioiTinh;
            this.hinhThucLamViec = result.tinhTrangNhanSu;
            this.tinhTrangHonNhan = result.tinhTrangHonNhan;
            this.trinhDoDaoTao = result.trinhDoDaoTao;
            this.xepLoaiHocLuc = result.xepLoaiHocLuc;
            this.congty=result.congty ;
        });

        // this._noiDaoTaosServiceProxy.getAllNoiDaoTao().subscribe((res) => {
        //     this.noiDaoTao = _.orderBy(res, ['tenNoiDaoTao'], ['asc']);
        //     console.log(this.noiDaoTao)
        // });
        // this._tinhThanhsServiceProxy.getAllTinhThanh().subscribe((res) => {
        //     this.tinhThanh = _.orderBy(res, ['tenTinhThanh'], ['asc']);
        //     console.log(this.tinhThanh)
        //     this.tinhThanhIdSelected = this.tinhThanh.filter(x => x.maTinhThanh === 'HCM')[0].id;
        // });
        // this._dangKyKCBsServiceProxy.getAllNoiDangKy().subscribe((res) => {
        //     this.noiDangKy = res;
        //     console.log(this.noiDangKy)
        // });

        // this._templatesServiceProxy.getAllTemplate().subscribe((res) => {
        //     this.templateLHD = res;

        //     console.log(this.templateLHD)


        // });


        // this._hopDongsServiceProxy.getAllHopDong().subscribe((res) => {
        //     this.hopDong = res;
        // });
        // this.getAllTruongGiaoDich().subscribe((res) => {

        //     this.truongGiaoDich = res;
        //    // this.viTriCongViec = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'VTUT'), ['displayOrder'], ['asc']);
        //     this.tienDoTuyenDung = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TDTD'), ['displayOrder'], ['asc']);
        //     this.trangThai = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TRTH'), ['displayOrder'], ['asc']);
        //     this.kenhTuyenDung = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'KTD'), ['displayOrder'], ['asc']);
        //       this.congTy = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'CT'), ['displayOrder'], ['asc']);
        //     this.gioiTinh = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'GT'), ['displayOrder'], ['asc']);
        //     this.tinhTrangHonNhan = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TTHN'), ['displayOrder'], ['asc']);
        //     this.trinhDoDaoTao = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TDDT'), ['displayOrder'], ['asc']);
        //     this.xepLoaiHocLuc = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'XLHL'), ['displayOrder'], ['asc']);
        //     this.hinhThucLamViec = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TTNS'), ['displayOrder'], ['asc']);
        //     this.defaultCbbOption = _.orderBy(this.truongGiaoDich.filter(x => x.setDefault == true), ['displayOrder'], ['asc']);


        //     console.log(this.defaultCbbOption);
        //     this.hoSo = new CreateOrEditHoSoDto();
        //     this.initUngvien();
        // });

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

    initUngvien() {

        this.hoSo.gioiTinhCode = this.defaultCbbOption.find(x => x.code == 'GT') ? this.defaultCbbOption.find(x => x.code == 'GT').cdName : '';
        this.hoSo.tinhThanhID = this.tinhThanhIdSelected;
        this.hoSo.tinhTrangHonNhanCode = this.defaultCbbOption.find(x => x.code == 'TTHN') ? this.defaultCbbOption.find(x => x.code == 'TTHN').cdName : '';
        this.hoSo.trinhDoVanHoa = '12/12';
        this.hoSo.nguoiNhapCV = this.appSession.user.userName;
        this.hoSo.trinhDoDaoTaoCode = this.defaultCbbOption.find(x => x.code == 'TDDT') ? this.defaultCbbOption.find(x => x.code == 'TDDT').cdName : '';

    }

    editQTCongTac(e: any) {
        console.log(e);
    }
    deleteQTCongTac(E:any) {

    }
    save(): void {

        let result = this.documentForm.instance.validate();
        if (result.isValid && this.validateNgaySinh(this.hoSo.ngaySinh)) {
            this.saving = true;
            this.hoSo.anhDaiDien = this.profilePicture;

            if (!this.hoSo.hoVaTen) {
                this.message.warn("Họ tên không được bỏ trống!");
                return;
            }
            if (!this.hoSo.soCMND) {
                this.message.warn("CMND không được bỏ trống!");
                return;
            }
            if (!this.hoSo.donViCongTacID) {
                this.message.warn("Đơn vị công tác không được bỏ trống!");
                return;
            }
            if (!this.hoSo.viTriCongViecCode) {
                this.message.warn("Vị trí công việc không được bỏ trống!");
                return;
            }
            if (!this.hoSo.gioiTinhCode) {
                this.message.warn("Giới tính không được bỏ trống!");
                return;
            }
            if (!this.hoSo.quocTich) {
                this.message.warn("Quốc tịch không được bỏ trống!");
                return;
            }
            if (!this.hoSo.danToc) {
                this.message.warn("Dân tộc không được bỏ trống!");
                return;
            }
            if (!this.hoSo.ngaySinh) {
                this.message.warn("Ngày sinh không được bỏ trống!");
                return;
            }
           
            if (!this.hoSo.loaiHopDongID) {
                this.message.warn("Loại HD không được bỏ trống!");
                return;
            }
            

            this.hoSo.ngaySinh = moment(this.hoSo.ngaySinh).utc(true);
            this.hoSo.ngayCap = moment(this.hoSo.ngayCap).utc(true);
            this.hoSo.ngayHetHan = moment(this.hoSo.ngayHetHan).utc(true);
            this.hoSo.ngayTapSu = moment(this.hoSo.ngayTapSu).utc(true);
            this.hoSo.ngayThuViec = moment(this.hoSo.ngayThuViec).utc(true);
            this.hoSo.ngayChinhThuc = moment(this.hoSo.ngayChinhThuc).utc(true);


            this.createOrEditHoSoInput.hoSo = this.hoSo;
            for (var j = 0; j < this.selectedRows.length; j++) {
                let file = new LichSuUploadNewDto();
                file.tenFile = this.selectedRows[j].tenFile;
                file.tieuDe = this.selectedRows[j].tieuDe;
                file.dungLuong = this.selectedRows[j].dungLuong;
                this.lichSuUploadList.push(file);
            }
            this.createOrEditHoSoInput.lichSuUpLoad = this.lichSuUploadList;
            this._hoSosServiceProxy.createOrEdit(this.createOrEditHoSoInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.hoSo = new CreateOrEditHoSoDto();
                    this.router.navigate(['/app/main/qlns/hoSos']);
                    this.modalSave.emit(null);
                });
        }


    }

    validateNgaySinh(event: any){
        var dateNow = moment().startOf('day').utc(true);
        var ngaySinhDate = moment(event).utc(true);
        if(dateNow.year() - ngaySinhDate.year() <= 18){
            this.notify.error(this.l('Ứng viên lớn hơn 18 tuổi'));
            return false;
        }
        return true;
    }
}

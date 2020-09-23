import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, OnInit, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, SessionServiceProxy, UngVienDto, LichSuLamViecDto, ListResultDtoOfOrganizationUnitDto, OrganizationUnitServiceProxy, HoSosServiceProxy, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy, CreateOrEditUngVienInput, LichSuUploadNewDto, GetUngVienForViewDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { DxFileUploaderComponent, DxFormComponent } from 'devextreme-angular';
import { Router } from '@angular/router';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import * as _ from 'lodash';
import { Location } from '@angular/common';

@Component({
    selector: 'createUngVien',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./create-ungVien.component.less'],
    templateUrl: './create-ungVien.component.html'
})

export class CreateUngVienComponent extends AppComponentBase implements OnInit {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;
    @Input() dataBack = new GetUngVienForViewDto();
    @Input() onBack: GetUngVienForViewDto;

    active = false;
    saving = false;
    rootUrl: string;
    link: string;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    listComment: LichSuLamViecDto[] = [];
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    gioiTinh: TruongGiaoDichDto[] = [];
    congty: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    tinhThanh: TinhThanhDto[] = [];
    noiDaoTao: NoiDaoTaoDto[] = [];
    createOrEditUngVienInput: CreateOrEditUngVienInput = new CreateOrEditUngVienInput();
    ungVien: CreateOrEditUngVienDto = new CreateOrEditUngVienDto();
    ngaySinh: Date;
    ngayCap: Date;
    approvE_DT: Date;
    time: string;
    uploadUrl: string;
    selectedRows = [];
    userId: number;
    currentDate = new Date();
    nameArr: any[] = [];
    dataDisplay = [];
    value: any[] = [];
    currentTime: any;
    tepDinhKemSave = '';
    years: any;
    totalUnitCount = 0;
    treeData: any;
    cmndExists = false;
    ungVienForCheckCMND: UngVienDto[] = [];
    tinhThanhIdSelected: number;
    listYear: [] = [];
    defaultCbbOption: TruongGiaoDichDto[] = [];
    popupVisible = false;
    danhSachCV: any;
    idDV: any;
    nam: [] = [];
    tennam: string;
    incommDateOptions: any;
    lichSuUploadList: any[] = [];
    //  lichSuUpload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    lichSuUpload: LichSuUploadNewDto = new LichSuUploadNewDto();
    danhSachDV: any;
    time2: any;
    time3: any;
    dayTime2: any;
    dayTime1: any;
    dayTime3: any;
    currentYear: any;


    noiDaoTaoList: NoiDaoTaoDto[] = [];
    tinhThanhList: TinhThanhDto[] = [];
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
        private location: Location,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _session: SessionServiceProxy
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;

    }

    getActionID(event) {
        this.onBack = event;
        console.log(this.onBack)
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

    troVe() {
        this.router.navigate(['/app/main/qlns/ungViens']);
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

    //  ghep chuoi ten file
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
    changeCTY(e) {
        console.log(e.value)

        this.danhSachDV = _.orderBy(this.treeData.filter(x => x.label == e.value)[0].children, ['value'], ['asc']);

    }
    selectItem(e) {

        console.log(e)
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {
            this.danhSachCV = _.orderBy(res, ['label'], ['asc']);
            console.log(this.danhSachCV)

        })

    }






    show(): void {
        this.ngaySinh = null;
        this.ngayCap = null;
        this.approvE_DT = null;
        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {
            this.noiDaoTaoList = result.noiDaoTaoList;
            this.tinhThanhList = result.tinhThanhList;
            this.viTriCongViec = result.viTriCongViec;
            this.tienDoTuyenDung = result.tienDoTuyenDung;
            this.trangThai = result.trangThai;
            this.kenhTuyenDung = result.kenhTuyenDung;
            this.gioiTinh = result.gioiTinh;
            this.tinhTrangHonNhan = result.tinhTrangHonNhan;
            this.trinhDoDaoTao = result.trinhDoDaoTao;
            this.xepLoaiHocLuc = result.xepLoaiHocLuc;
            this.congty = result.congty;
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
        // this.getAllTruongGiaoDich().subscribe((res) => {
        //     this.truongGiaoDich = res;
        //     this.viTriCongViec = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'VTUT'), ['displayOrder'], ['asc']);
        //     this.tienDoTuyenDung = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TDTD'), ['displayOrder'], ['asc']);
        //     this.trangThai = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TRTH'), ['displayOrder'], ['asc']);
        //     this.kenhTuyenDung = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'KTD'), ['displayOrder'], ['asc']);
        //     this.congty = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'CT'), ['displayOrder'], ['asc']);
        //     this.gioiTinh = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'GT'), ['displayOrder'], ['asc']);
        //     this.tinhTrangHonNhan = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TTHN'), ['displayOrder'], ['asc']);
        //     this.trinhDoDaoTao = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'TDDT'), ['displayOrder'], ['asc']);
        //     this.xepLoaiHocLuc = _.orderBy(this.truongGiaoDich.filter(x => x.code == 'XLHL'), ['displayOrder'], ['asc']);
        //     this.defaultCbbOption = _.orderBy(this.truongGiaoDich.filter(x => x.setDefault == true), ['displayOrder'], ['asc']);

        //     console.log(this.gioiTinh)



        // });

        this.ungVien = new CreateOrEditUngVienDto();
        this.initUngvien();
    }

    initUngvien() {
        // this.ungVien.trangThai = this.defaultCbbOption.find(x => x.code == 'TRTH') ? this.defaultCbbOption.find(x => x.code == 'TRTH').cdName : '';
        // this.ungVien.gioiTinh = this.defaultCbbOption.find(x => x.code == 'GT') ? this.defaultCbbOption.find(x => x.code == 'GT').cdName : '';
        // this.ungVien.tinhThanh = this.tinhThanhIdSelected;
        this.ungVien.tinhTrangHonNhanCode = this.defaultCbbOption.find(x => x.code == 'TTHN') ? this.defaultCbbOption.find(x => x.code == 'TTHN').cdName : '';
        this.ungVien.trinhDoVanHoa = '12/12';
        this.ungVien.trinhDoDaoTaoCode = this.defaultCbbOption.find(x => x.code == 'TDDT') ? this.defaultCbbOption.find(x => x.code == 'TDDT').cdName : '';
        this.ungVien.tienDoTuyenDungCode = this.defaultCbbOption.find(x => x.code == 'TDTD') ? this.defaultCbbOption.find(x => x.code == 'TDTD').cdName : '';
    }

    save(): void {


        let result = this.documentForm.instance.validate();
        if (result.isValid && this.validateNgayPV()) {
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
            if (this.dayTime1) {
                this.ungVien.day1 = moment(this.dayTime1).utc(true);
                this.ungVien.time1 = formatDate(this.dayTime1, 'hh-mm a', 'en-US');
            }
            if (this.dayTime2) {
                this.ungVien.day2 = moment(this.dayTime2).utc(true);
                this.ungVien.time2 = formatDate(this.dayTime2, 'hh-mm a', 'en-US');
            }
            if (this.dayTime3) {
                this.ungVien.day3 = moment(this.dayTime3).utc(true);
                this.ungVien.time3 = formatDate(this.dayTime3, 'hh-mm a', 'en-US');
            }
            this.createOrEditUngVienInput.ungVien = this.ungVien;
            for (var j = 0; j < this.selectedRows.length; j++) {
                let file = new LichSuUploadNewDto();
                file.tenFile = this.selectedRows[j].tenFile;
                file.tieuDe = this.selectedRows[j].tieuDe;
                file.dungLuong = this.selectedRows[j].dungLuong;
                this.lichSuUploadList.push(file);
            }
            this.createOrEditUngVienInput.lichSuUpLoad = this.lichSuUploadList;
            this._ungViensServiceProxy.createOrEdit(this.createOrEditUngVienInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.documentForm.instance.resetValues();
                    this.router.navigate(['/app/main/qlns/ungViens']);
                });

        }
    }

    // asyncValidation(params) {
    //     return sendRequest(params.value);
    // }



    // // chức năng  xóa file
    // xoaFile(e: any) {

    //     console.log(this.selectedRows)
    //     this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
    //     console.log(this.selectedRows)
    //     this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    // }
    // // chức năng xem file chi tiết
    showDetail(e: any) {
        console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.data.tenFile;
        console.log(this.link)
        window.open(this.link, '_blank');
    }

    close(): void {

        this.active = false;
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

import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { HoSosServiceProxy } from '@shared/service-proxies/service-proxies';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { Table } from 'primeng/table';

@Component({
    selector: 'viewNhanVienModal',
    templateUrl: './view-nhanVien-model.component.html'
})
export class ViewNhanVienModalComponent extends AppComponentBase{

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('paginatormodel', { static: true }) paginatormodel: Paginator;
    @ViewChild('dataTablemodel', { static: true }) dataTablemodel: Table;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    filterText = '';
    tenCtyFilter = '';
    hopDongHienTaiFilter = '';
    soHDFilter = '';
    donViCongTacNameFilter = '';
    choNgoiFilter = '';
    maxNoiDaoTaoIDFilter: number;
    maxNoiDaoTaoIDFilterEmpty: number;
    minNoiDaoTaoIDFilter: number;
    minNoiDaoTaoIDFilterEmpty: number;
    maxLoaiHopDongIDFilter: number;
    maxLoaiHopDongIDFilterEmpty: number;
    minLoaiHopDongIDFilter: number;
    minLoaiHopDongIDFilterEmpty: number;
    maSoNoiKCBFilter = '';
    maxNoiDangKyKCBIDFilter: number;
    maxNoiDangKyKCBIDFilterEmpty: number;
    minNoiDangKyKCBIDFilter: number;
    minNoiDangKyKCBIDFilterEmpty: number;
    maxNgayHetHanBHYTFilter: moment.Moment;
    minNgayHetHanBHYTFilter: moment.Moment;
    soTheBHYTFilter = '';
    maTinhCapFilter = '';
    maSoBHXHFilter = '';
    soSoBHXHFilter = '';
    maxTyLeDongBHFilter: number;
    maxTyLeDongBHFilterEmpty: number;
    minTyLeDongBHFilter: number;
    minTyLeDongBHFilterEmpty: number;
    maxNgayThamGiaBHFilter: moment.Moment;
    minNgayThamGiaBHFilter: moment.Moment;
    thamGiaCongDoanFilter = -1;
    nganHangCodeFilter = '';
    tkNganHangFilter = '';
    donViSoCongChuanCodeFilter = '';
    soCongChuanFilter = '';
    luongDongBHFilter = '';
    luongCoBanFilter = '';
    bacLuongCodeFilter = '';
    maxSoNgayPhepFilter: number;
    maxSoNgayPhepFilterEmpty: number;
    minSoNgayPhepFilter: number;
    minSoNgayPhepFilterEmpty: number;
    maxNgayChinhThucFilter: moment.Moment;
    minNgayChinhThucFilter: moment.Moment;
    maxNgayThuViecFilter: moment.Moment;
    minNgayThuViecFilter: moment.Moment;
    maxNgayTapSuFilter: moment.Moment;
    minNgayTapSuFilter: moment.Moment;
    soSoQLLaoDongFilter = '';
    diaDiemLamViecCodeFilter = '';
    quanLyGianTiepFilter = '';
    quanLyTrucTiepFilter = '';
    trangThaiLamViecCodeFilter = '';
    bacFilter = '';
    capFilter = '';
    chucDanhFilter = '';
    maChamCongFilter = '';
    diaChiLHKCFilter = '';
    emailLHKCFilter = '';
    dtDiDongLHKCFilter = '';
    dtNhaRiengLHKCFilter = '';
    quanHeLHKCFilter = '';
    hoVaTenLHKCFilter = '';
    diaChiHNFilter = '';
    tinhThanhIDHNFilter = '';
    quocGiaHNFilter = '';
    laChuHoFilter = -1;
    maSoHoGiaDinhFilter = '';
    soSoHoKhauFilter = '';
    diaChiHKTTFilter = '';
    tinhThanhIDHKTTFilter = '';
    quocGiaHKTTFilter = '';
    facebookFilter = '';
    skypeFilter = '';
    noiSinhFilter = '';
    tinhThanhIDFilter = '';
    nguyenQuanFilter = '';
    emailKhacFilter = '';
    emailCoQuanFilter = '';
    emailCaNhanFilter = '';
    dtKhacFilter = '';
    dtNhaRiengFilter = '';
    dtCoQuanFilter = '';
    dtDiDongFilter = '';
    tepDinhKemFilter = '';
    tinhTrangHonNhanCodeFilter = '';
    xepLoaiCodeFilter = '';
    maxNamTotNghiepFilter: number;
    maxNamTotNghiepFilterEmpty: number;
    minNamTotNghiepFilter: number;
    minNamTotNghiepFilterEmpty: number;
    chuyenNganhFilter = '';
    khoaFilter = '';
    trinhDoDaoTaoCodeFilter = '';
    trinhDoVanHoaFilter = '';
    maxNgayHetHanFilter: moment.Moment;
    minNgayHetHanFilter: moment.Moment;
    noiCapFilter = '';
    maxNgayCapFilter: moment.Moment;
    minNgayCapFilter: moment.Moment;
    soCMNDFilter = '';
    quocTichFilter = '';
    tonGiaoFilter = '';
    danTocFilter = '';
    viTriCongViecCodeFilter = '';
    maxDonViCongTacIDFilter: number;
    maxDonViCongTacIDFilterEmpty: number;
    minDonViCongTacIDFilter: number;
    minDonViCongTacIDFilterEmpty: number;
    mstCaNhanFilter = '';
    maxNgaySinhFilter: moment.Moment;
    minNgaySinhFilter: moment.Moment;
    gioiTinhCodeFilter = '';
    anhDaiDienFilter = '';
    hoVaTenFilter = '';
    maNhanVienFilter = '';
    maxTinhThanhIDHNFilter : number;
    maxTinhThanhIDHNFilterEmpty : number;
    minTinhThanhIDHNFilter : number;
    minTinhThanhIDHNFilterEmpty : number;
    maxTinhThanhIDHKTTFilter : number;
    maxTinhThanhIDHKTTFilterEmpty : number;
    minTinhThanhIDHKTTFilter : number;
    minTinhThanhIDHKTTFilterEmpty : number;
    maxTinhThanhIDFilter : number;
    maxTinhThanhIDFilterEmpty : number;
    minTinhThanhIDFilter : number;
    minTinhThanhIDFilterEmpty : number;
    chiNhanhFilter = '';
    dvtFilter = '';
    maxNgayKyHDKTHFilter: moment.Moment;
    minNgayKyHDKTHFilter: moment.Moment;
    maxNgayKyHD36THFilter: moment.Moment;
    minNgayKyHD36THFilter: moment.Moment;
    maxNgayKyHD12THFilter: moment.Moment;
    minNgayKyHD12THFilter: moment.Moment;
    maxNgayKyHDTVFilter: moment.Moment;
    minNgayKyHDTVFilter: moment.Moment;
    maxNgayKYHDCTVFilter: moment.Moment;
    minNgayKYHDCTVFilter: moment.Moment;
    maxNgayKyHDKVFilter: moment.Moment;
    minNgayKyHDKVFilter: moment.Moment;
    maxNgayKYHDTTFilter: moment.Moment;
    minNgayKYHDTTFilter: moment.Moment;
    maxNgayKyHDFilter: moment.Moment;
    minNgayKyHDFilter: moment.Moment;
    fromDate:any;
    listDataHoSo: any;
    constructor(
        injector: Injector,
        private _hoSosServiceProxy: HoSosServiceProxy,
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.gettHoSos();
    }
    show(): void {
        this.active = true;
        this.modal.show();
    }
    close(): void {
        this.saving = false;
        this.active = false;
        this.modal.hide();
    }
    gettHoSos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginatormodel.changePage(0);
            return;
        }
        this._hoSosServiceProxy.getAll(
          this.filterText,
          this.tenCtyFilter,
          this.hopDongHienTaiFilter,
          this.soHDFilter,
          this.donViCongTacNameFilter,
          this.choNgoiFilter,
          this.maxNoiDaoTaoIDFilter == null ? this.maxNoiDaoTaoIDFilterEmpty: this.maxNoiDaoTaoIDFilter,
          this.minNoiDaoTaoIDFilter == null ? this.minNoiDaoTaoIDFilterEmpty: this.minNoiDaoTaoIDFilter,
          this.maxLoaiHopDongIDFilter == null ? this.maxLoaiHopDongIDFilterEmpty: this.maxLoaiHopDongIDFilter,
          this.minLoaiHopDongIDFilter == null ? this.minLoaiHopDongIDFilterEmpty: this.minLoaiHopDongIDFilter,
          this.maSoNoiKCBFilter,
          this.maxNoiDangKyKCBIDFilter == null ? this.maxNoiDangKyKCBIDFilterEmpty: this.maxNoiDangKyKCBIDFilter,
          this.minNoiDangKyKCBIDFilter == null ? this.minNoiDangKyKCBIDFilterEmpty: this.minNoiDangKyKCBIDFilter,
          this.maxNgayHetHanBHYTFilter,
          this.minNgayHetHanBHYTFilter,
          this.soTheBHYTFilter,
          this.maTinhCapFilter,
          this.maSoBHXHFilter,
          this.soSoBHXHFilter,
          this.maxTyLeDongBHFilter == null ? this.maxTyLeDongBHFilterEmpty: this.maxTyLeDongBHFilter,
          this.minTyLeDongBHFilter == null ? this.minTyLeDongBHFilterEmpty: this.minTyLeDongBHFilter,
          this.maxNgayThamGiaBHFilter,
          this.minNgayThamGiaBHFilter,
          this.thamGiaCongDoanFilter,
          this.nganHangCodeFilter,
          this.tkNganHangFilter,
          this.donViSoCongChuanCodeFilter,
          this.soCongChuanFilter,
          this.luongDongBHFilter,
          this.luongCoBanFilter,
          this.bacLuongCodeFilter,
          this.maxSoNgayPhepFilter == null ? this.maxSoNgayPhepFilterEmpty: this.maxSoNgayPhepFilter,
          this.minSoNgayPhepFilter == null ? this.minSoNgayPhepFilterEmpty: this.minSoNgayPhepFilter,
          this.maxNgayChinhThucFilter == null?this.fromDate: this.maxNgayChinhThucFilter,
          this.minNgayChinhThucFilter == null?this.fromDate: this.minNgayChinhThucFilter,
          this.maxNgayThuViecFilter == null?this.fromDate: this.maxNgayThuViecFilter,
          this.minNgayThuViecFilter  == null?this.fromDate: this.minNgayThuViecFilter,
          this.maxNgayTapSuFilter  == null?this.fromDate: this.maxNgayTapSuFilter,
          this.minNgayTapSuFilter  == null?this.fromDate: this.minNgayTapSuFilter,
          this.soSoQLLaoDongFilter,
          this.diaDiemLamViecCodeFilter,
          this.quanLyGianTiepFilter,
          this.quanLyTrucTiepFilter,
          this.trangThaiLamViecCodeFilter,
          this.bacFilter,
          this.capFilter,
          this.chucDanhFilter,
          this.maChamCongFilter,
          this.diaChiLHKCFilter,
          this.emailLHKCFilter,
          this.dtDiDongLHKCFilter,
          this.dtNhaRiengLHKCFilter,
          this.quanHeLHKCFilter,
          this.hoVaTenLHKCFilter,
          this.diaChiHNFilter,
          this.maxTinhThanhIDHNFilter == null ? this.maxTinhThanhIDHNFilterEmpty: this.maxTinhThanhIDHNFilter,
          this.minTinhThanhIDHNFilter == null ? this.minTinhThanhIDHNFilterEmpty: this.minTinhThanhIDHNFilter,
          this.quocGiaHNFilter,
          this.laChuHoFilter,
          this.maSoHoGiaDinhFilter,
          this.soSoHoKhauFilter,
          this.diaChiHKTTFilter,
          this.maxTinhThanhIDHKTTFilter == null ? this.maxTinhThanhIDHKTTFilterEmpty: this.maxTinhThanhIDHKTTFilter,
          this.minTinhThanhIDHKTTFilter == null ? this.minTinhThanhIDHKTTFilterEmpty: this.minTinhThanhIDHKTTFilter,
          this.quocGiaHKTTFilter,
          this.facebookFilter,
          this.skypeFilter,
          this.noiSinhFilter,
          this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty: this.maxTinhThanhIDFilter,
          this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty: this.minTinhThanhIDFilter,
          this.nguyenQuanFilter,
          this.emailKhacFilter,
          this.emailCoQuanFilter,
          this.emailCaNhanFilter,
          this.dtKhacFilter,
          this.dtNhaRiengFilter,
          this.dtCoQuanFilter,
          this.dtDiDongFilter,
          this.tepDinhKemFilter,
          this.tinhTrangHonNhanCodeFilter,
          this.xepLoaiCodeFilter,
          this.maxNamTotNghiepFilter == null ? this.maxNamTotNghiepFilterEmpty: this.maxNamTotNghiepFilter,
          this.minNamTotNghiepFilter == null ? this.minNamTotNghiepFilterEmpty: this.minNamTotNghiepFilter,
          this.chuyenNganhFilter,
          this.khoaFilter,
          this.trinhDoDaoTaoCodeFilter,
          this.trinhDoVanHoaFilter,
          this.maxNgayHetHanFilter,
          this.minNgayHetHanFilter,
          this.noiCapFilter,
          this.maxNgayCapFilter,
          this.minNgayCapFilter,
          this.soCMNDFilter,
          this.quocTichFilter,
          this.tonGiaoFilter,
          this.danTocFilter,
          this.viTriCongViecCodeFilter,
          this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty: this.maxDonViCongTacIDFilter,
          this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty: this.minDonViCongTacIDFilter,
          this.mstCaNhanFilter,
          this.maxNgaySinhFilter,
          this.minNgaySinhFilter,
          this.gioiTinhCodeFilter,
          this.anhDaiDienFilter,
          this.hoVaTenFilter,
          this.maNhanVienFilter,
          this.chiNhanhFilter,
          this.dvtFilter,
          this.maxNgayKyHDKTHFilter,
          this.minNgayKyHDKTHFilter,
          this.maxNgayKyHD36THFilter,
          this.minNgayKyHD36THFilter,
          this.maxNgayKyHD12THFilter,
          this.minNgayKyHD12THFilter,
          this.maxNgayKyHDTVFilter,
          this.minNgayKyHDTVFilter,
          this.maxNgayKYHDCTVFilter,
          this.minNgayKYHDCTVFilter,
          this.maxNgayKyHDKVFilter,
          this.minNgayKyHDKVFilter,
          this.maxNgayKYHDTTFilter,
          this.minNgayKYHDTTFilter,
          this.maxNgayKyHDFilter,
          this.minNgayKyHDFilter,
          this.primengTableHelper.getSorting(this.dataTablemodel),
          this.primengTableHelper.getSkipCount(this.paginatormodel, event),
          10
      ).subscribe(result => {
          this.primengTableHelper.totalRecordsCount = result.totalCount;
          this.listDataHoSo = result.items;
          this.primengTableHelper.records = this.listDataHoSo;
    
      });
    }
}

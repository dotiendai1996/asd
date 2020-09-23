import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ViTriCongViecsServiceProxy, ViTriCongViecDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditViTriCongViecModalComponent } from './create-or-edit-viTriCongViec-modal.component';
import { ViewViTriCongViecModalComponent } from './view-viTriCongViec-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './viTriCongViecs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViTriCongViecsComponent extends AppComponentBase {

    @ViewChild('createOrEditViTriCongViecModal', { static: true }) createOrEditViTriCongViecModal: CreateOrEditViTriCongViecModalComponent;
    @ViewChild('viewViTriCongViecModalComponent', { static: true }) viewViTriCongViecModal: ViewViTriCongViecModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    tenCtyFilter = '';
    hopDongHienTaiFilter = '';
    soHDFilter = '';
    donViCongTacNameFilter = '';
    choNgoiFilter = '';
    maxNoiDaoTaoIDFilter : number;
		maxNoiDaoTaoIDFilterEmpty : number;
		minNoiDaoTaoIDFilter : number;
		minNoiDaoTaoIDFilterEmpty : number;
    maxLoaiHopDongIDFilter : number;
		maxLoaiHopDongIDFilterEmpty : number;
		minLoaiHopDongIDFilter : number;
		minLoaiHopDongIDFilterEmpty : number;
    maSoNoiKCBFilter = '';
    maxNoiDangKyKCBIDFilter : number;
		maxNoiDangKyKCBIDFilterEmpty : number;
		minNoiDangKyKCBIDFilter : number;
		minNoiDangKyKCBIDFilterEmpty : number;
    maxNgayHetHanBHYTFilter : moment.Moment;
		minNgayHetHanBHYTFilter : moment.Moment;
    soTheBHYTFilter = '';
    maTinhCapFilter = '';
    maSoBHXHFilter = '';
    soSoBHXHFilter = '';
    maxTyLeDongBHFilter : number;
		maxTyLeDongBHFilterEmpty : number;
		minTyLeDongBHFilter : number;
		minTyLeDongBHFilterEmpty : number;
    maxNgayThamGiaBHFilter : moment.Moment;
		minNgayThamGiaBHFilter : moment.Moment;
    thamGiaCongDoanFilter = -1;
    nganHangCodeFilter = '';
    tkNganHangFilter = '';
    donViSoCongChuanCodeFilter = '';
    soCongChuanFilter = '';
    luongDongBHFilter = '';
    luongCoBanFilter = '';
    bacLuongCodeFilter = '';
    maxSoNgayPhepFilter : number;
		maxSoNgayPhepFilterEmpty : number;
		minSoNgayPhepFilter : number;
		minSoNgayPhepFilterEmpty : number;
    maxNgayChinhThucFilter : moment.Moment;
		minNgayChinhThucFilter : moment.Moment;
    maxNgayThuViecFilter : moment.Moment;
		minNgayThuViecFilter : moment.Moment;
    maxNgayTapSuFilter : moment.Moment;
		minNgayTapSuFilter : moment.Moment;
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
    maxTinhThanhIDHNFilter : number;
		maxTinhThanhIDHNFilterEmpty : number;
		minTinhThanhIDHNFilter : number;
		minTinhThanhIDHNFilterEmpty : number;
    quocGiaHNFilter = '';
    laChuHoFilter = -1;
    maSoHoGiaDinhFilter = '';
    soSoHoKhauFilter = '';
    diaChiHKTTFilter = '';
    maxTinhThanhIDHKTTFilter : number;
		maxTinhThanhIDHKTTFilterEmpty : number;
		minTinhThanhIDHKTTFilter : number;
		minTinhThanhIDHKTTFilterEmpty : number;
    quocGiaHKTTFilter = '';
    facebookFilter = '';
    skypeFilter = '';
    noiSinhFilter = '';
    maxTinhThanhIDFilter : number;
		maxTinhThanhIDFilterEmpty : number;
		minTinhThanhIDFilter : number;
		minTinhThanhIDFilterEmpty : number;
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
    maxNamTotNghiepFilter : number;
		maxNamTotNghiepFilterEmpty : number;
		minNamTotNghiepFilter : number;
		minNamTotNghiepFilterEmpty : number;
    chuyenNganhFilter = '';
    khoaFilter = '';
    trinhDoDaoTaoCodeFilter = '';
    trinhDoVanHoaFilter = '';
    maxNgayHetHanFilter : moment.Moment;
		minNgayHetHanFilter : moment.Moment;
    noiCapFilter = '';
    maxNgayCapFilter : moment.Moment;
		minNgayCapFilter : moment.Moment;
    soCMNDFilter = '';
    quocTichFilter = '';
    tonGiaoFilter = '';
    danTocFilter = '';
    viTriCongViecCodeFilter = '';
    maxDonViCongTacIDFilter : number;
		maxDonViCongTacIDFilterEmpty : number;
		minDonViCongTacIDFilter : number;
		minDonViCongTacIDFilterEmpty : number;
    mstCaNhanFilter = '';
    maxNgaySinhFilter : moment.Moment;
		minNgaySinhFilter : moment.Moment;
    gioiTinhCodeFilter = '';
    anhDaiDienFilter = '';
    hoVaTenFilter = '';
    maNhanVienFilter = '';
    chiNhanhFilter = '';
    dvtFilter = '';
    maxNgayKyHDKTHFilter : moment.Moment;
		minNgayKyHDKTHFilter : moment.Moment;
    maxNgayKyHD36THFilter : moment.Moment;
		minNgayKyHD36THFilter : moment.Moment;
    maxNgayKyHD12THFilter : moment.Moment;
		minNgayKyHD12THFilter : moment.Moment;
    maxNgayKyHDTVFilter : moment.Moment;
		minNgayKyHDTVFilter : moment.Moment;
    maxNgayKYHDCTVFilter : moment.Moment;
		minNgayKYHDCTVFilter : moment.Moment;
    maxNgayKyHDKVFilter : moment.Moment;
		minNgayKyHDKVFilter : moment.Moment;
    maxNgayKYHDTTFilter : moment.Moment;
		minNgayKYHDTTFilter : moment.Moment;
    maxNgayKyHDFilter : moment.Moment;
		minNgayKyHDFilter : moment.Moment;




    constructor(
        injector: Injector,
        private _viTriCongViecsServiceProxy: ViTriCongViecsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getViTriCongViecs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._viTriCongViecsServiceProxy.getAll(
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
            this.maxNgayChinhThucFilter,
            this.minNgayChinhThucFilter,
            this.maxNgayThuViecFilter,
            this.minNgayThuViecFilter,
            this.maxNgayTapSuFilter,
            this.minNgayTapSuFilter,
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
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createViTriCongViec(): void {
        this.createOrEditViTriCongViecModal.show();
    }

    deleteViTriCongViec(viTriCongViec: ViTriCongViecDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._viTriCongViecsServiceProxy.delete(viTriCongViec.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._viTriCongViecsServiceProxy.getViTriCongViecsToExcel(
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
            this.maxNgayChinhThucFilter,
            this.minNgayChinhThucFilter,
            this.maxNgayThuViecFilter,
            this.minNgayThuViecFilter,
            this.maxNgayTapSuFilter,
            this.minNgayTapSuFilter,
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
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}

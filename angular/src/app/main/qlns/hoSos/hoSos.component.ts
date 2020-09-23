import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HoSosServiceProxy, HoSoDto, UngViensServiceProxy, CreateOrEditHoSoDto, GetHoSoForViewDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
//import { CreateOrEditHoSoModalComponent } from './create-hoSo.component';
import { ViewHoSoModalComponent } from './view-hoSo-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { LocalStorageService } from '@shared/utils/local-storage.service';
import { DxTextBoxModule, DxFileUploaderModule, DxButtonModule, DxFileUploaderComponent } from 'devextreme-angular';

@Component({
  templateUrl: './hoSos.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class HoSosComponent extends AppComponentBase implements OnInit {

  //@ViewChild('createOrEditHoSoModal', { static: true }) createOrEditHoSoModal: CreateOrEditHoSoModalComponent;
  // @ViewChild('viewHoSomodal.component', { static: true }) viewHoSoModal: ViewHoSoModalComponent;
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('fileUploader1', { static: false }) fileUploader1: DxFileUploaderComponent;
  @ViewChild('paginator', { static: true }) paginator: Paginator;
  advancedFiltersAreShown = false;
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
  maxTinhThanhIDHNFilter: number;
  maxTinhThanhIDHNFilterEmpty: number;
  minTinhThanhIDHNFilter: number;
  minTinhThanhIDHNFilterEmpty: number;
  maxTinhThanhIDHKTTFilter: number;
  maxTinhThanhIDHKTTFilterEmpty: number;
  minTinhThanhIDHKTTFilter: number;
  minTinhThanhIDHKTTFilterEmpty: number;
  maxTinhThanhIDFilter: number;
  maxTinhThanhIDFilterEmpty: number;
  minTinhThanhIDFilter: number;
  minTinhThanhIDFilterEmpty: number;
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

  minLoaiHopDongCodeFilter;
  name: any;
  listDataHoSo: any[] = [];
  selectedRowsData: any[] = [];
  data: any;
  a = true;
  id: any;
  currentDate = new Date();
  currentTime: any;
  value: any[] = [];
  dataDisplay: any;
  uploadUrl: any;
  rootUrl: any;
  viTriCongViec: any;
  trinhTrangNhanSu: any;
  trangThaiCodeFilter: any;
  fromDate: any;
  xemChiTietHoSo = new GetHoSoForViewDto();
  hoSoConts = 'hoSo.Filter';
  selectedName: any;
  hoverName: any;
  constructor(
    injector: Injector,
    private _hoSosServiceProxy: HoSosServiceProxy,
    private _ungViensServiceProxy: UngViensServiceProxy,
    private _notifyService: NotifyService,
    private _tokenAuth: TokenAuthServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _localStorageService: LocalStorageService,
    private router: Router,
    private _fileDownloadService: FileDownloadService
  ) {
    super(injector);
    this.rootUrl = AppConsts.remoteServiceBaseUrl;
    this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();

    this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
  }
  ngOnInit(): void {
    this.getListItemSearchHoSo();
    this._localStorageService.getItem('hoSo.Filter', (err, filters) => {
      this.bindingToFilter(filters);
      this.getHoSos();
    });
    // this.getHoSos();

  }
  bindingToFilter(filters: any) {
    if (filters) {
      this.trangThaiLamViecCodeFilter = filters.trangThaiLamViecCodeFilter;
      this.maxNgayChinhThucFilter = filters.maxNgayChinhThucFilter==null ? null : moment(filters.maxNgayChinhThucFilter);
      this.minNgayChinhThucFilter = filters.minNgayChinhThucFilter==null ? null : moment(filters.minNgayChinhThucFilter);
      this.maxNgayThuViecFilter = filters.maxNgayThuViecFilter==null ? null : moment(filters.maxNgayThuViecFilter);
      this.minNgayThuViecFilter = filters.minNgayThuViecFilter==null ? null : moment(filters.minNgayThuViecFilter);
      this.maxNgayTapSuFilter = filters.maxNgayTapSuFilter==null ? null : moment(filters.maxNgayTapSuFilter);
      this.minNgayTapSuFilter = filters.minNgayTapSuFilter==null ? null : moment(filters.minNgayTapSuFilter);
      this.filterText = filters.filterText;
    }
  }
  parseToFiltersDto() {
    return {
      filterText: this.filterText,
      minNgayTapSuFilter: this.minNgayTapSuFilter,
      maxNgayTapSuFilter: this.maxNgayTapSuFilter,
      minNgayThuViecFilter: this.minNgayThuViecFilter,
      maxNgayThuViecFilter: this.maxNgayThuViecFilter,
      minNgayChinhThucFilter: this.minNgayChinhThucFilter,
      maxNgayChinhThucFilter: this.maxNgayChinhThucFilter,
      trangThaiLamViecCodeFilter: this.trangThaiLamViecCodeFilter
    }
  }
  searchHoSo() {

    this.getHoSos();
  }

  startEdit(e: any) {

    //console.log(this.data)

    this.id = e;
    this.selectedName = e;
    if (this.id != null) {// && this.data!=null && this.xemChiTietUngVien!=null
      this.a = false;
    } else {
      this.a = true;
    }
  }

  lamtuoi() {
    this.trangThaiLamViecCodeFilter = "";
    this.maxNgayChinhThucFilter = this.fromDate;
    this.minNgayChinhThucFilter = this.fromDate;
    this.maxNgayThuViecFilter = this.fromDate;
    this.minNgayThuViecFilter = this.fromDate;
    this.maxNgayTapSuFilter = this.fromDate;
    this.minNgayTapSuFilter = this.fromDate;
    this.filterText = "";
  }

  createHoSo(): void {
    this.router.navigate(['/app/main/qlns/hoSos/create']);
  }

  getHoSos(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }
    this._localStorageService.setItem(this.hoSoConts, this.parseToFiltersDto());
    this._hoSosServiceProxy.getAll(
      this.filterText,
      this.tenCtyFilter,
      this.hopDongHienTaiFilter,
      this.soHDFilter,
      this.donViCongTacNameFilter,
      this.choNgoiFilter,
      this.maxNoiDaoTaoIDFilter == null ? this.maxNoiDaoTaoIDFilterEmpty : this.maxNoiDaoTaoIDFilter,
      this.minNoiDaoTaoIDFilter == null ? this.minNoiDaoTaoIDFilterEmpty : this.minNoiDaoTaoIDFilter,
      this.maxLoaiHopDongIDFilter == null ? this.maxLoaiHopDongIDFilterEmpty : this.maxLoaiHopDongIDFilter,
      this.minLoaiHopDongIDFilter == null ? this.minLoaiHopDongIDFilterEmpty : this.minLoaiHopDongIDFilter,
      this.maSoNoiKCBFilter,
      this.maxNoiDangKyKCBIDFilter == null ? this.maxNoiDangKyKCBIDFilterEmpty : this.maxNoiDangKyKCBIDFilter,
      this.minNoiDangKyKCBIDFilter == null ? this.minNoiDangKyKCBIDFilterEmpty : this.minNoiDangKyKCBIDFilter,
      this.maxNgayHetHanBHYTFilter,
      this.minNgayHetHanBHYTFilter,
      this.soTheBHYTFilter,
      this.maTinhCapFilter,
      this.maSoBHXHFilter,
      this.soSoBHXHFilter,
      this.maxTyLeDongBHFilter == null ? this.maxTyLeDongBHFilterEmpty : this.maxTyLeDongBHFilter,
      this.minTyLeDongBHFilter == null ? this.minTyLeDongBHFilterEmpty : this.minTyLeDongBHFilter,
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
      this.maxSoNgayPhepFilter == null ? this.maxSoNgayPhepFilterEmpty : this.maxSoNgayPhepFilter,
      this.minSoNgayPhepFilter == null ? this.minSoNgayPhepFilterEmpty : this.minSoNgayPhepFilter,
      this.maxNgayChinhThucFilter == null ? this.fromDate : this.maxNgayChinhThucFilter,
      this.minNgayChinhThucFilter == null ? this.fromDate : this.minNgayChinhThucFilter,
      this.maxNgayThuViecFilter == null ? this.fromDate : this.maxNgayThuViecFilter,
      this.minNgayThuViecFilter == null ? this.fromDate : this.minNgayThuViecFilter,
      this.maxNgayTapSuFilter == null ? this.fromDate : this.maxNgayTapSuFilter,
      this.minNgayTapSuFilter == null ? this.fromDate : this.minNgayTapSuFilter,
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
      this.maxTinhThanhIDHNFilter == null ? this.maxTinhThanhIDHNFilterEmpty : this.maxTinhThanhIDHNFilter,
      this.minTinhThanhIDHNFilter == null ? this.minTinhThanhIDHNFilterEmpty : this.minTinhThanhIDHNFilter,
      this.quocGiaHNFilter,
      this.laChuHoFilter,
      this.maSoHoGiaDinhFilter,
      this.soSoHoKhauFilter,
      this.diaChiHKTTFilter,
      this.maxTinhThanhIDHKTTFilter == null ? this.maxTinhThanhIDHKTTFilterEmpty : this.maxTinhThanhIDHKTTFilter,
      this.minTinhThanhIDHKTTFilter == null ? this.minTinhThanhIDHKTTFilterEmpty : this.minTinhThanhIDHKTTFilter,
      this.quocGiaHKTTFilter,
      this.facebookFilter,
      this.skypeFilter,
      this.noiSinhFilter,
      this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty : this.maxTinhThanhIDFilter,
      this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty : this.minTinhThanhIDFilter,
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
      this.maxNamTotNghiepFilter == null ? this.maxNamTotNghiepFilterEmpty : this.maxNamTotNghiepFilter,
      this.minNamTotNghiepFilter == null ? this.minNamTotNghiepFilterEmpty : this.minNamTotNghiepFilter,
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
      this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty : this.maxDonViCongTacIDFilter,
      this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty : this.minDonViCongTacIDFilter,
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
      this.listDataHoSo = result.items;
      this.primengTableHelper.records = this.listDataHoSo;

    });
  }
  downloadTemplate() {
    this._hoSosServiceProxy.getHoSoTemplateToExcel(
    )
      .subscribe(result => {
        this._fileDownloadService.downloadTempFile(result);
        this.notify.info(this.l('SavedSuccessfully'));
      });
  }
  public highlightRow(emp) {
    this.hoverName = emp.hoSo.id;
  }
  importToExcel() {

    const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
    console.log(this.value)
    this.value.forEach((ele) => {
      this.dataDisplay = ele.name
      this._hoSosServiceProxy.importToExcel(this.currentTime, this.dataDisplay).subscribe(res => {
        if (res == null) {
          this.message.success("Import thành công");
          this.getHoSos();
        }
        else {
          this.message.info("Nhập liệu chưa đúng");
        }

      })
      this.value.length = 0
    });

  }

  getListItemSearchHoSo() {
    this._hoSosServiceProxy.getListItemSearchHoSo().subscribe(result => {

      this.viTriCongViec = result.viTriCongViec;
      this.trinhTrangNhanSu = result.trangThai;
    })
  }
  editHoSoModal() {

    this.router.navigate(['/app/main/qlns/hoSos/edit/' + this.id]);
  }

  deleteHoSo(): void {
    this.message.confirm(
      '', this.l('AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this._hoSosServiceProxy.delete(this.id)
            .subscribe(() => {
              this.getHoSos();
              this.notify.success(this.l('SuccessfullyDeleted'));
            });
        }
      }
    );
  }
  viewChiTiet() {
    this.router.navigate(['/app/main/qlns/hoSo/XemChiTiet/' + this.id]);
  }

  // exportToExcel(): void {
  //   this._hoSosServiceProxy.getHoSosToExcel(
  //     this.filterText,
  //     this.maNhanVienFilter,
  //     this.hoVaTenFilter,
  //     this.anhDaiDienFilter,
  //     this.gioiTinhCodeFilter,
  //     this.maxNgaySinhFilter,
  //     this.minNgaySinhFilter,
  //     this.mstCaNhanFilter,
  //     this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty : this.maxDonViCongTacIDFilter,
  //     this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty : this.minDonViCongTacIDFilter,
  //     this.viTriCongViecCodeFilter,
  //     this.danTocFilter,
  //     this.tonGiaoFilter,
  //     this.quocTichFilter,
  //     this.soCMNDFilter,
  //     this.maxNgayCapFilter,
  //     this.minNgayCapFilter,
  //     this.noiCapFilter,
  //     this.maxNgayHetHanFilter,
  //     this.minNgayHetHanFilter,
  //     this.trinhDoVanHoaFilter,
  //     this.trinhDoDaoTaoCodeFilter,
  //     this.maxNoiDaoTaoCodeFilter == null ? this.maxNoiDaoTaoCodeFilterEmpty : this.maxNoiDaoTaoCodeFilter,
  //     this.minNoiDaoTaoCodeFilter == null ? this.minNoiDaoTaoCodeFilterEmpty : this.minNoiDaoTaoCodeFilter,
  //     this.khoaFilter,
  //     this.chuyenNganhFilter,
  //     this.maxNamTotNghiepFilter == null ? this.maxNamTotNghiepFilterEmpty : this.maxNamTotNghiepFilter,
  //     this.minNamTotNghiepFilter == null ? this.minNamTotNghiepFilterEmpty : this.minNamTotNghiepFilter,
  //     this.xepLoaiCodeFilter,
  //     this.tinhTrangHonNhanCodeFilter,
  //     this.tepDinhKemFilter,
  //     this.dtDiDongFilter,
  //     this.dtCoQuanFilter,
  //     this.dtNhaRiengFilter,
  //     this.dtKhacFilter,
  //     this.emailCaNhanFilter,
  //     this.emailCoQuanFilter,
  //     this.emailKhacFilter,
  //     this.nguyenQuanFilter,
  //     this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty : this.maxTinhThanhIDFilter,
  //     this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty : this.minTinhThanhIDFilter,
  //     this.noiSinhFilter,
  //     this.skypeFilter,
  //     this.facebookFilter,
  //     this.quocGiaHKTTFilter,
  //     this.maxTinhThanhIDHKTTFilter == null ? this.maxTinhThanhIDHKTTFilterEmpty : this.maxTinhThanhIDHKTTFilter,
  //     this.minTinhThanhIDHKTTFilter == null ? this.minTinhThanhIDHKTTFilterEmpty : this.minTinhThanhIDHKTTFilter,
  //     this.diaChiHKTTFilter,
  //     this.soSoHoKhauFilter,
  //     this.maSoHoGiaDinhFilter,
  //     this.laChuHoFilter,
  //     this.quocGiaHNFilter,
  //     this.maxTinhThanhIDHNFilter == null ? this.maxTinhThanhIDHNFilterEmpty : this.maxTinhThanhIDHNFilter,
  //     this.minTinhThanhIDHNFilter == null ? this.minTinhThanhIDHNFilterEmpty : this.minTinhThanhIDHNFilter,
  //     this.diaChiHNFilter,
  //     this.hoVaTenLHKCFilter,
  //     this.quanHeLHKCFilter,
  //     this.dtDiDongLHKCFilter,
  //     this.dtNhaRiengLHKCFilter,
  //     this.emailLHKCFilter,
  //     this.diaChiLHKCFilter,
  //     this.maChamCongFilter,
  //     this.chucDanhFilter,
  //     this.capFilter,
  //     this.bacFilter,
  //     this.trangThaiLamViecCodeFilter,
  //     this.quanLyTrucTiepFilter,
  //     this.quanLyGianTiepFilter,
  //     this.diaDiemLamViecCodeFilter,
  //     this.soSoQLLaoDongFilter,
  //     this.maxLoaiHopDongCodeFilter == null ? this.maxLoaiHopDongCodeFilterEmpty : this.maxLoaiHopDongCodeFilter,
  //     this.minLoaiHopDongCodeFilter == null ? this.minLoaiHopDongCodeFilterEmpty : this.minLoaiHopDongCodeFilter,
  //     this.maxNgayTapSuFilter,
  //     this.minNgayTapSuFilter,
  //     this.maxNgayThuViecFilter,
  //     this.minNgayThuViecFilter,
  //     this.maxNgayChinhThucFilter,
  //     this.minNgayChinhThucFilter,
  //     this.maxSoNgayPhepFilter == null ? this.maxSoNgayPhepFilterEmpty : this.maxSoNgayPhepFilter,
  //     this.minSoNgayPhepFilter == null ? this.minSoNgayPhepFilterEmpty : this.minSoNgayPhepFilter,
  //     this.bacLuongCodeFilter,
  //     this.maxLuongCoBanFilter == null ? this.maxLuongCoBanFilterEmpty : this.maxLuongCoBanFilter,
  //     this.minLuongCoBanFilter == null ? this.minLuongCoBanFilterEmpty : this.minLuongCoBanFilter,
  //     this.maxLuongDongBHFilter == null ? this.maxLuongDongBHFilterEmpty : this.maxLuongDongBHFilter,
  //     this.minLuongDongBHFilter == null ? this.minLuongDongBHFilterEmpty : this.minLuongDongBHFilter,
  //     this.maxSoCongChuanFilter == null ? this.maxSoCongChuanFilterEmpty : this.maxSoCongChuanFilter,
  //     this.minSoCongChuanFilter == null ? this.minSoCongChuanFilterEmpty : this.minSoCongChuanFilter,
  //     this.donViSoCongChuanCodeFilter,
  //     this.tkNganHangFilter,
  //     this.nganHangCodeFilter,
  //     this.thamGiaCongDoanFilter,
  //     this.maxNgayThamGiaBHFilter,
  //     this.minNgayThamGiaBHFilter,
  //     this.maxTyLeDongBHFilter == null ? this.maxTyLeDongBHFilterEmpty : this.maxTyLeDongBHFilter,
  //     this.minTyLeDongBHFilter == null ? this.minTyLeDongBHFilterEmpty : this.minTyLeDongBHFilter,
  //     this.soSoBHXHFilter,
  //     this.maSoBHXHFilter,
  //     this.maTinhCapFilter,
  //     this.soTheBHYTFilter,
  //     this.maxNgayHetHanBHYTFilter,
  //     this.minNgayHetHanBHYTFilter,
  //     this.maxNoiDangKyKCBIDFilter == null ? this.maxNoiDangKyKCBIDFilterEmpty : this.maxNoiDangKyKCBIDFilter,
  //     this.minNoiDangKyKCBIDFilter == null ? this.minNoiDangKyKCBIDFilterEmpty : this.minNoiDangKyKCBIDFilter,
  //     this.maSoNoiKCBFilter,
  //     this.autH_STATUSFilter,
  //     this.recorD_STATUSFilter,
  //     this.maxMARKER_IDFilter == null ? this.maxMARKER_IDFilterEmpty : this.maxMARKER_IDFilter,
  //     this.minMARKER_IDFilter == null ? this.minMARKER_IDFilterEmpty : this.minMARKER_IDFilter,
  //     this.maxCHECKER_IDFilter == null ? this.maxCHECKER_IDFilterEmpty : this.maxCHECKER_IDFilter,
  //     this.minCHECKER_IDFilter == null ? this.minCHECKER_IDFilterEmpty : this.minCHECKER_IDFilter,
  //     this.maxAPPROVE_DTFilter,
  //     this.minAPPROVE_DTFilter,
  //   )
  //     .subscribe(result => {
  //       this._fileDownloadService.downloadTempFile(result);
  //     });
  // }
}

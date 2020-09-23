import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HopDongsServiceProxy, HopDongDto, TruongGiaoDichDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
//import { CreateOrEditHopDongModalComponent } from './create-or-edit-hopDong-modal.component';
import { ViewHopDongModalComponent } from './view-hopDong-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LocalStorageService } from '@shared/utils/local-storage.service';

import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import $ from 'jquery';
import { formatDate } from '@angular/common';
import { result } from 'lodash';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { GetHopDongForViewDto } from '@shared/service-proxies/service-proxies';
declare const exportHTML: any;

@Component({
  templateUrl: './hopDongs.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class HopDongsComponent extends AppComponentBase implements OnInit {
  @ViewChild('viewHopDongModal', { static: true }) viewHopDongModal: ViewHopDongModalComponent;
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;

  advancedFiltersAreShown = false;
  filterText = '';
  hoTenNhanVienFilter = '';
  viTriCongViecCodeFilter = '';
  maxNgayKyFilter: moment.Moment;
  minNgayKyFilter: moment.Moment;
  maxDonViCongTacIDFilter: number;
  maxDonViCongTacIDFilterEmpty: number;
  minDonViCongTacIDFilter: number;
  minDonViCongTacIDFilterEmpty: number;
  tenHopDongFilter = '';
  loaiHopDongCodeFilter = '';
  hinhThucLamViecCodeFilter = '';
  maxNgayCoHieuLucFilter: moment.Moment;
  minNgayCoHieuLucFilter: moment.Moment;
  maxNgayHetHanFilter: moment.Moment;
  minNgayHetHanFilter: moment.Moment;
  maxLuongCoBanFilter: number;
  maxLuongCoBanFilterEmpty: number;
  minLuongCoBanFilter: number;
  minLuongCoBanFilterEmpty: number;
  maxLuongDongBaoHiemFilter: number;
  maxLuongDongBaoHiemFilterEmpty: number;
  minLuongDongBaoHiemFilter: number;
  minLuongDongBaoHiemFilterEmpty: number;
  chucDanhFilter = '';
  trichYeuFilter = '';
  recorD_STATUSFilter = '';
  printUrl = "";
  maxMARKER_IDFilter: number;
  maxMARKER_IDFilterEmpty: number;
  minMARKER_IDFilter: number;
  minMARKER_IDFilterEmpty: number;
  autH_STATUSFilter = '';
  maxCHECKER_IDFilter: number;
  maxCHECKER_IDFilterEmpty: number;
  minCHECKER_IDFilter: number;
  minCHECKER_IDFilterEmpty: number;
  maxAPPROVE_DTFilter: moment.Moment;
  minAPPROVE_DTFilter: moment.Moment;
  thoiHanHopDongFilter = '';
  loaiHopDong: TruongGiaoDichDto[] = [];
  selected: any;
  maHD: any;
  tenLoaiHD: any;
  truongGiaoDich: TruongGiaoDichDto[] = [];
  ghiChu: any;
  currentDate = new Date();
  code: any;
  listDataHopDong: any;
  selectedRowsData: any[] = [];
  data: any;
  id: any;
  a = true;
  currentTime: any;
  uploadUrl: any;
  value: any[] = [];
  uploadUrlImage: any;
  public selectedName: any;
  fromDate: any;
  hinhThucLamViec: any;
  loaihopdong: any;
  hopDongConts = 'hopDong.Filter';
  hoverName: any;
  constructor(
    injector: Injector,
    private _hopDongsServiceProxy: HopDongsServiceProxy,
    private _notifyService: NotifyService,
    private router: Router,
    private _tokenAuth: TokenAuthServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _localStorageService: LocalStorageService,
    private _fileDownloadService: FileDownloadService
  ) {
    super(injector);
    this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
    this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
    this.uploadUrlImage = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_fileImage';
  }
  editHopDong() {
    this.router.navigate(['/app/main/qlns/hopDong/edit/' + this.id]);

  }
  ngOnInit(): void {
    this.getListItemSearch();
    this._localStorageService.getItem('hopDong.Filter', (err, filters) => {
      this.bindingToFilter(filters);
      this.getHopDongs();
    });
  }
  bindingToFilter(filters: any) {
    if (filters) {
      this.hinhThucLamViecCodeFilter = filters.hinhThucLamViecCodeFilter;
      this.maxNgayKyFilter = filters.maxNgayKyFilter==null ? null : moment(filters.maxNgayKyFilter);
      this.minNgayKyFilter = filters.minNgayKyFilter==null ? null : moment(filters.minNgayKyFilter);
      this.filterText = filters.filterText;
    }
  }
  parseToFiltersDto() {
    return {
      filterText: this.filterText,
      minNgayKyFilter: this.minNgayKyFilter,
      maxNgayKyFilter: this.maxNgayKyFilter,
      hinhThucLamViecCodeFilter: this.hinhThucLamViecCodeFilter
    }
  }
  uploadImage() {
    const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

    this.value.length = 0;
  }

  exportHTML() {
    this._hopDongsServiceProxy.getHopDongForView(this.id).subscribe(res => {
      this.code = res.hopDong.linkTemplate;
      this.printUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/GetWordFile?hopDongId=' + this.id + '&&path=' + this.code + ' ';
      console.log(this.printUrl);
      console.log(this.code);
    })
    let dataEXport = this.data;


    $.ajax({

      url: this.printUrl,
      method: 'POST',
      xhrFields: {
        responseType: 'blob'
      },

      success: function (data1) {
        var a = document.createElement('a');
        var url = window.URL.createObjectURL(data1);

        a.href = url;
        a.download = dataEXport.loaiHopDongValue + ".docx";
        document.body.append(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
      }
    });


  }

  public highlightRow(emp) {
    this.hoverName = emp.hopDong.id;
  }
  getHopDongs(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }
    this._localStorageService.setItem(this.hopDongConts, this.parseToFiltersDto());
    this._hopDongsServiceProxy.getAll(
      this.filterText,
      this.hoTenNhanVienFilter,
      this.viTriCongViecCodeFilter,
      this.maxNgayKyFilter == null ? this.fromDate : this.maxNgayKyFilter,
      this.minNgayKyFilter == null ? this.fromDate : this.minNgayKyFilter,
      this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty : this.maxDonViCongTacIDFilter,
      this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty : this.minDonViCongTacIDFilter,
      this.tenHopDongFilter,
      this.loaiHopDongCodeFilter,
      this.hinhThucLamViecCodeFilter,
      this.maxNgayCoHieuLucFilter,
      this.minNgayCoHieuLucFilter,
      this.maxNgayHetHanFilter,
      this.minNgayHetHanFilter,
      this.maxLuongCoBanFilter == null ? this.maxLuongCoBanFilterEmpty : this.maxLuongCoBanFilter,
      this.minLuongCoBanFilter == null ? this.minLuongCoBanFilterEmpty : this.minLuongCoBanFilter,
      this.maxLuongDongBaoHiemFilter == null ? this.maxLuongDongBaoHiemFilterEmpty : this.maxLuongDongBaoHiemFilter,
      this.minLuongDongBaoHiemFilter == null ? this.minLuongDongBaoHiemFilterEmpty : this.minLuongDongBaoHiemFilter,
      this.chucDanhFilter,
      this.trichYeuFilter,
      this.recorD_STATUSFilter,
      this.maxMARKER_IDFilter == null ? this.maxMARKER_IDFilterEmpty : this.maxMARKER_IDFilter,
      this.minMARKER_IDFilter == null ? this.minMARKER_IDFilterEmpty : this.minMARKER_IDFilter,
      this.autH_STATUSFilter,
      this.maxCHECKER_IDFilter == null ? this.maxCHECKER_IDFilterEmpty : this.maxCHECKER_IDFilter,
      this.minCHECKER_IDFilter == null ? this.minCHECKER_IDFilterEmpty : this.minCHECKER_IDFilter,
      this.maxAPPROVE_DTFilter,
      this.minAPPROVE_DTFilter,
      this.thoiHanHopDongFilter,
      this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getSkipCount(this.paginator, event),
      this.primengTableHelper.getMaxResultCount(this.paginator, event)
    ).subscribe(result => {
      this.primengTableHelper.totalRecordsCount = result.totalCount;
      this.listDataHopDong = result.items;
      this.primengTableHelper.records = this.listDataHopDong
    });
  }

  startEdit(e: any) {
    this.id = e;
    this.selectedName = e;
    if (this.id != null) {
      this.a = false;
    } else {
      this.a = true;
    }
  }
  searchHopDong() {

    this.getHopDongs();
  }
  createHopDong() {
    this.router.navigate(['/app/main/qlns/hopDong/create']);
  }

  deleteHopDong(): void {

    this.message.confirm(
      '', this.l('AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this._hopDongsServiceProxy.delete(this.id)
            .subscribe(() => {
              this.getHopDongs();
              this.notify.success(this.l('SuccessfullyDeleted'));
            });
        }
      }
    );


  }


  getListItemSearch() {
    this._hopDongsServiceProxy.getListItemSearch().subscribe(result => {
      this.hinhThucLamViec = result.hinhThucLamViec
      // this.tinhThanhList = result.tinhThanhList;
      this.loaihopdong = result.templateList;
      console.log(this.hinhThucLamViec);
      console.log(this.loaihopdong);
    })
  }
  showDetail(){
    this._hopDongsServiceProxy.getHopDongForView(this.id).subscribe(result=> {
      this.viewHopDongModal.show(result);
    });
  }

  exportToExcel(): void {
    this._hopDongsServiceProxy.getHopDongsToExcel(
      this.filterText,
      this.hoTenNhanVienFilter,
      this.viTriCongViecCodeFilter,
      this.maxNgayKyFilter == null ? this.fromDate : this.maxNgayKyFilter,
      this.minNgayKyFilter == null ? this.fromDate : this.minNgayKyFilter,
      this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty : this.maxDonViCongTacIDFilter,
      this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty : this.minDonViCongTacIDFilter,
      this.tenHopDongFilter,
      this.loaiHopDongCodeFilter,
      this.hinhThucLamViecCodeFilter,
      this.maxNgayCoHieuLucFilter,
      this.minNgayCoHieuLucFilter,
      this.maxNgayHetHanFilter,
      this.minNgayHetHanFilter,
      this.maxLuongCoBanFilter == null ? this.maxLuongCoBanFilterEmpty : this.maxLuongCoBanFilter,
      this.minLuongCoBanFilter == null ? this.minLuongCoBanFilterEmpty : this.minLuongCoBanFilter,
      this.maxLuongDongBaoHiemFilter == null ? this.maxLuongDongBaoHiemFilterEmpty : this.maxLuongDongBaoHiemFilter,
      this.minLuongDongBaoHiemFilter == null ? this.minLuongDongBaoHiemFilterEmpty : this.minLuongDongBaoHiemFilter,
      this.chucDanhFilter,
      this.trichYeuFilter,
      this.recorD_STATUSFilter,
      this.maxMARKER_IDFilter == null ? this.maxMARKER_IDFilterEmpty : this.maxMARKER_IDFilter,
      this.minMARKER_IDFilter == null ? this.minMARKER_IDFilterEmpty : this.minMARKER_IDFilter,
      this.autH_STATUSFilter,
      this.maxCHECKER_IDFilter == null ? this.maxCHECKER_IDFilterEmpty : this.maxCHECKER_IDFilter,
      this.minCHECKER_IDFilter == null ? this.minCHECKER_IDFilterEmpty : this.minCHECKER_IDFilter,
      this.maxAPPROVE_DTFilter,
      this.minAPPROVE_DTFilter,
      this.thoiHanHopDongFilter,
    )
      .subscribe(result => {
        this._fileDownloadService.downloadTempFile(result);
      });
  }
  lamtuoi() {

    // this.viTriUngTuyenCodeFilter = "";
    // this.kenhTuyenDungCodeFilter = "";
    // this.gioiTinhCodeFilter = "";
    // this.tienDoTuyenDungCodeFilter = "";
    // this.trangThaiCodeFilter = "";
    // this.startDateFilter = null;
    // this.endDateFilter = null;
    // this.isCheckTimeFilter = false;
    // this.day1Filter = null;
    // this.day3Filter = null;
    // this.day2Filter = null;
    this.filterText = "";
    this.minNgayKyFilter = this.fromDate;
    this.maxNgayKyFilter = this.fromDate;
    this.hinhThucLamViecCodeFilter = "";
  }
}

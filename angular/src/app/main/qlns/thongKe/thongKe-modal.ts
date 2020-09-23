import { Component, Injector, ViewEncapsulation, ViewChild, SecurityContext, Input, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import * as _ from 'lodash';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { EventEmitter } from 'events';
import { HttpClient } from '@angular/common/http';
import { UtilityService } from '@shared/utils/UltilityService.service';
import { DatePipe } from '@angular/common';
import {formatDate} from '@angular/common';
import $ from 'jquery';
import { DxFormComponent, DxDataGridComponent, DxButtonComponent, DxSwitchComponent, DxNumberBoxComponent, DxDateBoxComponent, DxSelectBoxComponent, DxFileUploaderComponent } from 'devextreme-angular';
import { DynamicModuleComponent } from '@app/shared/common/dynamicModule/dynamicModule.component';
import { DomSanitizer } from '@angular/platform-browser';
import { DxiGroupComponent, DxoPopupComponent, DxoColCountByScreenModule } from 'devextreme-angular/ui/nested';
import { Location } from '@angular/common';
import { UngViensServiceProxy, TruongGiaoDichDto, CreateOrEditHoSoDto, HoSosServiceProxy } from '@shared/service-proxies/service-proxies';
//  import ExcelJS from 'exceljs';
//  import saveAs from 'file-saver';
 import { exportDataGrid } from 'devextreme/exporter';

@Component({
    selector: 'thongKeModal',
    templateUrl: './thongKe-modal.html',
    // styleUrls: ['./create-new-incomming-document.less'],
    encapsulation: ViewEncapsulation.None,
    providers: [DatePipe] ,
    animations: [appModuleAnimation()]

})
export class ThongKeModalComponent extends AppComponentBase {
    @ViewChild('printDocumentForm', { static: true}) printDocumentForm: DxFormComponent;
    @ViewChild('typeExport', { static: true}) typeExport: DxSelectBoxComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('fileUploader1', { static: false }) fileUploader1: DxFileUploaderComponent;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    data_typeExport = [
        {key: 1, type: 'Ngày'},
        {key: 2, type: 'Tháng'},
        {key: 3, type: 'Năm'}
        // {key: 4, type: 'Năm'}
    ];

    months = [
        {key: 1, value: 'Tháng 1'},
        {key: 2, value: 'Tháng 2'},
        {key: 3, value: 'Tháng 3'},
        {key: 4, value: 'Tháng 4'},
        {key: 5, value: 'Tháng 5'},
        {key: 6, value: 'Tháng 6'},
        {key: 7, value: 'Tháng 7'},
        {key: 8, value: 'Tháng 8'},
        {key: 9, value: 'Tháng 9'},
        {key: 10, value: 'Tháng 10'},
        {key: 11, value: 'Tháng 11'},
        {key: 12, value: 'Tháng 12'},
    ];

    // precious = [
    //     {key: 1, value: 'Quý 1'},
    //     {key: 2, value: 'Quý 2'},
    //     {key: 3, value: 'Quý 3'},
    //     {key: 4, value: 'Quý 4'}
    // ];

    years = [
      {key: 2012, value: '2012'},
      {key: 2013, value: '2013'},
      {key: 2014, value: '2014'},
      {key: 2015, value: '2015'},
      {key: 2016, value: '2016'},
      {key: 2017, value: '2017'},
      {key: 2018, value: '2018'},
      {key: 2019, value: '2019'},
      {key: 2020, value: '2020'},
  
    ];
   

    printUrl:any;
    advancedFiltersAreShown = false;
    filterText = '';
    maNhanVienFilter = '';
    hoVaTenFilter = '';
    anhDaiDienFilter = '';
    gioiTinhCodeFilter = '';
    maxNgaySinhFilter : moment.Moment;
		minNgaySinhFilter : moment.Moment;
    mstCaNhanFilter = '';
    maxDonViCongTacIDFilter : number;
		maxDonViCongTacIDFilterEmpty : number;
		minDonViCongTacIDFilter : number;
		minDonViCongTacIDFilterEmpty : number;
    viTriCongViecCodeFilter = '';
    danTocFilter = '';
    tonGiaoFilter = '';
    quocTichFilter = '';
    soCMNDFilter = '';
    loaiHopDongIDFilter: number;
    maxNgayCapFilter : moment.Moment;
		minNgayCapFilter : moment.Moment;
    noiCapFilter = '';
    maxNgayHetHanFilter : moment.Moment;
		minNgayHetHanFilter : moment.Moment;
    trinhDoVanHoaFilter = '';
    trinhDoDaoTaoCodeFilter = '';
    noiDaoTaoIDFilter : number;
    maxNoiDaoTaoCodeFilter : number;
    maxNoiDaoTaoCodeFilterEmpty : number;
    minNoiDaoTaoCodeFilter : number;
    // noiDaoTaoCodeFilter = '';
    khoaFilter = '';
    chuyenNganhFilter = '';
    maxNamTotNghiepFilter : number;
		maxNamTotNghiepFilterEmpty : number;
		minNamTotNghiepFilter : number;
		minNamTotNghiepFilterEmpty : number;
    xepLoaiCodeFilter = '';
    tinhTrangHonNhanCodeFilter = '';
    tepDinhKemFilter = '';
    dtDiDongFilter = '';
    dtCoQuanFilter = '';
    dtNhaRiengFilter = '';
    dtKhacFilter = '';
    emailCaNhanFilter = '';
    emailCoQuanFilter = '';
    emailKhacFilter = '';
    nguyenQuanFilter = '';
    maxTinhThanhIDFilter : number;
		maxTinhThanhIDFilterEmpty : number;
		minTinhThanhIDFilter : number;
		minTinhThanhIDFilterEmpty : number;
    noiSinhFilter = '';
    skypeFilter = '';
    facebookFilter = '';
    quocGiaHKTTFilter = '';
    maxTinhThanhIDHKTTFilter : number;
		maxTinhThanhIDHKTTFilterEmpty : number;
		minTinhThanhIDHKTTFilter : number;
		minTinhThanhIDHKTTFilterEmpty : number;
    diaChiHKTTFilter = '';
    soSoHoKhauFilter = '';
    maSoHoGiaDinhFilter = '';
    laChuHoFilter = -1;
    quocGiaHNFilter = '';
    maxTinhThanhIDHNFilter : number;
		maxTinhThanhIDHNFilterEmpty : number;
		minTinhThanhIDHNFilter : number;
		minTinhThanhIDHNFilterEmpty : number;
    diaChiHNFilter = '';
    hoVaTenLHKCFilter = '';
    quanHeLHKCFilter = '';
    dtDiDongLHKCFilter = '';
    dtNhaRiengLHKCFilter = '';
    emailLHKCFilter = '';
    diaChiLHKCFilter = '';
    maChamCongFilter = '';
    chucDanhFilter = '';
    capFilter = '';
    bacFilter = '';
    trangThaiLamViecCodeFilter = '';
    quanLyTrucTiepFilter = '';
    currentDate: Date;
    quanLyGianTiepFilter = '';
    diaDiemLamViecCodeFilter = '';
    minLoaiHopDongCodeFilterEmpty;
    soSoQLLaoDongFilter = '';
    loaiHopDongCodeID : number;
    maxloaiHopDongCodeFilter : number;
    minloaiHopDongCodeFilter : number;
    maxloaiHopDongCodeFilterEmpty : number;
    minNoiDaoTaoCodeFilterEmpty: number ;
    // loaiHopDongCodeFilter = '';
    maxNgayTapSuFilter : moment.Moment;
		minNgayTapSuFilter : moment.Moment;
    maxNgayThuViecFilter : moment.Moment;
		minNgayThuViecFilter : moment.Moment;
    maxNgayChinhThucFilter : moment.Moment;
		minNgayChinhThucFilter : moment.Moment;
    maxSoNgayPhepFilter : number;
		maxSoNgayPhepFilterEmpty : number;
		minSoNgayPhepFilter : number;
		minSoNgayPhepFilterEmpty : number;
    bacLuongCodeFilter = '';
    maxLuongCoBanFilter : number;
		maxLuongCoBanFilterEmpty : number;
    minLuongCoBanFilter : number;
    maxLoaiHopDongCodeFilterEmpty;
		minLuongCoBanFilterEmpty : number;
    maxLuongDongBHFilter : number;
		maxLuongDongBHFilterEmpty : number;
		minLuongDongBHFilter : number;
		minLuongDongBHFilterEmpty : number;
    maxSoCongChuanFilter : number;
		maxSoCongChuanFilterEmpty : number;
		minSoCongChuanFilter : number;
		minSoCongChuanFilterEmpty : number;
    donViSoCongChuanCodeFilter = '';
    tkNganHangFilter = '';
    nganHangCodeFilter = '';
    thamGiaCongDoanFilter = -1;
    maxNgayThamGiaBHFilter : moment.Moment;
		minNgayThamGiaBHFilter : moment.Moment;
    maxTyLeDongBHFilter : number;
		maxTyLeDongBHFilterEmpty : number;
        minTyLeDongBHFilter : number;
        listDataHoSo:any;
		minTyLeDongBHFilterEmpty : number;
    soSoBHXHFilter = '';
    maSoBHXHFilter = '';
    maTinhCapFilter = '';
    soTheBHYTFilter = '';
    maxNgayHetHanBHYTFilter : moment.Moment;
		minNgayHetHanBHYTFilter : moment.Moment;
    maxNoiDangKyKCBIDFilter : number;
		maxNoiDangKyKCBIDFilterEmpty : number;
		minNoiDangKyKCBIDFilter : number;
		minNoiDangKyKCBIDFilterEmpty : number;
    maSoNoiKCBFilter = '';
    autH_STATUSFilter = '';
    recorD_STATUSFilter = '';
    maxLoaiHopDongCodeFilter;
    hoSo: CreateOrEditHoSoDto = new CreateOrEditHoSoDto();
    maxMARKER_IDFilter : number;
		maxMARKER_IDFilterEmpty : number;
		minMARKER_IDFilter : number;
		minMARKER_IDFilterEmpty : number;
    maxCHECKER_IDFilter : number;
		maxCHECKER_IDFilterEmpty : number;
		minCHECKER_IDFilter : number;
		minCHECKER_IDFilterEmpty : number;
    maxAPPROVE_DTFilter : moment.Moment;
    minAPPROVE_DTFilter : moment.Moment;
    minLoaiHopDongCodeFilter;
    maxDay1Filter: moment.Moment;
    minDay1Filter: moment.Moment;
    maxDay2Filter: moment.Moment;
    minDay2Filter: moment.Moment;
    maxDay3Filter: moment.Moment;
    minDay3Filter: moment.Moment;


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


    soCongChuanFilter = '';
    luongDongBHFilter = '';
    luongCoBanFilter = '';
   
    tenCtyFilter = '';
    hopDongHienTaiFilter = '';


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

    tenFile  :any; 
    dataDisplay: any;
    selectedRows = [];
    currentTime: any;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    loaiHopDong: TruongGiaoDichDto[] = [];
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    urlExport = '';
    typeValue: number;
    typeValue1: number;
    fromLabel = '';
    toLabel = '';
    data_fromSelect = [];
    data_toSelect = [];
    fromValue: any;
    toValue: any;
    congTY:any;
    printDocument: any;
    listYear:any;
    tenCTY:any;
    selectedName :any;
    constructor(
        injector: Injector,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
    ){
        super(injector);
        this.currentDate = new Date();
      
        this.urlExport = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/GetExcelFile?';

    }

    getHoSos(event?: LazyLoadEvent) {
      if (this.primengTableHelper.shouldResetPaging(event)) {
          this.paginator.changePage(0);
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
         10
    ).subscribe(result => {
        this.primengTableHelper.totalRecordsCount = result.totalCount;
  
        let count = 0;
  
        this.listDataHoSo = result.items;
        for (var i = 0, len = this.listDataHoSo.length; i < len; i++) {
            this.listDataHoSo[i]["stt"] = ++count;
        }
        console.log(this.listDataHoSo);
        this.primengTableHelper.records = this.listDataHoSo;
        
  
    });
  }
  
        restart()
        {
         this.getHoSos();  
        }
    ngOnInit(){
        this.getHoSos();  
        this.typeValue = 1;
        this.typeValue1 = 2;
        this.fromLabel = 'Từ ngày';
        this.toLabel = 'Đến ngày';
        $('#toValue').dxDateBox({
            text: 'Từ ngày',
            displayFormat: 'dd/MM/yyyy',
            value: this.currentDate
        });
        $('#fromValue').dxDateBox({
            text: 'Từ ngày',
            displayFormat: 'dd/MM/yyyy',
            value: this.addDays(this.currentDate, -7)
        });

        this.getAllTruongGiaoDich().subscribe((res) => {
          this.truongGiaoDich = res;
          this.congTY= this.truongGiaoDich.filter(x => x.code == 'CT');      
          console.log( this.congTY)
      }); 
    }
    getDaTa(e:any)
    {
      console.log(e)

      this.tenCTY = e.value; 
      // this._hoSosServiceProxy.getListNhanVienToCty(e.value).subscribe(res =>{
      //   this.listDataHoSo= res;
      //   console.log( this.listDataHoSo)
      // })
    }

    addDays(val,days: number){
        var date = new Date(val);
        date.setDate(date.getDate()+ days);
        return date;
    }

    typeChangedOption(e: any){
        switch(e.value){
            case 1:
                this.fromLabel = 'Từ ngày';
                this.toLabel = 'Đến ngày';
                $('#toValueDiv').show();
                $('#firstElement').children()[0].remove();
                var $newdiv1 = $("<div>", {id:'fromValue'});
                $("#firstElement").append($newdiv1);
                $('#secondElement').children()[0].remove();
                var $newdiv2 = $("<div>", {id:'toValue'});
                $("#secondElement").append( $newdiv2);
                $('#toValue').dxDateBox({
                    text: 'Đến ngày',
                    type: 'date',
                    displayFormat: 'dd/MM/yyyy',
                    value: this.currentDate
                });
                $('#fromValue').dxDateBox({
                    text: 'Từ ngày',
                    type: 'date',
                    displayFormat: 'dd/MM/yyyy',
                    value: this.addDays(this.currentDate, -7)
                });
                $('#toValueDiv').show();
                break;
            case 2:
                this.fromLabel = 'Chọn tháng';
                this.toLabel = 'Chọn năm';
                $('#toValueDiv').show();
                $('#firstElement').children()[0].remove();
                var $newdiv1 = $("<div>", {id:'fromValue'});
                $("#firstElement").append($newdiv1);
                $('#secondElement').children()[0].remove();
                var $newdiv2 = $("<div>", {id:'toValue'});
                $("#secondElement").append( $newdiv2);
                $('#fromValue').dxSelectBox({
                    text: 'Tháng',
                    dataSource: this.months,
                    valueExpr: 'key',
                    displayExpr: 'value',
                    value: this.currentDate.getMonth()
                });
                $('#toValue').dxSelectBox({
                    text: 'Năm',
                    valueExpr: 'key',
                    displayExpr: 'value',
                    dataSource: this.years,
                    value: this.currentDate.getFullYear()
                });
                
                break;
            case 3:

              $('#firstElement').children()[0].remove();
              var $newdiv1 = $("<div>", {id:'fromValue'});
              $("#firstElement").append($newdiv1);
              this.fromLabel = 'Chọn năm';
              $('#fromValue').dxSelectBox({
                  text: 'Năm',
                  valueExpr: 'key',
                  displayExpr: 'value',
                  dataSource: this.years,
                  value: this.currentDate.getFullYear()
              });
              $('#toValueDiv').hide();
              break;
                // $('#toValueDiv').show();
                // $('#firstElement').children()[0].remove();
                // var $newdiv1 = $("<div>", {id:'fromValue'});
                // $("#firstElement").append($newdiv1);
                // $('#secondElement').children()[0].remove();
                // var $newdiv2 = $("<div>", {id:'toValue'});
                // $("#secondElement").append( $newdiv2);
                // this.fromLabel = 'Chọn quý';
                // this.toLabel = 'Chọn năm';
                // $('#fromValue').dxSelectBox({
                //     text: 'Quý',
                //     dataSource: this.precious,
                //     valueExpr: 'key',
                //     displayExpr: 'value'
                // });
                // $('#toValue').dxSelectBox({
                //     text: 'Năm',
                //     valueExpr: 'key',
                //     displayExpr: 'value',
                //     dataSource: this.years,
                //     value: this.currentDate.getFullYear()
                // });
                
                // break;

            // case 4:
                // $('#firstElement').children()[0].remove();
                // var $newdiv1 = $("<div>", {id:'fromValue'});
                // $("#firstElement").append($newdiv1);
                // this.fromLabel = 'Chọn năm';
                // $('#fromValue').dxSelectBox({
                //     text: 'Năm',
                //     valueExpr: 'key',
                //     displayExpr: 'value',
                //     dataSource: this.years,
                //     value: this.currentDate.getFullYear()
                // });
                // $('#toValueDiv').hide();
                // break;
         }
    }
    public highlightRow(emp) {
      this.selectedName = emp.hoSo.id;
    } 
    locDuLieu(){
        var type = this.typeExport.value;
       console.log( this.tenCTY)
        switch(type){
            case 1:
                let fd = $('#fromValue').dxDateBox('instance').option('value');
                let td = $('#toValue').dxDateBox('instance').option('value');
                var fromDate =  moment(fd);
                var toDate = moment(td);
                this.tenFile  = "BaocaoTuNgayDenNgay.xlsx"
                if(toDate.diff(fromDate, 'days') >= 0){
                    this.makeRequest(fromDate.format('YYYY-MM-DD'), toDate.format('YYYY-MM-DD'), this.tenFile ,this.tenCTY);
                }
                else{
                    this.makeRequest(toDate.format('YYYY-MM-DD'), fromDate.format('YYYY-MM-DD'),this.tenFile ,this.tenCTY);
                }
                break;
            case 2:
                let month = $('#fromValue').dxSelectBox('instance').option('value');
                let year = $('#toValue').dxSelectBox('instance').option('value');
                const startOfMonth = moment(year + '-' + month + '-01').startOf('month').format('YYYY-MM-DD');
                const endOfMonth   = moment(year + '-' + month + '-01').endOf('month').format('YYYY-MM-DD');
                this.tenFile  = "BaocaoThang.xlsx"
                this.makeRequest(startOfMonth, endOfMonth,this.tenFile ,this.tenCTY);
                break;
            case 3:
              

              let year4 = $('#fromValue').dxSelectBox('instance').option('value');
              let start1 = moment(year4 + '-01-01').startOf('year').format('YYYY-MM-DD');
              let end2   = moment(year4 + '-01-01').endOf('year').format('YYYY-MM-DD');
              this.tenFile  = "BaocaoNam.xlsx"
              this.makeRequest(start1, end2 ,this.tenFile ,this.tenCTY);
              break;

                // let precious = $('#fromValue').dxSelectBox('instance').option('value');
                // let year2 = $('#toValue').dxSelectBox('instance').option('value');
                // var start = '';
                // var end = '';
                // this.tenFile  = "BaocaoQuy.xlsx"
                // switch(precious){
                //     case 1:
                //         start = moment(year2 + '-01-01').startOf('month').format('YYYY-MM-DD');
                //         end   = moment(year2 + '-03-01').endOf('month').format('YYYY-MM-DD');
                //         console.log(start, end);

                //         break;
                //     case 2:
                //         start = moment(year2 + '-04-01').startOf('month').format('YYYY-MM-DD');
                //         end   = moment(year2 + '-06-01').endOf('month').format('YYYY-MM-DD');
                //         console.log(start, end);
                //         break;
                //     case 3:
                //         start = moment(year2 + '-07-01').startOf('month').format('YYYY-MM-DD');
                //         end   = moment(year2 + '-09-01').endOf('month').format('YYYY-MM-DD');
                //         console.log(start, end);
                //         break;
                //     case 4:
                //         start = moment(year2 + '-10-01').startOf('month').format('YYYY-MM-DD');
                //         end   = moment(year2 + '-12-01').endOf('month').format('YYYY-MM-DD');
                //         console.log(start, end);
                //         break;
                //     }
                // this.makeRequest(start, end ,this.tenFile);
                // break;
                // case 4:
                    // let year4 = $('#fromValue').dxSelectBox('instance').option('value');
                    // let start1 = moment(year4 + '-01-01').startOf('year').format('YYYY-MM-DD');
                    // let end2   = moment(year4 + '-01-01').endOf('year').format('YYYY-MM-DD');
                    // this.tenFile  = "BaocaoNam.xlsx"
                    // this.makeRequest(start1, end2 ,this.tenFile);
                    // break;
        
             
        }
    }

     xuatDuLieu(){
      var type = this.typeExport.value;
      switch(type){
          case 1:
              let fd = $('#fromValue').dxDateBox('instance').option('value');
              let td = $('#toValue').dxDateBox('instance').option('value');
              var fromDate =  moment(fd);
              var toDate = moment(td);
              this.tenFile  = "BaocaoTuNgayDenNgay.xlsx"
              if(toDate.diff(fromDate, 'days') >= 0){
                  this.makeRequest1(fromDate.format('YYYY-MM-DD'), toDate.format('YYYY-MM-DD'), this.tenFile ,this.tenCTY);
              }
              else{
                  this.makeRequest1(toDate.format('YYYY-MM-DD'), fromDate.format('YYYY-MM-DD'),this.tenFile ,this.tenCTY);
              }
              break;
          case 2:
              let month = $('#fromValue').dxSelectBox('instance').option('value');
              let year = $('#toValue').dxSelectBox('instance').option('value');
              const startOfMonth = moment(year + '-' + month + '-01').startOf('month').format('YYYY-MM-DD');
              const endOfMonth   = moment(year + '-' + month + '-01').endOf('month').format('YYYY-MM-DD');
              this.tenFile  = "BaocaoThang.xlsx"
              this.makeRequest1(startOfMonth, endOfMonth,this.tenFile,this.tenCTY );
              break;
          case 3:
            

            let year4 = $('#fromValue').dxSelectBox('instance').option('value');
            let start1 = moment(year4 + '-01-01').startOf('year').format('YYYY-MM-DD');
            let end2   = moment(year4 + '-01-01').endOf('year').format('YYYY-MM-DD');
            this.tenFile  = "BaocaoNam.xlsx"
            this.makeRequest1(start1, end2 ,this.tenFile,this.tenCTY);
            break;

              // let precious = $('#fromValue').dxSelectBox('instance').option('value');
              // let year2 = $('#toValue').dxSelectBox('instance').option('value');
              // var start = '';
              // var end = '';
              // this.tenFile  = "BaocaoQuy.xlsx"
              // switch(precious){
              //     case 1:
              //         start = moment(year2 + '-01-01').startOf('month').format('YYYY-MM-DD');
              //         end   = moment(year2 + '-03-01').endOf('month').format('YYYY-MM-DD');
              //         console.log(start, end);

              //         break;
              //     case 2:
              //         start = moment(year2 + '-04-01').startOf('month').format('YYYY-MM-DD');
              //         end   = moment(year2 + '-06-01').endOf('month').format('YYYY-MM-DD');
              //         console.log(start, end);
              //         break;
              //     case 3:
              //         start = moment(year2 + '-07-01').startOf('month').format('YYYY-MM-DD');
              //         end   = moment(year2 + '-09-01').endOf('month').format('YYYY-MM-DD');
              //         console.log(start, end);
              //         break;
              //     case 4:
              //         start = moment(year2 + '-10-01').startOf('month').format('YYYY-MM-DD');
              //         end   = moment(year2 + '-12-01').endOf('month').format('YYYY-MM-DD');
              //         console.log(start, end);
              //         break;
              //     }
              // this.makeRequest(start, end ,this.tenFile);
              // break;
              // case 4:
                  // let year4 = $('#fromValue').dxSelectBox('instance').option('value');
                  // let start1 = moment(year4 + '-01-01').startOf('year').format('YYYY-MM-DD');
                  // let end2   = moment(year4 + '-01-01').endOf('year').format('YYYY-MM-DD');
                  // this.tenFile  = "BaocaoNam.xlsx"
                  // this.makeRequest(start1, end2 ,this.tenFile);
                  // break;
      
           
      }
  }
  

  makeRequest(fromDate: string, toDate: string , tenFile :string , tenCTY:string ){

    this._hoSosServiceProxy.getListNhanVienToDataFromDate(fromDate ,toDate ,tenCTY).subscribe(res =>{
      console.log(res);
      this.listDataHoSo = res;
  })

  
 
}
    makeRequest1(fromDate: string, toDate: string , tenFile :string  , tenCTY:string){
      

      $.ajax({
        url: this.urlExport + 'FromDate=' + fromDate +'&ToDate=' + toDate+ '&tenFile='+tenFile +'&tenCTY='+tenCTY,
        method: 'POST',
        xhrFields: {
            responseType: 'blob'
        },

        success: function (data) {
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(data);
            a.href = url;
            a.download = tenFile;
            document.body.append(a);
            a.click();
            a.remove();
            window.URL.revokeObjectURL(url);
        }

    });
     
    }

    getQuarter(d) {
        d = d || new Date();
        var m = Math.floor(d.getMonth()/3) + 2;
        return m > 4? m - 4 : m;
      }


      
}
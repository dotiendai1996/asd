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
import { DxFormComponent, DxDataGridComponent, DxButtonComponent, DxSwitchComponent, DxNumberBoxComponent, DxDateBoxComponent, DxSelectBoxComponent } from 'devextreme-angular';
import { DynamicModuleComponent } from '@app/shared/common/dynamicModule/dynamicModule.component';
import { DomSanitizer } from '@angular/platform-browser';
import { DxiGroupComponent, DxoPopupComponent } from 'devextreme-angular/ui/nested';
import { Location } from '@angular/common';
import { UngViensServiceProxy, TruongGiaoDichDto, CreateOrEditHoSoDto, HoSosServiceProxy, TemplatesServiceProxy, TemplateDto } from '@shared/service-proxies/service-proxies';
//  import ExcelJS from 'exceljs';
//  import saveAs from 'file-saver';
 import { exportDataGrid } from 'devextreme/exporter';
 import { DxDataGridModule ,DxSelectBoxModule } from 'devextreme-angular';

@Component({
    selector: 'templateHDModal',
    templateUrl: './templateHD-modal.html',
    // styleUrls: ['./create-new-incomming-document.less'],
    encapsulation: ViewEncapsulation.None,
    providers: [DatePipe] ,
    animations: [appModuleAnimation()]

})
export class TemplateHDModalComponent extends AppComponentBase {
    @ViewChild('printDocumentForm', { static: true}) printDocumentForm: DxFormComponent;
    @ViewChild('typeExport', { static: true}) typeExport: DxSelectBoxComponent;

    printUrl:any;
    advancedFiltersAreShown = false;
    filterText = '';
    value: any[] = [];
    minDay3Filter: moment.Moment;
    tenFile  :any; 
    dataDisplay: any;
    selectedRows = [];
    currentTime: any;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    loaiHopDong: TemplateDto[] = [];
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
    fromLabel = '';
    toLabel = '';
    data_fromSelect = [];
    data_toSelect = [];
    rootUrl: string;
    tenLoaiHD: any;
    hopDong: any;
    selected:any;
    tenHD:any; 
    link:any;
    maHD:any;
    printDocument: any;
    listYear:any;
    congTY:any ; 
    currentDate:any; 
    popupVisibleLHD = false ;
    uploadUrl:any;
    uploadUrlImage:any;
    cty:any;
    nameHD:any; 
    maTemplate:any;
    constructor(
        injector: Injector,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _templatesServiceProxy: TemplatesServiceProxy,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
    ){
        super(injector);
        
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/UploadHĐ?path1=' + this.selected +'&currentTime'+this.currentTime;
      
       
    }
   
     
    ngOnInit(){
     this.show();   
    }

    uploadImage(e:any) {

    console.log(this.selected);
    console.log(this.maTemplate);
    this.tenHD = e.file.name;
    console.log(  this.tenHD);
    this.link = "Template/"+this.selected+"/"+this.currentTime+"/"+ this.tenHD; 
    console.log( this.link)
 this._templatesServiceProxy.thayDoiLink( this.link, this.maTemplate).subscribe(res =>{
    this._templatesServiceProxy.getListTemplate( this.selected).subscribe(res =>{

        this.loaiHopDong= res;
        let count = 0;
    
        for(var i = 0, len =  this.loaiHopDong.length; i < len; i++){
            this.loaiHopDong[i]["stt"] = ++count;      
            console.log( this.loaiHopDong)
        }
      })
 })
    
    } 
  
    taiVe(e:any )
    {
        console.log(e)
        this.maTemplate=e.data.maTemplate;
        this.tenHD = e.data.linkTemplate;
        console.log(this.tenHD)
        this.link = this.rootUrl + '/'+this.tenHD;
        console.log(this.link)
        window.open(this.link, '_blank');
    }
 
    chonLoaiHopDong(e:any)
    {
        console.log(e)
        this.hopDong.loaiHopDongCode = e.data.value;
        this.tenLoaiHD= this.hopDong.loaiHopDongCode;
       this.maHD = e.data.cdName ; 
        this.popupVisibleLHD = false;
    }
 
    onValueChanged (e) {
       
          this.selected =  e.value ; 
          this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
          this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/UploadHĐ?path1=' + this.selected +'&currentTime='+this.currentTime;
          console.log( this.uploadUrl)

          this._templatesServiceProxy.getListTemplate( this.selected).subscribe(res =>{

            this.loaiHopDong= res;
            let count = 0;
        
            for(var i = 0, len =  this.loaiHopDong.length; i < len; i++){
                this.loaiHopDong[i]["stt"] = ++count;      
                console.log( this.loaiHopDong)
            }
          })
     
      }
    show(): void {

       
        this.getAllTruongGiaoDich().subscribe((res) => {
            this.truongGiaoDich = res;
            this.congTY= this.truongGiaoDich.filter(x => x.code == 'CT');      

        }); 

    }
     
}


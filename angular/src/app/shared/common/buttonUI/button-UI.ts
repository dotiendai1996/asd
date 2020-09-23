import { Component, Injector, ViewEncapsulation, ViewChild, EventEmitter, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
// import {  } from '@shared/service-proxies/service-proxies';
// import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
// import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import * as _ from 'lodash';
import * as moment from 'moment';
import { DocumentsDto, KeywordDetailsServiceProxy, MenusServiceProxy, DynamicActionsServiceProxy } from '@shared/service-proxies/service-proxies';
import RemoteFileProvider from 'devextreme/ui/file_manager/file_provider/remote';
import { AppConsts } from '@shared/AppConsts';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxFileManagerModule, DxPopupModule } from 'devextreme-angular';
import { BrowserModule } from '@angular/platform-browser';
import { NgForm } from '@angular/forms';
import { DxTextBoxModule, DxFileUploaderModule, DxButtonModule } from 'devextreme-angular';
import { from } from 'rxjs';
import * as $ from 'jquery';



@Component({
    selector: 'buttonUI',
    templateUrl: './button-UI.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]

})
export class ButtonUIComponent extends AppComponentBase {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @Input() labelId = '';
    @Input() dataSource : any;
    documentData: DocumentsDto;
    dataGrid: any;
    selectedRows: number[];

    constructor(
        injector: Injector,
        private router : Router,
        // private _notifyService: NotifyService,
        // private _tokenAuth: TokenAuthServiceProxy,
        // private service: MenusServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _dynamicAction: DynamicActionsServiceProxy
    ) {
        super(injector);
    }

    getLabelId(id: string){
        this.labelId = id;
        console.log(id);
    }

    data: any;

    ngOnInit(){
        console.log(this.labelId);
        this._dynamicAction.getDynamicActionByLabelId(parseInt(this.labelId)).subscribe(res => {
            if(res != null){
                this.data = res;
            }
        });
    }

   
    click1()
    {
        this.router.navigate(['/app/main/qlvb/vanban-InProcess']); 
    }


    save(): void {
            // this.saving = true;

			
            // this._sqlConfigDetailsServiceProxy.createOrEdit(this.sqlConfigDetail)
            //  .pipe(finalize(() => { this.saving = false;}))
            //  .subscribe(() => {
            //     this.notify.info(this.l('SavedSuccessfully'));
            //     this.close();
            //     this.modalSave.emit(null);
            //  });

            
    }

    transfer(id: number[]){
        console.log(this.dataSource);
    }
}
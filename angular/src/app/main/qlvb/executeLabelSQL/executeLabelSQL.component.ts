import { Component, Injector, OnInit, Output, Input, ViewChild, EventEmitter } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CachingServiceProxy, EntityDtoOfString, WebLogServiceProxy, AuditLogServiceProxy, SqlConfigDetailsServiceProxy, VanbansServiceProxy, LabelsServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { UtilityService } from '@shared/utils/UltilityService.service';
import { Router } from '@angular/router';
import { DxDataGridComponent } from 'devextreme-angular';
import { SqlConfigHelperService } from '@shared/utils/sql-config-helper.service';
import { ButtonUIComponent } from '@app/shared/common/buttonUI/button-UI';

@Component({
    templateUrl: './executeLabelSQL.component.html',
    styleUrls: ['./executeLabelSQL.component.less'],
    animations: [appModuleAnimation()]
})
export class ExecuteLabelSQLComponent extends AppComponentBase implements OnInit {
    // @Input() rawSql: string;
    @ViewChild(DxDataGridComponent, { static: false }) gridContainer: DxDataGridComponent;
    @ViewChild(ButtonUIComponent, { static: true }) private buttonUI: ButtonUIComponent;
    // @Output() labelId: EventEmitter<number> = new EventEmitter<number>();
    
    loading = false;
    caches: any = null;
    logs: any = '';
    // rawSql = '';
    tableHeader = [];
    queryResult = [];
    dataFieldKey = [];
    rawSql: string;
    header: string;
    selectionChangedBySelectbox: boolean;
    prefix = '';
    initialData: any;
    labelId: string;

    constructor(
        injector: Injector,
        private _cacheService: CachingServiceProxy,
        private _webLogService: WebLogServiceProxy,
        private _auditLog: AuditLogServiceProxy,
        private ultility: UtilityService,
        private router: Router,
        private _sqlConfigHelperService: SqlConfigHelperService,
        private _sqlConfigDetailsService: SqlConfigDetailsServiceProxy,
        private _labelServiceProxy: LabelsServiceProxy,
        private _fileDownloadService: FileDownloadService) {
        super(injector);
        this.labelId = sessionStorage.getItem('labelId');
    }

    

    ngOnInit(){
        if(sessionStorage.getItem('rawSql') == undefined)
        {
            this.router.navigate(['/app/main/qlvb/not-processed-yet']);
        }
        console.log(this.labelId)
        // this.buttonUI.getLabelId(this.labelId);
        // this.labelId.emit(parseInt(sessionStorage.getItem('labelId')));
        this.rawSql = sessionStorage.getItem('rawSql');
        this.header = sessionStorage.getItem('labelHeader');
        console.log(this.rawSql);
        this.getVanbanDxTable(this.rawSql);
    }

    selectionChangedHandler() {
        if(!this.selectionChangedBySelectbox) {
            this.prefix = null;
        }
        
        this.selectionChangedBySelectbox=false;
    }
    columnFields: any[] = [];
    getVanbanDxTable(code: string) {
        console.log(code);
        this._labelServiceProxy.getAllDataAndColumnConfig(code).subscribe(result => {
            this.initialData = result.listVanBan;
            console.log( this.initialData);
            this.gridContainer.dataSource = this.initialData;
            this.gridContainer.columns = this._sqlConfigHelperService.generateColumns(result.listColumnConfig, true);
            console.log(this.gridContainer.columns);
        });
    }

    selectedRows: number[] = [];
    selectedRowsData: [] = [];

    showModalTransfer(){
        // this.transferHandleModal.show(this.selectedRows);
        this.ultility.selectedRows = this.selectedRows;
        console.log(this.selectedRows);
        // sessionStorage.setItem('selectedRows', JSON.stringify(this.selectedRows));
        this.router.navigate(['/app/main/qlvb/transfer-handle']);
    }

    transfer(e: any){
        debugger;
        if(this.selectedRowsData.length == 0){
            return;
        }
        this.selectedRowsData.forEach(element => {
            this.selectedRows.push(element["Id"]);
        });
        // this.selectedRows.filter(x => typeof(x) == "number");
        this.showModalTransfer();
    }
}
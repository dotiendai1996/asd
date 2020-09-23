import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';

import { QuanLyNghiPhepsComponent } from './qlns/quanLyNghiPheps/quanLyNghiPheps.component';
import { ViewQuanLyNghiPhepModalComponent } from './qlns/quanLyNghiPheps/view-quanLyNghiPhep-modal.component';





import { LichSuUploadsComponent } from './qlns/lichSuUploads/lichSuUploads.component';
import { ViewLichSuUploadModalComponent } from './qlns/lichSuUploads/view-lichSuUpload-modal.component';
import { CreateOrEditLichSuUploadModalComponent } from './qlns/lichSuUploads/create-or-edit-lichSuUpload-modal.component';


import { ViTriCongViecsComponent } from './qlns/viTriCongViecs/viTriCongViecs.component';
import { ViewViTriCongViecModalComponent } from './qlns/viTriCongViecs/view-viTriCongViec-modal.component';
import { CreateOrEditViTriCongViecModalComponent } from './qlns/viTriCongViecs/create-or-edit-viTriCongViec-modal.component';

import { ConfigEmailsComponent } from './qlns/configEmails/configEmails.component';
import { ViewConfigEmailModalComponent } from './qlns/configEmails/view-configEmail-modal.component';
import { CreateOrEditConfigEmailModalComponent } from './qlns/configEmails/create-or-edit-configEmail-modal.component';

import { TemplatesComponent } from './qlns/templates/templates.component';
import { ViewTemplateModalComponent } from './qlns/templates/view-template-modal.component';
import { CreateOrEditTemplateModalComponent } from './qlns/templates/create-or-edit-template-modal.component';


import { StoreDatasourcesComponent } from './qlvb/storeDatasources/storeDatasources.component';
import { ViewStoreDatasourceModalComponent } from './qlvb/storeDatasources/view-storeDatasource-modal.component';
import { CreateOrEditStoreDatasourceModalComponent } from './qlvb/storeDatasources/create-or-edit-storeDatasource-modal.component';

import { HardDatasourcesComponent } from './qlvb/hardDatasources/hardDatasources.component';
import { ViewHardDatasourceModalComponent } from './qlvb/hardDatasources/view-hardDatasource-modal.component';
import { CreateOrEditHardDatasourceModalComponent } from './qlvb/hardDatasources/create-or-edit-hardDatasource-modal.component';

import { CommandDatasourcesComponent } from './qlvb/commandDatasources/commandDatasources.component';
import { ViewCommandDatasourceModalComponent } from './qlvb/commandDatasources/view-commandDatasource-modal.component';
import { CreateOrEditCommandDatasourceModalComponent } from './qlvb/commandDatasources/create-or-edit-commandDatasource-modal.component';

import { DynamicDatasourceComponent } from './qlvb/dynamicDatasource/dynamicDatasource.component';
import { ViewDynamicDatasourceModalComponent } from './qlvb/dynamicDatasource/view-dynamicDatasource-modal.component';
import { CreateOrEditDynamicDatasourceModalComponent } from './qlvb/dynamicDatasource/create-or-edit-dynamicDatasource-modal.component';


import { DynamicValuesComponent } from './qlvb/dynamicValues/dynamicValues.component';
import { ViewDynamicValueModalComponent } from './qlvb/dynamicValues/view-dynamicValue-modal.component';
import { CreateOrEditDynamicValueModalComponent } from './qlvb/dynamicValues/create-or-edit-dynamicValue-modal.component';

import { DynamicFieldsComponent } from './qlvb/dynamicFields/dynamicFields.component';
import { ViewDynamicFieldModalComponent } from './qlvb/dynamicFields/view-dynamicField-modal.component';
import { CreateOrEditDynamicFieldModalComponent } from './qlvb/dynamicFields/create-or-edit-dynamicField-modal.component';

import { SqlStoreParamsComponent } from './management/sqlStoreParams/sqlStoreParams.component';
import { ViewSqlStoreParamModalComponent } from './management/sqlStoreParams/view-sqlStoreParam-modal.component';
import { CreateOrEditSqlStoreParamModalComponent } from './management/sqlStoreParams/create-or-edit-sqlStoreParam-modal.component';

import { SqlConfigDetailsComponent } from './management/sqlConfigDetails/sqlConfigDetails.component';
import { ViewSqlConfigDetailModalComponent } from './management/sqlConfigDetails/view-sqlConfigDetail-modal.component';
import { CreateOrEditSqlConfigDetailModalComponent } from './management/sqlConfigDetails/create-or-edit-sqlConfigDetail-modal.component';

import { SqlConfigsComponent } from './management/sqlConfigs/sqlConfigs.component';
import { ViewSqlConfigModalComponent } from './management/sqlConfigs/view-sqlConfig-modal.component';
import { CreateOrEditSqlConfigModalComponent } from './management/sqlConfigs/create-or-edit-sqlConfig-modal.component';

import { SchedulesComponent } from './qlvb/schedules/schedules.component';
import { ViewScheduleModalComponent } from './qlvb/schedules/view-schedule-modal.component';
import { CreateOrEditScheduleModalComponent } from './qlvb/schedules/create-or-edit-schedule-modal.component';

import { DocumentHandlingDetailsComponent } from './qlvb/documentHandlingDetails/documentHandlingDetails.component';
import { ViewDocumentHandlingDetailModalComponent } from './qlvb/documentHandlingDetails/view-documentHandlingDetail-modal.component';
import { CreateOrEditDocumentHandlingDetailModalComponent } from './qlvb/documentHandlingDetails/create-or-edit-documentHandlingDetail-modal.component';

import {DataChamCongsComponent} from './qlns/dataChamCongs/dataChamCongs.component';

import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { ModifySqlConfigDetailsComponent } from './management/sqlConfigDetails/modify-sqlConfigDetails.component';
import { TruncatePipe } from '../shared/common/truncate.pipe'
import { FormatNumber } from '../shared/common/formatnumber.pipe'
import {
    DxTextAreaModule,
    DxMenuModule,
    DxDataGridModule,
    DxButtonModule,
    DxCheckBoxModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxProgressBarModule,
    DxPopupModule,
    DxTemplateModule,
    DxDateBoxModule,
    DxHtmlEditorModule,
    DxToolbarModule,
    DxButtonGroupModule,
    DxResizableModule,

    DxFileUploaderModule,
    DxTagBoxModule,
    DxListModule,
    DxScrollViewModule,
    DxTreeViewModule,
    DxContextMenuModule,
    DxPopoverModule,
    DxDropDownBoxModule,
    DxFormModule,
    DxFileManagerModule,
    DxSchedulerModule,
    DxTreeListModule,
    DxLookupModule,
    DxGanttModule,
    DxPieChartModule,
    DxNumberBoxModule
} from 'devextreme-angular';
import { CreateGroupDynamicFieldComponent } from './qlvb/dynamicFields/create-groupDynamicField.component';
import { EditGroupDynamicFieldComponent } from './qlvb/dynamicFields/edit-groupDynamicField.component';


import { ListAllDynamicFieldComponent } from './qlvb/dynamicFields/list-all-groupDynamicField.component';


import { ExecuteLabelSQLComponent } from './qlvb/executeLabelSQL/executeLabelSQL.component';

import { ListAllButtonUIComponent } from '@app/shared/common/buttonUI/list-all-buttonUI';

import { ButtonUIComponent } from '@app/shared/common/buttonUI/button-UI';
import { SYS_PREFIXsComponent } from './qlns/syS_PREFIXs/syS_PREFIXs.component';
import { ViewSYS_PREFIXModalComponent } from './qlns/syS_PREFIXs/view-syS_PREFIX-modal.component';
import { CreateOrEditSYS_PREFIXModalComponent } from './qlns/syS_PREFIXs/create-or-edit-syS_PREFIX-modal.component';
import { SYS_CODEMASTERSsComponent } from './qlns/syS_CODEMASTERSs/syS_CODEMASTERSs.component';
import { ViewSYS_CODEMASTERSModalComponent } from './qlns/syS_CODEMASTERSs/view-syS_CODEMASTERS-modal.component';
import { CreateOrEditSYS_CODEMASTERSModalComponent } from './qlns/syS_CODEMASTERSs/create-or-edit-syS_CODEMASTERS-modal.component';
import { EditUngVienComponent } from './qlns/ungViens/edit-ungVien.component';
import { HoSosComponent } from './qlns/hoSos/hoSos.component';
import { ViewHoSoModalComponent } from './qlns/hoSos/view-hoSo-modal.component';
import { CreateHoSoComponent } from './qlns/hoSos/create-hoSo.component';
import { EditHoSoComponent } from './qlns/hoSos/edit-hoSo.component';
import { HopDongsComponent } from './qlns/hopDongs/hopDongs.component';
import { ViewHopDongModalComponent } from './qlns/hopDongs/view-hopDong-modal.component';
//import { CreateOrEditHopDongModalComponent } from './qlns/hopDongs/create-or-edit-hopDong-modal.component';
import { TuongTacUngViensComponent } from './qlns/tuongTacUngVien/tuongTacUngViens.component';
import { UngViensComponent } from './qlns/ungViens/ungViens.component';
import { ViewUngVienModalComponent } from './qlns/ungViens/view-ungVien-modal.component';
import { CreateUngVienComponent } from './qlns/ungViens/create-ungVien.component';




import { DangKyKCBsComponent } from './qlns/dangKyKCBs/dangKyKCBs.component';
import { ViewDangKyKCBModalComponent } from './qlns/dangKyKCBs/view-dangKyKCB-modal.component';
import { CreateOrEditDangKyKCBModalComponent } from './qlns/dangKyKCBs/create-or-edit-dangKyKCB-modal.component';
import { LichSuLamViecsComponent } from './qlns/lichSuLamViecs/lichSuLamViecs.component';
import { ViewLichSuLamViecModalComponent } from './qlns/lichSuLamViecs/view-lichSuLamViec-modal.component';
import { CreateOrEditLichSuLamViecModalComponent } from './qlns/lichSuLamViecs/create-or-edit-lichSuLamViec-modal.component';
import { TinhThanhsComponent } from './qlns/tinhThanhs/tinhThanhs.component';
import { ViewTinhThanhModalComponent } from './qlns/tinhThanhs/view-tinhThanh-modal.component';
import { CreateOrEditTinhThanhModalComponent } from './qlns/tinhThanhs/create-or-edit-tinhThanh-modal.component';
import { TruongGiaoDichsComponent } from './qlns/truongGiaoDichs/truongGiaoDichs.component';
import { ViewTruongGiaoDichModalComponent } from './qlns/truongGiaoDichs/view-truongGiaoDich-modal.component';
import { CreateOrEditTruongGiaoDichModalComponent } from './qlns/truongGiaoDichs/create-or-edit-truongGiaoDich-modal.component';
import { NoiDaoTaosComponent } from './qlns/noiDaoTaos/noiDaoTaos.component';
import { ViewNoiDaoTaoModalComponent } from './qlns/noiDaoTaos/view-noiDaoTao-modal.component';
import { CreateOrEditNoiDaoTaoModalComponent } from './qlns/noiDaoTaos/create-or-edit-noiDaoTao-modal.component';
import { EditButtonUIComponent } from '@app/shared/common/buttonUI/edit-buttonUI';
import { OrganizationUnitsTreeComponent } from '@app/admin/shared/organization-unit-tree.component';
import { TreeModule } from 'primeng/tree';
import { CreateHopDongComponent } from './qlns/hopDongs/create-hoDong.component';
import { EditHopDongComponent } from './qlns/hopDongs/edit-hoDong.component';
import { ThongKeModalComponent } from './qlns/thongKe/thongKe-modal';
import { TemplateHDModalComponent } from './qlns/templateHD/templateHD-modal';
import { CKEditorModule } from 'ng2-ckeditor';
import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
import { DialogModule } from '@syncfusion/ej2-angular-popups';
import { ViewTuongTacUngVienModalComponent } from './qlns/tuongTacUngVien/view-tuongTacUngVien-modal.component';

import { CreateNghiPhepComponent } from './qlns/quanLyNghiPheps/create-nghiPhep.component';
import { EditNghiPhepComponent } from './qlns/quanLyNghiPheps/edit-nghiPhep.component';
import { ExportChamCongModalComponent } from './qlns/dataChamCongs/export-ChamCong-modal.component';
import { ViewNhanVienModalComponent } from './qlns/hopDongs/view-nhanVien-model.component';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        FileUploadModule,
        AutoCompleteModule,
        PaginatorModule,
        EditorModule,
        InputMaskModule, TableModule,
        DxTextAreaModule,
        DxMenuModule,
        DxCheckBoxModule,
        RichTextEditorAllModule,
        DialogModule,
        DxDataGridModule,
        DxButtonModule,
        DxCheckBoxModule,
        DxSelectBoxModule,
        DxHtmlEditorModule,
        DxTextBoxModule,
        DxTemplateModule,
        DxProgressBarModule,
        DxPopupModule,
        DxTemplateModule,
        DxDateBoxModule,
        DxHtmlEditorModule,
        DxToolbarModule,
        DxButtonGroupModule,
        DxResizableModule,
        CKEditorModule,
        DxFileUploaderModule,
        DxTagBoxModule,
        DxListModule,
        CKEditorModule,
        DxScrollViewModule,
        DxSchedulerModule,
        DxTreeViewModule,
        DxGanttModule,
        DxCheckBoxModule,
        DxSelectBoxModule,
        DxContextMenuModule,
        DxPopoverModule,
        DxDropDownBoxModule,
        DxFormModule,
        DxTextBoxModule,
        DxFileManagerModule,
        DxPopupModule,
        DxButtonModule,
        DxNumberBoxModule,
        DxLookupModule,
        FileUploadModule,
        AutoCompleteModule,
        PaginatorModule,
        EditorModule,
        InputMaskModule,
        TableModule,
        CommonModule,
        FormsModule,
        DxPieChartModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(), DxTreeListModule,
        TreeModule,
    ],
    declarations: [

	
	
		QuanLyNghiPhepsComponent,  CreateNghiPhepComponent,
        EditNghiPhepComponent,
		ViewQuanLyNghiPhepModalComponent,
		LichSuUploadsComponent,
		ViewLichSuUploadModalComponent,		CreateOrEditLichSuUploadModalComponent,
		ViTriCongViecsComponent,
		ViewViTriCongViecModalComponent,		CreateOrEditViTriCongViecModalComponent,
		ConfigEmailsComponent,
		ViewConfigEmailModalComponent,		CreateOrEditConfigEmailModalComponent,
		TemplatesComponent,
		ViewTemplateModalComponent,		CreateOrEditTemplateModalComponent,
        DashboardComponent,
        SYS_PREFIXsComponent,
        ViewSYS_PREFIXModalComponent,
        CreateOrEditSYS_PREFIXModalComponent,
		SYS_CODEMASTERSsComponent,
        ViewSYS_CODEMASTERSModalComponent,
        CreateOrEditSYS_CODEMASTERSModalComponent,
        EditUngVienComponent,
        TuongTacUngViensComponent,ViewTuongTacUngVienModalComponent,
        HoSosComponent,
        ViewHoSoModalComponent,
        CreateHoSoComponent,
        EditHoSoComponent,
		HopDongsComponent,
        ViewHopDongModalComponent,
        ViewNhanVienModalComponent,
    //    CreateOrEditHopDongModalComponent,
		UngViensComponent,
        ViewUngVienModalComponent,
        CreateUngVienComponent,
        DataChamCongsComponent,
        ExportChamCongModalComponent,

		DangKyKCBsComponent,
        ViewDangKyKCBModalComponent,
        CreateOrEditDangKyKCBModalComponent,
		LichSuLamViecsComponent,
        ViewLichSuLamViecModalComponent,
        CreateOrEditLichSuLamViecModalComponent,
		TinhThanhsComponent,
        ViewTinhThanhModalComponent,
        CreateOrEditTinhThanhModalComponent,
		TruongGiaoDichsComponent,
        ViewTruongGiaoDichModalComponent,
        CreateOrEditTruongGiaoDichModalComponent,
		NoiDaoTaosComponent,
        ViewNoiDaoTaoModalComponent,
        CreateOrEditNoiDaoTaoModalComponent,
		StoreDatasourcesComponent,      ListAllDynamicFieldComponent,
		DocumentHandlingDetailsComponent,
		CreateOrEditDocumentHandlingDetailModalComponent,
		StoreDatasourcesComponent,
		ViewStoreDatasourceModalComponent,		CreateOrEditStoreDatasourceModalComponent,
		HardDatasourcesComponent,
		ViewHardDatasourceModalComponent,		CreateOrEditHardDatasourceModalComponent,
		CommandDatasourcesComponent,
		ViewCommandDatasourceModalComponent,		CreateOrEditCommandDatasourceModalComponent,
		EditGroupDynamicFieldComponent,
		CreateGroupDynamicFieldComponent,
		DynamicDatasourceComponent,
		ViewDynamicDatasourceModalComponent,		CreateOrEditDynamicDatasourceModalComponent,
		ViewDynamicValueModalComponent,		CreateOrEditDynamicValueModalComponent,
		DynamicValuesComponent,
		ViewDynamicValueModalComponent,		CreateOrEditDynamicValueModalComponent,
		DynamicFieldsComponent,
		ViewDynamicFieldModalComponent,		CreateOrEditDynamicFieldModalComponent,
		SqlStoreParamsComponent,
		ViewSqlStoreParamModalComponent,		CreateOrEditSqlStoreParamModalComponent,
		ModifySqlConfigDetailsComponent,
		SqlConfigDetailsComponent,
		ViewSqlConfigDetailModalComponent,		CreateOrEditSqlConfigDetailModalComponent,
		SqlConfigsComponent,
		ViewSqlConfigModalComponent,		CreateOrEditSqlConfigModalComponent,


		SchedulesComponent,
		ViewScheduleModalComponent,		CreateOrEditScheduleModalComponent,

		DocumentHandlingDetailsComponent,
		ViewDocumentHandlingDetailModalComponent,		CreateOrEditDocumentHandlingDetailModalComponent,
		// MemorizeKeywordsComponent,

        CreateHopDongComponent,
        EditHopDongComponent,
        ExecuteLabelSQLComponent,

        ListAllButtonUIComponent ,
        EditButtonUIComponent,
        ButtonUIComponent ,
        ThongKeModalComponent ,
        TemplateHDModalComponent,
        TruncatePipe,
        FormatNumber


    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ],
    entryComponents:[
        ViewNhanVienModalComponent
    ]
})
export class MainModule { }

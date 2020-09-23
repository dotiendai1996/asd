import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';





import { LichSuUploadsComponent } from './qlns/lichSuUploads/lichSuUploads.component';

import { ViTriCongViecsComponent } from './qlns/viTriCongViecs/viTriCongViecs.component';
import { ConfigEmailsComponent } from './qlns/configEmails/configEmails.component';
import { TemplatesComponent } from './qlns/templates/templates.component';
import { DynamicDatasourceComponent } from './qlvb/dynamicDatasource/dynamicDatasource.component';
import { DynamicValuesComponent } from './qlvb/dynamicValues/dynamicValues.component';
import { DynamicFieldsComponent } from './qlvb/dynamicFields/dynamicFields.component';
import { SqlStoreParamsComponent } from './management/sqlStoreParams/sqlStoreParams.component';
import { SqlConfigDetailsComponent } from './management/sqlConfigDetails/sqlConfigDetails.component';
import { SqlConfigsComponent } from './management/sqlConfigs/sqlConfigs.component';

import { SchedulesComponent } from './qlvb/schedules/schedules.component';
import { DocumentHandlingDetailsComponent } from './qlvb/documentHandlingDetails/documentHandlingDetails.component';

//import { DocumentsesComponent } from './qlvb/documentses/documentses.component';

import { DashboardComponent } from './dashboard/dashboard.component';
import { ModifySqlConfigDetailsComponent } from './management/sqlConfigDetails/modify-sqlConfigDetails.component';

import {StoreDatasourcesComponent} from '@app/main/qlvb/storeDatasources/storeDatasources.component';

import {HardDatasourcesComponent} from '@app/main/qlvb/hardDatasources/hardDatasources.component';
import {CommandDatasourcesComponent} from '@app/main/qlvb/commandDatasources/commandDatasources.component';


import {EditGroupDynamicFieldComponent} from '@app/main/qlvb/dynamicFields/edit-groupDynamicField.component';
import {CreateGroupDynamicFieldComponent} from '@app/main/qlvb/dynamicFields/create-groupDynamicField.component';


import { ListAllDynamicFieldComponent } from './qlvb/dynamicFields/list-all-groupDynamicField.component';

import { ExecuteLabelSQLComponent } from './qlvb/executeLabelSQL/executeLabelSQL.component';

import { ListAllButtonUIComponent } from '@app/shared/common/buttonUI/list-all-buttonUI';

import { ButtonUIComponent } from '@app/shared/common/buttonUI/button-UI';
import { SYS_PREFIXsComponent } from './qlns/syS_PREFIXs/syS_PREFIXs.component';
import { SYS_CODEMASTERSsComponent } from './qlns/syS_CODEMASTERSs/syS_CODEMASTERSs.component';
import { HoSosComponent } from './qlns/hoSos/hoSos.component';
import { HopDongsComponent } from './qlns/hopDongs/hopDongs.component';
import { UngViensComponent } from './qlns/ungViens/ungViens.component';
import { CreateUngVienComponent } from './qlns/ungViens/create-ungVien.component';
import { EditUngVienComponent } from './qlns/ungViens/edit-ungVien.component';


import { DangKyKCBsComponent } from './qlns/dangKyKCBs/dangKyKCBs.component';
import { LichSuLamViecsComponent } from './qlns/lichSuLamViecs/lichSuLamViecs.component';
import { TinhThanhsComponent } from './qlns/tinhThanhs/tinhThanhs.component';
import { TruongGiaoDichsComponent } from './qlns/truongGiaoDichs/truongGiaoDichs.component';
import { NoiDaoTaosComponent } from './qlns/noiDaoTaos/noiDaoTaos.component';
import { CreateHoSoComponent } from './qlns/hoSos/create-hoSo.component';
import { EditHoSoComponent } from './qlns/hoSos/edit-hoSo.component';
import { CreateHopDongComponent } from './qlns/hopDongs/create-hoDong.component';
import { EditHopDongComponent } from './qlns/hopDongs/edit-hoDong.component';
import { ThongKeModalComponent } from './qlns/thongKe/thongKe-modal';
import { TemplateHDModalComponent } from './qlns/templateHD/templateHD-modal';
import { ViewUngVienModalComponent } from './qlns/ungViens/view-ungVien-modal.component';
import { ViewHoSoModalComponent } from './qlns/hoSos/view-hoSo-modal.component';
import { TuongTacUngViensComponent } from './qlns/tuongTacUngVien/tuongTacUngViens.component';
import { ViewTuongTacUngVienModalComponent } from './qlns/tuongTacUngVien/view-tuongTacUngVien-modal.component';
import { QuanLyNghiPhepsComponent } from './qlns/quanLyNghiPheps/quanLyNghiPheps.component';

import { EditNghiPhepComponent } from './qlns/quanLyNghiPheps/edit-nghiPhep.component';
import { CreateNghiPhepComponent } from './qlns/quanLyNghiPheps/create-nghiPhep.component';

import {DataChamCongsComponent} from './qlns/dataChamCongs/dataChamCongs.component';
import { ViewQuanLyNghiPhepModalComponent } from './qlns/quanLyNghiPheps/view-quanLyNghiPhep-modal.component';

// import { ScheduleComponent } from './qlvb/schedule/schedule.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                   // { path: 'qlns/quanLyTrucTiepPNPs', component: QuanLyTrucTiepPNPsComponent, data: { permission: 'Pages.QuanLyTrucTiepPNPs' }  },
                    { path: 'qlns/quanLyNghiPheps', component: QuanLyNghiPhepsComponent, data: { permission: 'Pages.QuanLyNghiPheps' }  },
                  
                    { path: 'qlns/lichSuUploads', component: LichSuUploadsComponent, data: { permission: 'Pages.LichSuUploads' }  },
                    { path: 'qlns/viTriCongViecs', component: ViTriCongViecsComponent, data: { permission: 'Pages.ViTriCongViecs' }  },
                    { path: 'qlns/configEmails', component: ConfigEmailsComponent, data: { permission: 'Pages.ConfigEmails' }  },
                    { path: 'qlns/templates', component: TemplatesComponent, data: { permission: 'Pages.Templates' }  },
                    { path: 'qlns/syS_PREFIXs', component: SYS_PREFIXsComponent, data: { permission: 'Pages.SYS_PREFIXs' }  },
                    { path: 'qlns/syS_CODEMASTERSs', component: SYS_CODEMASTERSsComponent, data: { permission: 'Pages.SYS_CODEMASTERSs' }  },
                    { path: 'qlns/hoSos', component: HoSosComponent, data: { permission: 'Pages.HoSos' }  },
                    { path: 'qlns/hoSos/create', component: CreateHoSoComponent, data: { permission: 'Pages.HoSos' }  },
                    { path: 'qlns/hoSos/edit/:id', component: EditHoSoComponent, data: { permission: 'Pages.HoSos' }  },
                    { path: 'qlns/hopDongs', component: HopDongsComponent, data: { permission: 'Pages.HopDongs' }  },
                    { path: 'qlns/ungViens', component: UngViensComponent, data: { permission: 'Pages.UngViens' }  },
                    { path: 'qlns/ungViens/create', component: CreateUngVienComponent, data: { permission: 'Pages.UngViens' }  },
                    { path: 'qlns/ungViens/edit/:id', component: EditUngVienComponent, data: { permission: 'Pages.UngViens' }  },           
                    { path: 'qlns/dangKyKCBs', component: DangKyKCBsComponent, data: { permission: 'Pages.DangKyKCBs' }  },
                    { path: 'qlns/lichSuLamViecs', component: LichSuLamViecsComponent, data: { permission: 'Pages.LichSuLamViecs' }  },
                    { path: 'qlns/tinhThanhs', component: TinhThanhsComponent, data: { permission: 'Pages.TinhThanhs' }  },
                    { path: 'qlns/truongGiaoDichs', component: TruongGiaoDichsComponent, data: { permission: 'Pages.TruongGiaoDichs' }  },
                    { path: 'qlns/noiDaoTaos', component: NoiDaoTaosComponent, data: { permission: 'Pages.NoiDaoTaos' }  },
                    { path: 'qlvb/dynamicFields/list', component: ListAllDynamicFieldComponent, data: { permission: 'Pages.DynamicFields' }},
                    { path: 'qlvb/storeDatasources', component: StoreDatasourcesComponent, data: { permission: 'Pages.StoreDatasources' }  },
                    { path: 'qlvb/hardDatasources', component: HardDatasourcesComponent, data: { permission: 'Pages.HardDatasources' }  },
                    { path: 'qlvb/commandDatasources', component: CommandDatasourcesComponent, data: { permission: 'Pages.CommandDatasources' }  },
                    { path: 'qlvb/dynamicFields/edit/:id', component: EditGroupDynamicFieldComponent, data: { permission: 'Pages.DynamicFields' }},
                    { path: 'qlvb/dynamicFields/create', component: CreateGroupDynamicFieldComponent, data: { permission: 'Pages.DynamicFields' }},

                    { path: 'qlvb/dynamicDatasource', component: DynamicDatasourceComponent, data: { permission: 'Pages.DynamicDatasource' }  },


                    { path: 'qlvb/dynamicValues', component: DynamicValuesComponent, data: { permission: 'Pages.DynamicValues' }  },
                    { path: 'qlvb/dynamicFields', component: DynamicFieldsComponent, data: { permission: 'Pages.DynamicFields' }  },
                    { path: 'management/sqlStoreParams', component: SqlStoreParamsComponent, data: { permission: 'Pages.SqlStoreParams' }  },
                    { path: 'management/sqlConfigDetails/config/:id', component: ModifySqlConfigDetailsComponent, data: { permission: 'Pages.SqlConfigDetails' }  },
                    { path: 'management/sqlConfigDetails', component: SqlConfigDetailsComponent, data: { permission: 'Pages.SqlConfigDetails' }  },
                    { path: 'management/sqlConfigs', component: SqlConfigsComponent, data: { permission: 'Pages.SqlConfigs' }  },
                    { path: 'qlvb/schedules', component: SchedulesComponent, data: { permission: 'Pages.Schedules' }  },
                    { path: 'qlvb/documentHandlingDetails', component: DocumentHandlingDetailsComponent, data: { permission: 'Pages.DocumentHandlingDetails' }  },
                     { path: 'qlns/hoSo/XemChiTiet/:id', component: ViewHoSoModalComponent, data: { permission: '' }  },
                     { path: 'qlns/ungVien/xemChiTiet/:id', component: ViewUngVienModalComponent, data: { permission: '' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'qlns/tuongtacungvien', component: TuongTacUngViensComponent, data: { permission: 'Pages.UngViens' }  },
                    { path: 'qlns/tuongtacungvien/:id', component: ViewTuongTacUngVienModalComponent, data: { permission: 'Pages.UngViens.Edit' }  },
                    { path: 'qlvb/executeLabelSQL', component: ExecuteLabelSQLComponent, data: { permission: '' } },
                    //luong quan ly
                    { path: 'qlns/hopDong/create', component: CreateHopDongComponent, data: { permission: 'Pages.HopDongs' }  },
                    { path: 'qlns/hopDong/edit/:id', component: EditHopDongComponent, data: { permission: 'Pages.HopDongs' }  },

                     { path: 'qlns/thongKe', component: ThongKeModalComponent, data: { permission: 'Pages.HopDongs' } },
                     { path: 'qlns/templateHD', component: TemplateHDModalComponent, data: { permission: 'Pages.HopDongs' } },

                     { path: 'qlns/nghiPhep/xemChiTiet/:id', component: ViewQuanLyNghiPhepModalComponent, data: { permission: 'Pages.QuanLyNghiPheps' }  },
                     { path: 'qlns/nghiPhep/create', component: CreateNghiPhepComponent, data: { permission: 'Pages.QuanLyNghiPheps' }  },
                     { path: 'qlns/nghiPhep/edit/:id', component: EditNghiPhepComponent, data: { permission: 'Pages.QuanLyNghiPheps' }  },
                     { path: 'qlns/chamcong', component: DataChamCongsComponent, data: { permission: 'Pages.DataChamCongs' }  },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }

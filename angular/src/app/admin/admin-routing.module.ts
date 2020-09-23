import { NgModule } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { TemplatesComponent } from './qlns/templates/templates.component';
import { DynamicActionsComponent } from './qlvb/dynamicActions/dynamicActions.component';
import { RoleMappersComponent } from './qlvb/roleMappers/roleMappers.component';
import { LabelsComponent } from './management/labels/labels.component';
import { SettingConfigsComponent } from './management/settingConfigs/settingConfigs.component';
import { MenusComponent } from './management/menus/menus.component';

import { AuditLogsComponent } from './audit-logs/audit-logs.component';
import { HostDashboardComponent } from './dashboard/host-dashboard.component';
import { DemoUiComponentsComponent } from './demo-ui-components/demo-ui-components.component';
import { EditionsComponent } from './editions/editions.component';
import { InstallComponent } from './install/install.component';
import { LanguageTextsComponent } from './languages/language-texts.component';
import { LanguagesComponent } from './languages/languages.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { OrganizationUnitsComponent } from './organization-units/organization-units.component';
import { RolesComponent } from './roles/roles.component';
import { HostSettingsComponent } from './settings/host-settings.component';
import { TenantSettingsComponent } from './settings/tenant-settings.component';
import { InvoiceComponent } from './subscription-management/invoice/invoice.component';
import { SubscriptionManagementComponent } from './subscription-management/subscription-management.component';
import { TenantsComponent } from './tenants/tenants.component';
import { UiCustomizationComponent } from './ui-customization/ui-customization.component';
import { UsersComponent } from './users/users.component';
import { MenusServiceProxy } from '@shared/service-proxies/service-proxies';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'qlns/templates', component: TemplatesComponent, data: { permission: 'Pages.Administration.Templates' }  },
                    { path: 'qlvb/dynamicActions', component: DynamicActionsComponent, data: { permission: '' }  },
                    { path: 'roleMappers', component: RoleMappersComponent, data: { permission: '' }  },
                    //{ path: 'dynamicModule', component: DynamicModuleComponent, data: {}},
                    { path: 'labels', component: LabelsComponent, data: { permission: '' }  },
                    { path: 'settingConfigs', component: SettingConfigsComponent, data: { permission: '' }  },
                    { path: 'menus', component: MenusComponent, data: { permission: '' }  },
                    
                    { path: 'users', component: UsersComponent, data: { permission: '' } },
                    { path: 'roles', component: RolesComponent, data: { permission: '' } },
                    { path: 'auditLogs', component: AuditLogsComponent, data: { permission: '' } },
                    { path: 'maintenance', component: MaintenanceComponent, data: { permission: '' } },
                    { path: 'hostSettings', component: HostSettingsComponent, data: { permission: '' } },
                    { path: 'editions', component: EditionsComponent, data: { permission: '' } },
                    { path: 'languages', component: LanguagesComponent, data: { permission: '' } },
                    { path: 'languages/:name/texts', component: LanguageTextsComponent, data: { permission: '' } },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' } },
                    { path: 'organization-units', component: OrganizationUnitsComponent, data: { permission: '' } },
                    { path: 'subscription-management', component: SubscriptionManagementComponent, data: { permission: 'Pages.Administration.Tenant.SubscriptionManagement' } },
                    { path: 'invoice/:paymentId', component: InvoiceComponent, data: { permission: 'Pages.Administration.Tenant.SubscriptionManagement' } },
                    { path: 'tenantSettings', component: TenantSettingsComponent, data: { permission: 'Pages.Administration.Tenant.Settings' } },
                    { path: 'hostDashboard', component: HostDashboardComponent, data: { permission: '' } },
                    { path: 'demo-ui-components', component: DemoUiComponentsComponent, data: { permission: 'Pages.DemoUiComponents' } },
                    { path: 'install', component: InstallComponent },
                    { path: 'ui-customization', component: UiCustomizationComponent },
                    // { path: 'menu', component: MenusServiceProxy, data: {  }}
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule {

    constructor(
        private router: Router
    ) {
        router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }
}

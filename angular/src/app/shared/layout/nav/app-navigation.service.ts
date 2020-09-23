import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';
import { MenusServiceProxy, PagedResultDtoOfGetMenusForViewDto, MenuDto, UserServiceProxy, LabelsServiceProxy, DocumentServiceProxy, CounterDto } from '@shared/service-proxies/service-proxies';
import { error } from '@angular/compiler/src/util';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';

@Injectable()
export class AppNavigationService {

    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _menuApp: MenusServiceProxy,
        private _userAppService: UserServiceProxy,
        private _labelAppService: LabelsServiceProxy,
        private _documentAppService: DocumentServiceProxy
    ) {
    }

    currentUserRole = '';
    child = '';
    labels: AppMenuItem[] = [];
    labelFather: AppMenuItem[] = [];
    labelFather1: AppMenuItem[] = [];
    labelFather2: AppMenuItem[] = [];
    dataDisplay: any;
    list_label: any;
    selecchildID: any;
    selecchildParenttID: any;
    selecMenuParenttID: any
    labels_: AppMenuItem[] = [];

    numberOfDocuments: CounterDto[] = [];
    fullMenu = new AppMenu('LeftMenu', 'LeftMenu', []);

    getUserRole(): string{
        this._userAppService.getRoleNameOfUser(this._appSessionService.userId).subscribe((result: string) => {
            return result;
        });
        return '';
    }

    getFullLabels(): AppMenu{
        // this.getNumberOfDocument();
        this.createMenu();
    
        return this.fullMenu;
      //  return new AppMenu("LeftMenu", "LeftMenu", this.labels);
       
    }

    getNumberOfDocument(){
        this._documentAppService.getNumberOfAllDocumentType().subscribe(result => {
            this.numberOfDocuments = result;
        });
    }

    // createMenu(){
    //     var child = [];
    //     this.getNumberOfDocument();
    //     this._labelAppService.getFullLabels().subscribe(result => {
    //         result.forEach(x => {
    //             var father = new AppMenuItem(x.name, x.requiredPermissionName, x.icon, x.link);
    //             if(x.childLabels){
    //                 x.childLabels.forEach(z => {
    //                     this.numberOfDocuments.every(element => {
    //                         if(z.name == element.code){
    //                             z.name = z.name + "(" + element.number +")";
    //                             return false;
    //                         }
    //                         else return true;
    //                     });
    //                     child.push(new AppMenuItem(z.name, z.requiredPermissionName, z.icon, z.link))
    //                 });
    //                 father.items = child;
    //                 child = [];
    //             }
                
    //             this.labels.push(father);
    //         });
    //     });
    // }

    createMenu(){
        var child = [];
        // this.getNumberOfDocument();
        this._labelAppService.getChildLabel().subscribe(result => {
         
           this.labelFather1.length = 0;
            result.forEach(x => {
                
                this.numberOfDocuments.every(element => {
                    if(x.name == element.code){
                        x["numNoti"] = element.number; 
                        return false;
                    }
                    else return true;
                });
                var father = new AppMenuItem(x.name, x.requiredPermissionName, x.icon, x.link, [], x.id,  x.parent);
                
                this.labelFather1.push(father);
              
                
            this.labels_.length = 0;

            this.labels_ = this._arrayToTreeConverterService.createMenu(this.labelFather1,   // ben kia no la 1 cai list 
                "parent",
                'id',
                0, 
                'items'
            );
         
            this.fullMenu.items = this.labels_;
                
            });
       
        });
        
    }

    getAllTopMenu(): AppMenu {
        var topMenus = [];

        this._menuApp.getAllTopMenu().subscribe(result => {
            result.items.forEach(element => {
                topMenus.push(new AppMenuItem(element.title, element.requiredPermissionName, element.icon, element.link, [], element.id));
            });
            console.log(topMenus);
            localStorage.setItem('topMenuSelected', topMenus[0].id);
            console.log(localStorage.getItem('topMenuSelected'));
        });
        return new AppMenu('TopMenu', 'TopMenu', topMenus);
    }

    getAllSideBarMenu(paretnId: number): AppMenu {
        var sideMenu = [];

        this._menuApp.getAllSideBarMenu(paretnId).subscribe(result => {
            result.items.forEach(element => {
                sideMenu.push(new AppMenuItem(element.title, element.requiredPermissionName, element.icon, element.link, [], element.id));
            });
            
        console.log(result);
        });
        return new AppMenu('TopMenu', 'TopMenu', sideMenu);
    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [

        
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            //new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Documentses', 'Pages.Documentses', 'flaticon-more', '/app/main/qlvb/documentses'),
            
            new AppMenuItem('DocumentTypes', 'Pages.DocumentTypes', 'flaticon-more', '/app/main/qlvb/documentTypes'),
            
            new AppMenuItem('WorkAssigns', 'Pages.WorkAssigns', 'flaticon-more', '/app/main/qlvb/workAssigns'),
            
            new AppMenuItem('WordProcessings', 'Pages.WordProcessings', 'flaticon-more', '/app/main/qlvb/wordProcessings'),
            
            new AppMenuItem('WorkDetails', 'Pages.WorkDetails', 'flaticon-more', '/app/main/qlvb/workDetails'),
            
            new AppMenuItem('DocumentDetails', 'Pages.DocumentDetails', 'flaticon-more', '/app/main/qlvb/documentDetails'),
            
            new AppMenuItem('MemorizeKeywords', 'Pages.MemorizeKeywords', 'flaticon-more', '/app/main/qlvb/memorizeKeywords'),
            
            new AppMenuItem('DocumentHandlingDetails', 'Pages.DocumentHandlingDetails', 'flaticon-more', '/app/main/qlvb/documentHandlingDetails'),
            
            new AppMenuItem('TypeHandes', 'Pages.TypeHandes', 'flaticon-more', '/app/main/qlvb/typeHandes'),
            
            new AppMenuItem('ReceiveUnits', 'Pages.ReceiveUnits', 'flaticon-more', '/app/main/qlvb/receiveUnits'),
            
            new AppMenuItem('Promulgateds', 'Pages.Promulgateds', 'flaticon-more', '/app/main/qlvb/promulgateds'),
            
            new AppMenuItem('Schedules', 'Pages.Schedules', 'flaticon-more', '/app/main/qlvb/schedules'),
            
            new AppMenuItem('DocumentHandlings', 'Pages.DocumentHandlings', 'flaticon-more', '/app/main/qlvb/documentHandlings'),
           
            new AppMenuItem('HistoryUploads', 'Pages.HistoryUploads', 'flaticon-more', '/app/main/qlvb/historyUploads'),
            
            new AppMenuItem('DynamicFields', 'Pages.DynamicFields', 'flaticon-more', '/app/main/qlvb/dynamicFields'),
            
            new AppMenuItem('DynamicValues', 'Pages.DynamicValues', 'flaticon-more', '/app/main/qlvb/dynamicValues'),
            
            new AppMenuItem('Textbooks', 'Pages.Textbooks', 'flaticon-more', '/app/main/qlvb/textbooks'),
            
            new AppMenuItem('TextBooks', 'Pages.TextBooks', 'flaticon-more', '/app/main/qlvb/textBooks'),
            
            new AppMenuItem('Memorize_Keywordses', 'Pages.Memorize_Keywordses', 'flaticon-more', '/app/main/qlvb/memorize_Keywordses'),
            new AppMenuItem('Priorities', 'Pages.Priorities', 'flaticon-more', '/app/main/qlvb/priorities'),
            
            new AppMenuItem('DocumentStatuses', 'Pages.DocumentStatuses', 'flaticon-more', '/app/main/qlvb/documentStatuses'),
            
            new AppMenuItem('KeywordDetails', 'Pages.KeywordDetails', 'flaticon-more', '/app/main/qlvb/keywordDetails'),
            
            new AppMenuItem('Templates', 'Pages.Templates', 'flaticon-more', '/app/main/qlns/templates'),
            
            new AppMenuItem('ConfigEmails', 'Pages.ConfigEmails', 'flaticon-more', '/app/main/qlns/configEmails'),
            
            new AppMenuItem('ViTriCongViecs', 'Pages.ViTriCongViecs', 'flaticon-more', '/app/main/qlns/viTriCongViecs'),
            
            new AppMenuItem('HistoryUploads', 'Pages.HistoryUploads', 'flaticon-more', '/app/main/qlns/historyUploads'),
            
            new AppMenuItem('LichSuUploads', 'Pages.LichSuUploads', 'flaticon-more', '/app/main/qlns/lichSuUploads'),
            
            new AppMenuItem('QuyTrinhCongTacs', 'Pages.QuyTrinhCongTacs', 'flaticon-more', '/app/main/qlns/quyTrinhCongTacs'),
            
            new AppMenuItem('DataChamCongs', 'Pages.DataChamCongs', 'flaticon-more', '/app/main/qlns/dataChamCongs'),
            
            new AppMenuItem('QuanLyNghiPheps', 'Pages.QuanLyNghiPheps', 'flaticon-more', '/app/main/qlns/quanLyNghiPheps'),
            
            new AppMenuItem('QuanLyTrucTiepPNPs', 'Pages.QuanLyTrucTiepPNPs', 'flaticon-more', '/app/main/qlns/quanLyTrucTiepPNPs'),
             new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
                // new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                // new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                // new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
            new AppMenuItem('Templates', 'Pages.Administration.Templates', 'flaticon-more', '/app/admin/qlns/templates'),
            
            // new AppMenuItem('DynamicActions', 'Pages.Administration.DynamicActions', 'flaticon-more', '/app/admin/qlvb/dynamicActions'),
            
            // new AppMenuItem('RoleMappers', 'Pages.Administration.RoleMappers', 'flaticon-more', '/app/admin/qlvb/roleMappers'),
            
            
            // new AppMenuItem('Labels', 'Pages.Administration.Labels', 'flaticon-more', '/app/admin/management/labels'),
            
                // new AppMenuItem('SettingConfigs', 'Pages.Administration.SettingConfigs', 'flaticon-more', '/app/admin/management/settingConfigs'),
                
                // new AppMenuItem('Menus', 'Pages.Administration.Menus', 'flaticon-more', '/app/admin/management/menus'),
            
                // new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
                // new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                // new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                // new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
                // new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
                // new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                // new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ])
            //new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')
        ]);


        // this.currentUserRole = this._permissionCheckerService.isGranted("Page.Administration") ? 'Administrator' : 'User';
		// //if(type === undefined){
        //     if(this.currentUserRole == 'Administrator'){
        //         //Click Menu Top đổi Menu Side
        //         if(localStorage.getItem("TypeOfTopMenu") !== null){  
                            
        //             typeOfTopMenu = localStorage.getItem("TypeOfTopMenu");
                  
        //             this.getParentMenu(typeOfTopMenu, this.currentUserRole);
        //             //this.getAllChildMenus();
        //         }
        //         else{                   
        //             typeOfTopMenu = "administration";
        //             this.getParentMenu(typeOfTopMenu, this.currentUserRole);
        //             //this.getAllChildMenus();
        //         }
        //     }
        //     else{

        //         this.getParentMenu(typeOfTopMenu, this.currentUserRole);
        //         //this.getAllChildMenus();
        //     }         

 
        // return new AppMenu('MainMenu','MainMenu',
        //         this.menus
        //     );
    }

    checkChildMenuItemPermission(menuItem): boolean {

        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null || subMenuItem.permissionName && this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                return true;
            } else if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            }
        }

        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' && this._appSessionService.tenant && !this._appSessionService.tenant.edition) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }

    /**
     * Returns all menu items recursively
     */
    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach(menuItem => {
            // allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });

        return allMenuItems;
    }

    // private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
    //     if (!menuItem.items) {
    //         return [menuItem];
    //     }

    //     let menuItems = [menuItem];
    //     menuItem.items.forEach(subMenu => {
    //         menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
    //     });

    //     return menuItems;
    // }
}

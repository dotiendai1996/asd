import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Component, Injector, OnInit, ViewEncapsulation, Input, Renderer2, Output, EventEmitter } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AppMenu } from './app-menu';
import { AppNavigationService } from './app-navigation.service';
import * as objectPath from 'object-path';
import { filter } from 'rxjs/operators';
import { MenuOptions } from '@metronic/app/core/_base/layout/directives/menu.directive';
import { OffcanvasOptions } from '@metronic/app/core/_base/layout/directives/offcanvas.directive';
import { MenuDto, MenusServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppMenuItem } from './app-menu-item';

@Component({
    templateUrl: './top-bar-menu.component.html',
    selector: 'top-bar-menu',
    encapsulation: ViewEncapsulation.None
})
export class TopBarMenuComponent extends AppComponentBase implements OnInit {
    @Input() isTabMenuUsed?: boolean;
    @Output() topId: EventEmitter<string> = new EventEmitter<string>();
    menu: AppMenu = null;
    currentRouteUrl: any = '';
    menuDepth: 0;
    clickButton = [];
    childMenu: MenuDto[] = [];

    menuOptions: MenuOptions = {
        submenu: {
            desktop: 'side-bar',
            tablet: 'accordion',
            mobile: 'accordion'
        },
        accordion: {
            slideSpeed: 200, // accordion toggle slide speed in milliseconds
            expandAll: true // allow having multiple expanded accordions in the menu
        }
    };

    offcanvasOptions: OffcanvasOptions = {
        overlay: true,
        baseClass: 'kt-header-menu-wrapper',
        closeBy: 'kt_header_menu_mobile_close_btn',
        toggleBy: {
            target: 'kt_header_mobile_toggler',
            state: 'kt-header-mobile__toolbar-toggler--active'
        }
    };

    dataRefresher: any;

    constructor(
        injector: Injector,
        private router: Router,
        public permission: PermissionCheckerService,
        private _appNavigationService: AppNavigationService,
        private _menuService: MenusServiceProxy,
        private render: Renderer2) {
        super(injector);
    }

    log(id: number){
        console.log(id);
    }

    ngOnInit() {
        // this.menu = this._appNavigationService.getAllTopMenu();
        // console.log(this.menu)
        // if(localStorage.getItem('topMenuSelected') != undefined){
        //     this._menuService.getAllChildMenu(parseInt(localStorage.getItem('topMenuSelected'))).subscribe((result) => {
        //         this.childMenu = result;
        //     });
        // }
        // else{
        // }

        // this.childMenu = this.menu.items[0].items;


        this.currentRouteUrl = this.router.url;

        this.router.events
            // .pipe(filter(event => event instanceof NavigationEnd))
            .pipe()
            .subscribe(event => {
                this.currentRouteUrl = this.router.url;
        });

        // this.getData(true);
        // this.refreshData();
        // setTimeout(() => {
            
        // }, 3000);
        
        //6/3/2020 luôn click vào nhân sự
        //this.mouseClick(null, '1024');

    }

    // getData(setPageFlag){
    //     if(localStorage.getItem('topMenuSelected') != undefined){
    //         this._menuService.getAllChildMenu(parseInt(localStorage.getItem('topMenuSelected'))).subscribe((result) => {
    //             this.childMenu = result;
    //         });
    //     }
    // }

    // refreshData(){
    //     this.dataRefresher = 
    //     setInterval(() => {
    //         this.getData(false);
    //     }, 3000);
    // }

    /**
	 * Use for fixed left aside menu, to show menu on mouseenter event.
	 * @param e Event
	 */
    mouseEnter(e: Event) {
        // check if the left aside menu is fixed
        if (!document.body.classList.contains('kt-menu__item--hover')) {
            this.render.addClass(document.body, 'kt-menu__item--hover');
        }
    }

    /**
     * Mouse Leave event
     * @param event: MouseEvent
     */
    mouseLeave(event: MouseEvent) {
        this.render.removeClass(event.target, 'kt-menu__item--hover');
    }

    mouseClick($event: MouseEvent, id: string){
        localStorage.setItem('topMenuSelected', id);
        // location.reload();
        this._menuService.getAllChildMenu(parseInt(id)).subscribe((result) => {
            this.childMenu = result;
        });
        //chỉ có 1 button sáng lên khi nhấn vào
        this.clickButton.push(id);
        if(this.clickButton.length > 1){
            document.getElementById("TopMenu"+this.clickButton[0]).classList.remove("kt-menu__item--active");
            this.clickButton.shift();           
        }
        document.getElementById("TopMenu"+id).classList.add("kt-menu__item--active");
        this.topId.emit(id);
    }

    showMenuItem(menuItem): boolean {
        return this._appNavigationService.showMenuItem(menuItem);
    }

    getItemCssClasses(item, parentItem, depth) {
        let isRootLevel = item && !parentItem;

        let cssClasses = 'kt-menu__item kt-menu__item--rel';

        if (objectPath.get(item, 'items.length') || this.isRootTabMenuItemWithoutChildren(item, isRootLevel)) {
            cssClasses += ' kt-menu__item--submenu';
        }

        if (objectPath.get(item, 'icon-only')) {
            cssClasses += ' kt-menu__item--icon-only';
        }

        if (this.isMenuItemIsActive(item)) {
            cssClasses += ' kt-menu__item--active';
        }

        if (this.isTabMenuUsed && isRootLevel) {
            cssClasses += ' kt-menu__item--tabs';
        }

        if (this.isTabMenuUsed && !isRootLevel && item.items.length) {
            cssClasses += ' kt-menu__item--submenu kt-menu__item--rel';
            if (depth && depth === 1) {
                cssClasses += ' kt-menu__item--submenu-tabs kt-menu__item--open-dropdown';
            }

        } else if (!this.isTabMenuUsed && item.items.length) {
            if (depth && depth >= 1) {
                cssClasses += ' kt-menu__item--submenu';
            } else {
                cssClasses += ' kt-menu__item--rel';
            }
        }

        return cssClasses;
    }

    getAnchorItemCssClasses(item, parentItem): string {
        let isRootLevel = item && !parentItem;
        let cssClasses = 'kt-menu__link';

        if ((this.isTabMenuUsed && isRootLevel) || item.items.length) {
            cssClasses += ' kt-menu__toggle';
        }

        return cssClasses;
    }

    getSubmenuCssClasses(item, parentItem, depth): string {
        let cssClasses = 'kt-menu__submenu kt-menu__submenu--classic';

        if (this.isTabMenuUsed) {
            if (depth === 0) {
                cssClasses += ' kt-menu__submenu--tabs';
            }

            cssClasses += ' kt-menu__submenu--' + (depth >= 2 ? 'right' : 'left');
        } else {
            cssClasses += ' kt-menu__submenu--' + (depth >= 1 ? 'right' : 'left');
        }

        return cssClasses;
    }

    isRootTabMenuItemWithoutChildren(item: any, isRootLevel: boolean): boolean {
        return this.isTabMenuUsed && isRootLevel && !item.items.length;
    }

    isMenuItemIsActive(item): boolean {
        if (item.items.length) {
            return this.isMenuRootItemIsActive(item);
        }

        if (!item.route) {
            return false;
        }

        return item.route === this.currentRouteUrl;
    }

    isMenuRootItemIsActive(item): boolean {
        if (item.items) {
            for (const subItem of item.items) {
                if (this.isMenuItemIsActive(subItem)) {
                    return true;
                }
            }
        }

        return false;
    }

    getItemAttrSubmenuToggle(menuItem, parentItem, depth) {
        let isRootLevel = menuItem && !parentItem;
        if (isRootLevel && this.isTabMenuUsed) {
            return 'tab';
        } else {
            if (depth && depth >= 1) {
                return 'hover';
            } else {
                return 'click';
            }
        }
    }

    isMobileDevice(): any {
        return KTUtil.isMobileDevice();
    }
}

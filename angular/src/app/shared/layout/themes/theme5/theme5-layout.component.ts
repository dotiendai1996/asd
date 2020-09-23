import { Injector, ElementRef, Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ThemesLayoutBaseComponent } from '@app/shared/layout/themes/themes-layout-base.component';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { LayoutRefService } from '@metronic/app/core/_base/layout/services/layout-ref.service';
import { AppConsts } from '@shared/AppConsts';
import { OffcanvasOptions } from '@metronic/app/core/_base/layout/directives/offcanvas.directive';
import { SideBarMenuComponent } from '../../nav/side-bar-menu.component';

@Component({
    templateUrl: './theme5-layout.component.html',
    selector: 'theme5-layout',
    animations: [appModuleAnimation()]
})
export class Theme5LayoutComponent extends ThemesLayoutBaseComponent implements OnInit, AfterViewInit {
    @ViewChild(SideBarMenuComponent, { static: false }) sideBar: SideBarMenuComponent
    @ViewChild('ktHeader', { static: true }) ktHeader: ElementRef;
    topId: number;
    remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;

    menuCanvasOptions: OffcanvasOptions = {
        baseClass: 'kt-aside',
        overlay: true,
        closeBy: 'kt_aside_close_btn',
        toggleBy: {
            target: 'kt_aside_desktop_toggler',
            state: 'kt-header-desktop__toolbar-toggler--active'
        }
    };

    constructor(
        injector: Injector,
        private layoutRefService: LayoutRefService
    ) {
        super(injector);
    }

    getTopId(id: number){
        this.topId = id;
        this.sideBar.ngOnInit(this.topId);
        console.log(this.topId);
    }

    ngOnInit() {
        this.installationMode = UrlHelper.isInstallUrl(location.href);
    }

    ngAfterViewInit(): void {
        this.layoutRefService.addElement('header', this.ktHeader.nativeElement);
    }
}

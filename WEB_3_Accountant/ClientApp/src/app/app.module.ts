import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { SingleCardComponent } from './layouts/single-card/single-card.component';
import { SideNavInnerToolbarComponent } from './layouts/side-nav-inner-toolbar/side-nav-inner-toolbar.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { SideNavigationMenuComponent } from './shared/components';
import { ScreenService, AppInfoService } from './shared/services';
import { DxScrollViewModule } from 'devextreme-angular/ui/scroll-view';
import { DxDrawerModule } from 'devextreme-angular/ui/drawer';
import { DxToolbarModule } from 'devextreme-angular/ui/toolbar';
import { CommonModule } from '@angular/common';
import { DxTreeViewModule } from 'devextreme-angular/ui/tree-view';
import { SideNavOuterToolbarComponent } from './layouts';

@NgModule({
  declarations: [
    AppComponent,
    SideNavInnerToolbarComponent,
    SideNavOuterToolbarComponent,
    SideNavigationMenuComponent,
    SingleCardComponent,
    FooterComponent,
    HeaderComponent,
  ],
  imports:
  [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DxScrollViewModule,
    DxDrawerModule,
    DxToolbarModule,
    DxScrollViewModule,
    CommonModule,
    DxTreeViewModule
  ],
  providers: [
    ScreenService,
    AppInfoService,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { ContactComponent } from './contact/contact';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CurrentPositionComponent } from './current-position/current-position';
import { EducationComponent } from './education/education';
import { HomeComponent } from './home/home';
import { SkillsComponent } from './skills/skills';

@NgModule({
  declarations: [
    App,
    ContactComponent,
    CurrentPositionComponent,
    EducationComponent,
    HomeComponent,
    SkillsComponent
  ],
  imports: [BrowserModule, AppRoutingModule],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [App],
})
export class AppModule {}

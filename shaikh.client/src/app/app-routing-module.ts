import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './contact/contact';
import { CurrentPositionComponent } from './current-position/current-position';
import { EducationComponent } from './education/education';
import { HomeComponent } from './home/home';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'current-position', component: CurrentPositionComponent },
  { path: 'education', component: EducationComponent },
  { path: 'home', component: HomeComponent }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

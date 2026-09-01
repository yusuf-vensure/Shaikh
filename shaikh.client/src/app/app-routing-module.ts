import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './contact/contact';
import { CurrentPositionComponent } from './current-position/current-position';
import { EducationComponent } from './education/education';

const routes: Routes = [
  { path: 'contact', component: ContactComponent },
  { path: 'current-position', component: CurrentPositionComponent },
  { path: 'education', component: EducationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

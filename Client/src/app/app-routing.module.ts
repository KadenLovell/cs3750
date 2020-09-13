import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppTestComponent } from './app-test/app-test.component';

const routes: Routes = [
  { path: 'test', component: AppTestComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

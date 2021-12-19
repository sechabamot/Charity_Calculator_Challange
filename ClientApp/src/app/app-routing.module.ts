import { CalculationPageComponent } from './components/pages/calculation-page/calculation-page.component';
import { HomeComponent } from './components/layouts/home/home.component';
import { LoginPageComponent } from './components/pages/login-page/login-page.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "",
    component: LoginPageComponent,
  },
  {
    path: "home",
    component: HomeComponent,
    children: [
      {
        path: "",
        component: CalculationPageComponent,
      }  
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabled'
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

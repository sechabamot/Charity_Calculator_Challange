import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './components/pages/login-page/login-page.component';
import { FormsModule } from '@angular/forms';
import { AuthGuardService } from './library/services/auth-guard-service';
import { AuthService } from './library/services/auth-service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/layouts/home/home.component';
import { CalculationPageComponent } from './components/pages/calculation-page/calculation-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    HomeComponent,
    CalculationPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [AuthService, AuthGuardService],
  bootstrap: [AppComponent]
})
export class AppModule { }

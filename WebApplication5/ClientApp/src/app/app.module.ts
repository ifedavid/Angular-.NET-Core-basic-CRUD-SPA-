import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { CategoriesComponent } from './categories/categories.component';
import { NgxLoadingModule } from 'ngx-loading';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { MaterialModule } from './material.module';
import { DailySpendingsComponent } from './daily-spendings/daily-spendings.component';
import { StatsComponent } from './stats/stats.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { FooterComponent } from './footer/footer.component';
import { DemoDashboardComponent } from './demo-dashboard/demo-dashboard.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatIconModule } from '@angular/material/icon';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { FullCalendarModule } from '@fullcalendar/angular';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';





  let config = new AuthServiceConfig([
    {
      id: GoogleLoginProvider.PROVIDER_ID,
      provider: new GoogleLoginProvider('233670777619-6m2r1t4bedr0au7tuf8betgmu3vqp79p.apps.googleusercontent.com')
    },
    {
      id: FacebookLoginProvider.PROVIDER_ID,
      provider: new FacebookLoginProvider('2738895199456484')
    },
  ]);


export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    CategoriesComponent,
    DailySpendingsComponent,
    StatsComponent,
    FooterComponent,
    DemoDashboardComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SocialLoginModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.circle,
      backdropBackgroundColour: 'rgba(192,192,192,0.4)',
      backdropBorderRadius: '4px',
      primaryColour: '#64b2cd',
      secondaryColour: '#ffffff',
      tertiaryColour: '#ffffff'
  }),
  MaterialModule,
    GoogleChartsModule,
    MatIconModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
    FullCalendarModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    {
    provide: AuthServiceConfig,
    useFactory: provideConfig
    }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }

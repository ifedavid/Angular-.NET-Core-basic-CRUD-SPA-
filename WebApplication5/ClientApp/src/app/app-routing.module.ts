import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

import { CategoriesComponent } from './categories/categories.component';

import { DailySpendingsComponent } from './daily-spendings/daily-spendings.component';
import { StatsComponent } from './stats/stats.component';
import { DemoDashboardComponent } from './demo-dashboard/demo-dashboard.component';
import { DashboardComponent } from './dashboard/dashboard.component';





const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot([
    {path: 'home' , component: HomeComponent},
    {path: '', component: HomeComponent, pathMatch: 'full' },
    {path: 'categories/:id', component: CategoriesComponent},
    {path: 'DailyRecords', component: DailySpendingsComponent},
    { path: 'Stats/:id', component: StatsComponent },
    { path: 'demo', component: DemoDashboardComponent },
    { path: 'Dashboard', component: DashboardComponent },
    {path: '**', redirectTo: '/home'},

  ])],
  exports: [RouterModule]
})
export class AppRoutingModule { }

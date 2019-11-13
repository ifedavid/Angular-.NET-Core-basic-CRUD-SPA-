import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

import { CategoriesComponent } from './categories/categories.component';

import { DailySpendingsComponent } from './daily-spendings/daily-spendings.component';
import { StatsComponent } from './stats/stats.component';




const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot([
    {path: 'home' , component: HomeComponent},
    {path: '', component: HomeComponent, pathMatch: 'full' },
    {path: 'login', component: LoginComponent},
    {path: 'categories', component: CategoriesComponent},
    {path: 'spendings', component: DailySpendingsComponent},
    {path: 'stats', component: StatsComponent},
    {path: '**', redirectTo: '/home'},

  ])],
  exports: [RouterModule]
})
export class AppRoutingModule { }

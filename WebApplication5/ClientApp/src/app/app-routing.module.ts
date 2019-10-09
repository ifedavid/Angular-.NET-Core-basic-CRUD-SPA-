import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ToursComponent } from './tours/tours.component';
import { TourPlannersComponent } from './tour-planners/tour-planners.component';



const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot([
    {path: 'home' , component: HomeComponent},
    {path: '', component: HomeComponent, pathMatch: 'full' },
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'tours', component: ToursComponent},
    {path: 'tourplanners', component: TourPlannersComponent},
    {path: '**', redirectTo: '/home'},



  ])],
  exports: [RouterModule]
})
export class AppRoutingModule { }

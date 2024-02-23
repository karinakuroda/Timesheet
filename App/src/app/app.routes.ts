import { Routes } from '@angular/router';
import { AppointmentComponent } from './appointment/appointment.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

export const routes: Routes = [
    {
        path:'',
        component: HomeComponent,
        title:'Home page'
    },
    {
        path:'appointment',
        component: AppointmentComponent,
        title:'Appointment'
    }
];

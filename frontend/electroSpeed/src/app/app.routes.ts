import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
 import { BaseLoginRegistroComponent } from './pages/base-login-registro/base-login-registro.component'; 
import { LoginComponent } from './pages/base-login-registro/login/login.component';
import { RegistroComponent } from './pages/base-login-registro/registro/registro.component';
import { Component } from '@angular/core';
import { LoginRegistroComponent } from './pages/login-registro/login-registro.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'f', component: BaseLoginRegistroComponent}, 
    {path: 'login', component: LoginComponent},
    {path: 'registro', component: RegistroComponent},
    {path: 'logreg', component: LoginRegistroComponent},
];

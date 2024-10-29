import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { Component } from '@angular/core';
import { LoginRegistroComponent } from './pages/login-registro/login-registro.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'logreg', component: LoginRegistroComponent},
];

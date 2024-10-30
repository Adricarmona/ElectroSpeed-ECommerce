import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { LoginRegistroComponent } from './pages/login-registro/login-registro.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'logreg', component: LoginRegistroComponent},
];

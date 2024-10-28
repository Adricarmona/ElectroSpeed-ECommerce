import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { BaseLoginRegistroComponent } from './pages/base-login-registro/base-login-registro.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'f', component: BaseLoginRegistroComponent}
];

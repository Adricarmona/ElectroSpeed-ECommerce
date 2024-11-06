import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { BaseLoginRegistroComponent } from './pages/base-login-registro/base-login-registro.component'; 
import { LoginComponent } from './pages/base-login-registro/login/login.component';
import { RegistroComponent } from './pages/base-login-registro/registro/registro.component';
import { SobreNosotrosComponent } from './pages/sobre-nosotros/sobre-nosotros.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
     {path: 'f', component: BaseLoginRegistroComponent}, 
    {path: 'login', component: LoginComponent},
    {path: 'registro', component: RegistroComponent},
    {path: 'sobreNosotros', component: SobreNosotrosComponent}
];

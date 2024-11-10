import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { CatalogoComponent } from './pages/catalogo/catalogo.component';
import { SobreNosotrosComponent } from './pages/sobre-nosotros/sobre-nosotros.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistroComponent } from './pages/registro/registro.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'log', component: LoginComponent},
    {path: 'reg', component: RegistroComponent},
    {path: 'catalogo', component: CatalogoComponent},
    {path: 'catalogo/:id', component: CatalogoComponent},
    {path: 'sobreNosotros', component:  SobreNosotrosComponent}
];

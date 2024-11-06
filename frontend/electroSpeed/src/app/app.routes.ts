import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { LoginRegistroComponent } from './pages/login-registro/login-registro.component';
import { CatalogoComponent } from './pages/catalogo/catalogo.component';
import { SobreNosotrosComponent } from './pages/sobre-nosotros/sobre-nosotros.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'logreg', component: LoginRegistroComponent},
    {path: 'catalogo', component: CatalogoComponent},
    {path: 'catalogo/:id', component: CatalogoComponent},
    {path: 'sobreNosotros', component:  SobreNosotrosComponent}
];

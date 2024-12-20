import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { CatalogoComponent } from './pages/catalogo/catalogo.component';
import { SobreNosotrosComponent } from './pages/sobre-nosotros/sobre-nosotros.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { VistaDetalleComponent } from './pages/vista-detalle/vista-detalle.component';
import { CarritoComponent } from './pages/carrito/carrito.component';
import { StripeComponent } from './pages/stripe/stripe.component';
import { EthereumComponent } from './pages/ethereum/ethereum.component';
import { ConfirmacionCompraComponent } from './pages/confirmacion-compra/confirmacion-compra.component';
import { redirectionGuard } from './guards/redirection.guard/redirection.guard.component';
import { AdministradorComponent } from './pages/administrador/administrador.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';
import { PerfilComponent } from './pages/perfil/perfil.component';

export const routes: Routes = [
    {path: '', component: InicioComponent},
    {path: 'log', component: LoginComponent},
    {path: 'reg', component: RegistroComponent},
    {path: 'catalogo', component: CatalogoComponent},
    {path: 'detalle/:id', component: VistaDetalleComponent},
    {path: 'sobreNosotros', component:  SobreNosotrosComponent},
    {path: 'carrito', component: CarritoComponent},
    {path: 'confirmacion', component: ConfirmacionCompraComponent},
    {path: 'administrador', component: AdministradorComponent, canActivate: [redirectionGuard]},
    {path: 'perfil', component: PerfilComponent, canActivate: [redirectionGuard]},
    {path: 'checkout', component: CheckoutComponent,  canActivate: [redirectionGuard]}
];

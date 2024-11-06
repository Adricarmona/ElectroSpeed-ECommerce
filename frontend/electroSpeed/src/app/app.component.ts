import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InicioComponent } from "./pages/inicio/inicio.component";
import { BaseLoginRegistroComponent } from './pages/base-login-registro/base-login-registro.component';
import { NavbarBlackComponent } from "./pages/navbar-black/navbar-black.component";
import { NavbarWhiteComponent } from "./pages/navbar-white/navbar-white.component";
import { ServiciosComponent } from "./pages/inicio/servicios/servicios.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, InicioComponent, BaseLoginRegistroComponent, NavbarBlackComponent, NavbarWhiteComponent, ServiciosComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'electroSpeed';
}

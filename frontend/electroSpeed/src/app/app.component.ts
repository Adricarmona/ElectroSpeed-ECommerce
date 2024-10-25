import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InicioComponent } from "./pages/inicio/inicio.component";
import { BaseLoginRegistroComponent } from './pages/base-login-registro/base-login-registro.component';
import { NavbarComponent } from "./page/navbar/navbar.component";
import { ServiciosComponent } from "./pages/inicio/servicios/servicios.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, InicioComponent, BaseLoginRegistroComponent, NavbarComponent, ServiciosComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'electroSpeed';
}

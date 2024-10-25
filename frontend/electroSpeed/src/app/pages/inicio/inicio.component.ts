import { Component } from '@angular/core';
import { TituloComponent } from "./titulo/titulo.component";
import { OfrecerMovilidadComponent } from "./ofrecer-movilidad/ofrecer-movilidad.component";
import { DestacadosComponent } from '../inicio/destacados/destacados.component';
import { ServiciosComponent } from "./servicios/servicios.component";
import { MarcasComponent } from "./marcas/marcas.component";
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [TituloComponent, OfrecerMovilidadComponent, DestacadosComponent, NavbarComponent, ServiciosComponent, MarcasComponent],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

}

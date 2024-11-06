import { Component } from '@angular/core';
import { TituloComponent } from "./titulo/titulo.component";
import { OfrecerMovilidadComponent } from "./ofrecer-movilidad/ofrecer-movilidad.component";
import { DestacadosComponent } from '../inicio/destacados/destacados.component';
import { ServiciosComponent } from "./servicios/servicios.component";
import { MarcasComponent } from "./marcas/marcas.component";
import { NavbarBlackComponent } from '../navbar-black/navbar-black.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [TituloComponent, OfrecerMovilidadComponent, DestacadosComponent, NavbarBlackComponent, ServiciosComponent, MarcasComponent, FooterComponent],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

}

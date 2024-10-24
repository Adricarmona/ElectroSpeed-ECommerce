import { Component } from '@angular/core';
import { TituloComponent } from "./titulo/titulo.component";
import { OfrecerMovilidadComponent } from "./ofrecer-movilidad/ofrecer-movilidad.component";
import { DestacadosComponent } from '../destacados/destacados.component';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [TituloComponent, OfrecerMovilidadComponent, DestacadosComponent],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

}

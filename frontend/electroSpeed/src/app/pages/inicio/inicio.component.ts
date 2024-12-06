import { Component } from '@angular/core';
import { FooterComponent } from '../footer/footer.component';
import { NavbarService } from '../../service/navbar.service';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

  constructor(private navbarService : NavbarService) {
    navbarService.cambiarCss(1)
  }
}

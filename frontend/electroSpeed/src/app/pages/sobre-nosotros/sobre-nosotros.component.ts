import { Component } from '@angular/core';
import { FooterComponent } from '../footer/footer.component';
import { NavbarService } from '../../service/navbar.service';

@Component({
  selector: 'app-sobre-nosotros',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './sobre-nosotros.component.html',
  styleUrl: './sobre-nosotros.component.css'
})
export class SobreNosotrosComponent {
  constructor(private navbarService: NavbarService) {
    navbarService.cambiarCss(4)
  }
}

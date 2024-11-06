import { Component } from '@angular/core';
import { NavbarWhiteComponent } from '../navbar-white/navbar-white.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-sobre-nosotros',
  standalone: true,
  imports: [NavbarWhiteComponent, FooterComponent],
  templateUrl: './sobre-nosotros.component.html',
  styleUrl: './sobre-nosotros.component.css'
})
export class SobreNosotrosComponent {

}

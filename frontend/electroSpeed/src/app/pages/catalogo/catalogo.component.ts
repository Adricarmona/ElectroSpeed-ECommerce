import { Component } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent {
  isDropdownVisible = false;

  constructor(private catalogoService: CatalogoService) {}

  toggleDropdown(): void {
    this.isDropdownVisible = !this.isDropdownVisible;
  }


}

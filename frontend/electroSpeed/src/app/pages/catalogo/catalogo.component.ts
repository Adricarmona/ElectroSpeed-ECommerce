import { Component, Input } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { FormsModule } from '@angular/forms';
import { Filtro } from '../../models/filtro';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent {
  isDropdownVisible = false;

  nombre: boolean = false;
  precio: boolean = false;
  asc: boolean = false;
  desc: boolean = false;

  constructor(private catalogoService: CatalogoService) {}

  toggleDropdown(): void {
    console.log((document.getElementById("categoriaSelect"))?.getAttribute)
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  async submitFiltro() {
/*
    const filtro: Filtro =
    {
        consulta: "",
        criterio: 
    }
*/
  }

}

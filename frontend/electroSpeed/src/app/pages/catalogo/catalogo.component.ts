import { Component, Input, OnInit } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { FormsModule } from '@angular/forms';
import { Filtro } from '../../models/filtro';
import { BiciPagina } from '../../models/bici-pagina';
import { Bicicletas } from '../../models/catalogo';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent implements OnInit {
  isDropdownVisible = false;

  consulta: string = "";
  critero: string = "nombre";
  orden: string = "desc";
  cantidadProductos: number = 10; // esto fijo por que no se si hay que variarlo en el filtro y ahora no me apetece
  paginaActual: number = 1; /// A VER COMO HACEMOS ESTO AHORA

  biciFiltradasTotales: Bicicletas[] = [];
  paginasTotales: number = 0;


  constructor(private catalogoService: CatalogoService) {}

  ngOnInit(): void {
    this.submitFiltro();
  }

  toggleDropdown(): void {
    console.log((document.getElementById("categoriaSelect"))?.getAttribute)
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  async submitFiltro() {

    if (this.cantidadProductos > 99) {
      
    } else{
      const filtro: Filtro =
    {
        consulta: this.consulta,
        criterio: (this.critero == "nombre") ?  0 : 1, // 0 es nombre y 1 precio
        orden: (this.orden == "asc") ?  0 : 1, // 0 es ascendente y 1 descendente
        cantidadProductos: this.cantidadProductos,
        paginaActual: this.paginaActual
    }
      const bicisFiltradas = await this.catalogoService.showBikes(filtro)

    if (bicisFiltradas != null) {
      this.biciFiltradasTotales = bicisFiltradas.bicicletas;
      this.paginasTotales = bicisFiltradas.paginasTotales;
    }
    }

    


  /*
    const filtroSesion = sessionStorage.getItem("filtro")
    if (filtroSesion) {
      const jsonFiltro: Filtro = JSON.parse(filtroSesion)
      console.log(jsonFiltro.consulta)

      filtro.consulta = jsonFiltro.consulta
      filtro.cantidadProductos = jsonFiltro.cantidadProductos
      filtro.orden = jsonFiltro.orden
      filtro.cantidadProductos = jsonFiltro.cantidadProductos
      filtro.paginaActual = jsonFiltro.paginaActual
    } else {
      sessionStorage.setItem("filtro", JSON.stringify(filtro))
    }
  */

    

  }


  goPrevious(): void {
    if (this.paginaActual > 1) {
      this.paginaActual--;
      this.submitFiltro();
    }
  }

  goNext(): void {
    if (this.paginaActual < this.paginasTotales) {
      this.paginaActual++;
      this.submitFiltro();
    }
  }

  goToPage(page: number): void {
      this.paginaActual = page;
      this.submitFiltro();
  } 
    


}

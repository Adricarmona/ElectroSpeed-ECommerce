import { Component } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { CarritoService } from '../../service/carrito.service';
import { Bicicletas } from '../../models/catalogo';

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [],
  templateUrl: './carrito.component.html',
  styleUrl: './carrito.component.css',
})
export class CarritoComponent {
  constructor(
    private catalogoService: CatalogoService,
    private carritoService: CarritoService
  ) {}
  codigoIdentificador: string[] = [];

  nombreModelo: string = 'Modelo bicicleta - Marca bicicleta';
  precioBici: number = 1;
  fotoBici: string = '';
  idUser: number = 0;
  bicicletaCarrito : Bicicletas[] = []

  async ngOnInit() {
    const iddata = localStorage.getItem('idbici');
    const tokenDataSession = sessionStorage.getItem('token');
    const tokenDataLocal = localStorage.getItem('token');
    if (iddata || tokenDataSession || tokenDataLocal) {
      const ids = iddata ? iddata.split(',').map((id) => JSON.parse(id)) : [];
      if (tokenDataLocal) {
        this.idUser = Number(tokenDataLocal!);
      } else {
        this.idUser = Number(tokenDataSession!);
      }

      const carrito = await this.carritoService.getIdCarrito(this.idUser);

      this.codigoIdentificador = ids;

      for (const id of this.codigoIdentificador) {
        const bicicleta = await this.catalogoService.showOneBike(id);
        if (bicicleta) {
          this.bicicletaCarrito.push(bicicleta);
        } else {
          console.log(`No se encontró bicicleta con ID ${id}`);
        }
      }
    }
  }
}

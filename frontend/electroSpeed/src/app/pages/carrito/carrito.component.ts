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
  codigoIdentificadorLogueado: number[] = [];
  nombreModelo: string = 'Modelo bicicleta - Marca bicicleta';
  precioBici: number = 1;
  fotoBici: string = '';
  idUser: number = 0;
  cantidad: number = 0;
  bicicletaCarrito: Bicicletas[] = [];
  bicicletaCarrito2: Bicicletas[] = [];

  async ngOnInit() {
    const iddata = localStorage.getItem('idbici');
    const tokenDataSession = sessionStorage.getItem('token');
    const tokenDataLocal = localStorage.getItem('token');
    if (iddata || tokenDataSession || tokenDataLocal) {
      const ids = iddata ? iddata.split(',').map((id) => id.trim()) : [];

      if (tokenDataLocal) {
        this.idUser = Number(tokenDataLocal!);
      } else {
        this.idUser = Number(tokenDataSession!);
      }

      const carrito = await this.carritoService.getIdCarrito(this.idUser);

      this.codigoIdentificador = ids;
      if (carrito) {
        this.bicicletaCarrito2 = carrito.idBici
        this.bicicletaCarrito2.forEach(item => {
          this.codigoIdentificadorLogueado = [1, 2, 3, 4]
        });
      }

      //no esta logueado

      for (const id of this.codigoIdentificador) {
        const bicicleta = await this.catalogoService.showOneBike(id);
        if (bicicleta) {
          if (this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)) {

          } 
          this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)
          this.bicicletaCarrito.push(bicicleta);
        } else {
          console.log(`No se encontró bicicleta con ID ${id}`);
        }
      }

      //esta logueado

      for (const id2 of this.codigoIdentificadorLogueado) {
        const bicicleta = await this.catalogoService.showOneBike(id2.toString());
        if (bicicleta) {
          if (this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)) {

          } 
          this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)
          this.bicicletaCarrito.push(bicicleta);
        } else {
          console.log(`No se encontró bicicleta con ID ${id2}`);
        }
      }
      
    }
  }

  calcularTotal(): number {
    return this.bicicletaCarrito.reduce((total, bici) => total + bici.precio, 0);
  }

  eliminarBici(id: number){
    this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id !== id);
    const idsUpdated = this.bicicletaCarrito.map((bici) => bici.id)
    if (localStorage.getItem('idbici')) {
      localStorage.setItem('idbici', JSON.stringify(idsUpdated));
    }else{

    }
    
  }
}



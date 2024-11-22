import { Component } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { CarritoService } from '../../service/carrito.service';
import { Bicicletas } from '../../models/catalogo';
import { AuthService } from '../../service/auth.service';
import { CarritoEntero } from '../../models/carrito-entero';
import { BicisCantidad } from '../../models/bicis-cantidad';

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
    private carritoService: CarritoService,
    private auth: AuthService
  ) {}
  
  codigoIdentificador: string[] = [];
  codigoIdentificadorLogueado: number[] = [];
  nombreModelo: string = 'Modelo bicicleta - Marca bicicleta';
  precioBici: number = 1;
  fotoBici: string = '';
  idUser: number;
  idBicicleta: number;
  cantidad: number = 0;
  idCarrito: number = 0;
  bicicletaCarrito: Bicicletas[] = [];
  bicicletaCarrito2: BicisCantidad[] = [];

  async ngOnInit() {
    const iddata = localStorage.getItem('idbici');
    const tokenDataSession = sessionStorage.getItem('token');
    const tokenDataLocal = localStorage.getItem('token');
    if (iddata || tokenDataSession || tokenDataLocal) {
      const ids = iddata ? iddata.split(',').map((id) => id.trim()) : [];

      this.idUser = await (await this.auth.getIdUserEmail(this.auth.getEmailUserToken())).id

      console.log(this.idUser)
      const carrito = await this.carritoService.getIdCarrito(this.idUser);
      this.bicicletaCarrito2 = carrito.bicisCantidad;
      console.log(carrito)
      this.codigoIdentificador = ids;

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

      for (const id2 of this.bicicletaCarrito2) {
        const bicicleta = await this.catalogoService.showOneBike(id2.idBici.toString());
        if (bicicleta) {
          bicicleta.cantidad = id2.cantidad
          this.idCarrito = id2.idCarrito
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
    return this.bicicletaCarrito.reduce((total, bici) => total + bici.precio * bici.cantidad, 0);
  }

  eliminarBici(id: number){
    this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id !== id);
    const idsUpdated = this.bicicletaCarrito.map((bici) => bici.id)
    if (localStorage.getItem('token') !== null || sessionStorage.getItem('token') !== null) {
      console.log(this.idCarrito)
      this.carritoService.borrarBiciCarrito(this.idCarrito, id)
    }else{
      localStorage.setItem('idbici', JSON.stringify(idsUpdated));
    }
    
  }
}



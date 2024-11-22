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
  ) { }

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
  carro : CarritoEntero

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

      this.imprimirBici();

    }
  }

  calcularTotal(): number {
    return this.bicicletaCarrito.reduce((total, bici) => total + bici.precio * bici.cantidad, 0);
  }

  async eliminarBici(id: number) {
    try {
        if (localStorage.getItem('token') !== null || sessionStorage.getItem('token') !== null) {
            // Llamar al servicio para eliminar la bicicleta en el servidor
            await this.carritoService.borrarBiciCarrito(this.idCarrito, id);
            console.log(`Bicicleta con ID ${id} eliminada del servidor.`);

            // Recargar el carrito actualizado desde el servidor
            this.carro = await this.carritoService.devolverCarritoPorUsuario(this.idUser);
            this.bicicletaCarrito2 = this.carro.bicisCantidad;

            // Actualizar la lista de bicicletas locales
            this.bicicletaCarrito = [];
            this.imprimirBici();

            console.log('Carrito actualizado:', this.bicicletaCarrito);
        } else {
            // Lógica para usuarios no logueados
            this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id !== id);
            const idsUpdated = this.bicicletaCarrito.map((bici) => bici.id);
            localStorage.setItem('idbici', JSON.stringify(idsUpdated));
            console.log(`Bicicleta con ID ${id} eliminada localmente.`);
        }
    } catch (error) {
        console.error(`Error al eliminar la bicicleta con ID ${id}:`, error);
    }
}



  async imprimirBici(){
    for (const id2 of this.bicicletaCarrito2) {
      const bicicleta = await this.catalogoService.showOneBike(id2.idBici.toString());
      if (bicicleta) {
        bicicleta.cantidad = id2.cantidad
        this.idCarrito = id2.idCarrito
        this.idBicicleta = id2.idBici
        if (this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)) {

        }
        this.bicicletaCarrito = this.bicicletaCarrito.filter(bicicleta => bicicleta.id == bicicleta.id)
        await this.bicicletaCarrito.push(bicicleta);
      } else {
        console.log(`No se encontró bicicleta con ID ${id2}`);
      }
    }
  }

  async eliminarYPintar(){
    await this.eliminarBici(this.idBicicleta);
    await this.imprimirBici();
  }
}



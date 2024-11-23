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
  bicicletas: Bicicletas[] = [];
  bicicletaCarrito: BicisCantidad[] = [];
  carro : CarritoEntero

  async ngOnInit() {
    
    // si no existe el usuario coge las bicis del local storage
    if (!this.auth.getToken()) {
      this.cogerBicisLocalStorage()
    } else {
      // coge el id del usuario y con el id de usuario lo guarda en bicicleta carito 2
      this.idUser = await (await this.auth.getIdUserEmail(this.auth.getEmailUserToken())).id
      this.bicicletaCarrito = (await this.carritoService.getIdCarrito(this.idUser)).bicisCantidad;
    }

      this.imprimirBici();
  }

  async cogerBicisLocalStorage(){
    // coge las bicis del local storage
    const iddata = localStorage.getItem('idbici');

    // divide las bicis de el localstorage
    this.codigoIdentificador = iddata ? iddata.split(',').map((id) => id.trim()) : [];

    // se mete en un for each para coger las bicis y meterlas en bicicletaCarrito
    // que es la variable donde estan todas las bicis del carrito 
    for (const id of this.codigoIdentificador) {
      const bicicleta = await this.catalogoService.showOneBike(id);
      if (bicicleta) {
        this.bicicletas = this.bicicletas.filter(bicicleta => bicicleta.id == bicicleta.id)
        this.bicicletas.push(bicicleta);
      } else {
        console.log(`No se encontró bicicleta con ID ${id}`);
      }
    }
  }

  calcularTotal(): number {
    return this.bicicletas.reduce((total, bici) => total + bici.precio * bici.cantidad, 0);
  }

  async eliminarBici(id: number) {
    try {
        if (localStorage.getItem('token') !== null || sessionStorage.getItem('token') !== null) {
            // Llamar al servicio para eliminar la bicicleta en el servidor
            await this.carritoService.borrarBiciCarrito(this.idCarrito, id);

            // Recargar el carrito actualizado desde el servidor
            this.carro = await this.carritoService.devolverCarritoPorUsuario(this.idUser);
            this.bicicletaCarrito = this.carro.bicisCantidad;

            // Actualizar la lista de bicicletas locales
            this.bicicletas = [];
            this.imprimirBici();

            console.log('Carrito actualizado:', this.bicicletas);
        } else {
            // Lógica para usuarios no logueados
            this.bicicletas = this.bicicletas.filter(bicicleta => bicicleta.id !== id);
            const idsUpdated = this.bicicletas.map((bici) => bici.id);
            localStorage.setItem('idbici', JSON.stringify(idsUpdated));
            this.bicicletas = [];
        }
    } catch (error) {
        console.error(`Error al eliminar la bicicleta con ID ${id}:`, error);
    }
}



  async imprimirBici(){
    for (const id2 of this.bicicletaCarrito) {
      const bicicleta = await this.catalogoService.showOneBike(id2.idBici.toString());
      if (bicicleta) {
        bicicleta.cantidad = id2.cantidad
        this.idCarrito = id2.idCarrito
        this.idBicicleta = id2.idBici
        if (this.bicicletas.filter(bicicleta => bicicleta.id == bicicleta.id)) {

        }
        this.bicicletas = this.bicicletas.filter(bicicleta => bicicleta.id == bicicleta.id)
        await this.bicicletas.push(bicicleta);
      } else {
        console.log(`No se encontró bicicleta con ID ${id2}`);
      }
    }
  }

  async eliminarYPintar(idBicis: number){
    await this.eliminarBici(idBicis);
    await this.imprimirBici();
  }
}



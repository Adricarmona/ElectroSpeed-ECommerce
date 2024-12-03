import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { CarritoService } from './carrito.service';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  nombre = ""
  productosCarrito = false

  constructor(
    private authService: AuthService,
    private carritoService: CarritoService
  ) { }

  async pintarNombre() {
    await this.authService.getNameUserToken()
    this.nombre = this.authService.Usuarios.name
  }

  async cogerProductos(){
    const bicisLocal = localStorage.getItem('idbici')
    if (this.authService.loged()) {
      const bicisRemoto = await this.carritoService.devolverCarritoPorUsuario(await this.authService.getIdUser())
      if (bicisRemoto.bicisCantidad.length > 0) {
        this.productosCarrito = true
      } else {
        this.productosCarrito = false
      }
    } else {
      if (bicisLocal.length > 0) {
        this.productosCarrito = true
      } else {
        this.productosCarrito = false
      }
    }
  }

}

import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { CarritoService } from './carrito.service';
import { NavbarComponent } from '../pages/navbar/navbar.component';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  nombre = ""
  productosCarrito = false
  productosCarritoCantidad = 0
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
        this.productosCarritoCantidad = bicisRemoto.bicisCantidad.length
      } else {
        this.productosCarrito = false
      }
    } else {
      if (bicisLocal.length > 0) {
        this.productosCarrito = true
        this.productosCarritoCantidad = bicisLocal.length
      } else {
        this.productosCarrito = false
      }
    }

    return this.productosCarrito
  }

  cambiarCss(pagina: number) { // inicio = 1 , productos = 2, carrito = 3, Sobre nosotros = 4
    // ids los a de los id
    const inicio = document.getElementById("inicio")
    const productos = document.getElementById("productos")
    const carrito = document.getElementById("carrito")
    const sobreNosotros = document.getElementById("sobreNosotros")

    // eliminamos todas las barras
    inicio.classList.remove("active")
    productos.classList.remove("active")
    carrito.classList.remove("active")
    sobreNosotros.classList.remove("active")

    // añadimos la bara en el que deseemos
    switch (pagina) {
      case 1:
        inicio.classList.add("active")
      break;
      case 2:
        productos.classList.add("active")
        break;
      case 3:
        carrito.classList.add("active")
        break;
      case 4:
        sobreNosotros.classList.add("active")
        break;
    
      default:
        break;
    }
  }

}

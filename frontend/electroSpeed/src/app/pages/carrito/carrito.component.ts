import { Component} from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { CarritoService } from '../../service/carrito.service';
import { Bicicletas } from '../../models/catalogo';
import { AuthService } from '../../service/auth.service';
import { CarritoEntero } from '../../models/carrito-entero';
import { BicisCantidad } from '../../models/bicis-cantidad';
import { NavbarService } from '../../service/navbar.service';
import { Carrito } from '../../models/carrito';
import { OrdenTemporal } from '../../models/orden-temporal';
import { BiciTemporal } from '../../models/bici-temporal';
import { CheckoutService } from '../../service/checkout.service';
import { Router } from '@angular/router';

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
    private auth: AuthService,
    private navBar: NavbarService,
    private router: Router,
    private checkoutservice: CheckoutService,
  ) { 
    navBar.cambiarCss(3)
  }

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
  carro : CarritoEntero;


  async ngOnInit() {
    
    // si no existe el usuario coge las bicis del local storage
    if (!this.auth.getToken()) {
      await this.cogerBicisLocalStorage()
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
    var encontrado = false; 
    for (const id of this.codigoIdentificador) {
      const bicicleta: Bicicletas = await this.catalogoService.showOneBike(id);
      if (bicicleta) {

        // recorre las bicis del local storage por si ya existen
        this.bicicletas.forEach(bisi => {
          if (bisi.id == bicicleta.id) {
            bisi.cantidad++
            encontrado = true
          }
        });

        // si no la encuentra le da cantidad 1 y la mete en el array
        if (!encontrado) {
          bicicleta.cantidad = 1
          this.bicicletas.push(bicicleta);
        }

      } else {
        console.log(`No se encontró bicicleta con ID ${id}`);
      }
      encontrado = false
    }
  }

  calcularTotal(): number {
    return this.bicicletas.reduce((total, bici) => total + bici.precio * bici.cantidad, 0);
  }

  async eliminarBici(id: number) {
    try {
        if (this.usuarioToken() != null) {
            // Llamar al servicio para eliminar la bicicleta en el servidor
            await this.carritoService.borrarBiciCarrito(this.idCarrito, id).then();
          
            // Recargar el carrito actualizado desde el servidor
            this.carro = await this.carritoService.devolverCarritoPorUsuario(this.idUser);
            console.log(this.carro)
            this.bicicletaCarrito = this.carro.bicisCantidad;

            // Actualizar la lista de bicicletas locales
            this.bicicletas = [];
        } else {
            // Lógica para usuarios no logueados

            const index = this.codigoIdentificador.findIndex(bici => bici == id.toString());
            this.codigoIdentificador.splice(index, 1);

            // limpiamos el localstorage y le metemos los datos
            localStorage.clear()
            localStorage.setItem('idbici',this.codigoIdentificador.toString())

            this.bicicletas = [];
            // importante borro las bicis y luego las guardo en el array (llevo 1h con esto xdd)
            await this.cogerBicisLocalStorage()

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
    this.navBar.cogerProductos()
    await this.imprimirBici();
  }

  usuarioToken() {
    const token = this.auth.getToken()
    return token
  }

  carritoVacio() {
    //console.log(this.codigoIdentificador)
    if (this.bicicletaCarrito.length > 0 || this.codigoIdentificador.length > 0) {
      return false
    }
    return true
  }

  async crearOrdenTemporalLocal(){
    var carrito = localStorage.getItem("idbici")
    var result  = await this.checkoutservice.postOrdenTemporalLocal(carrito)
    return result.data
  }

  async crearOrdenTemporalCarrito(){
    var result  = await this.checkoutservice.postOrdenTemporalCarrito(this.idUser)
    return result.data
  }

  async goTarjeta() {
    if (this.idUser) {
      var reserva_id = await this.crearOrdenTemporalCarrito();
    }else{
      var reserva_id = await this.crearOrdenTemporalLocal();
    }
    //var reserva_id = await this.crearOrdenTemporalLocal();
    console.log("session_id:  "+reserva_id)
    this.router.navigate(
      ['/checkout'],
      { queryParams: { 'reserva_id': reserva_id, 'metodo_pago': 'stripe' } }
    );
  }

  async goBlockchain() {
    if (this.idUser) {
      var reserva_id = await this.crearOrdenTemporalCarrito();
    }else{
      var reserva_id = await this.crearOrdenTemporalLocal();
    }
    //var reserva_id = await this.crearOrdenTemporalLocal();
    console.log("session_id:  "+reserva_id)
    this.router.navigate(
      ['/checkout'],
      { queryParams: { 'reserva_id': reserva_id, 'metodo_pago': 'ethereum' } }
    );
}
}

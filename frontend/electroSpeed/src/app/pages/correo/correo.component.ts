import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { CarritoService } from '../../service/carrito.service';
import { CarritoEntero } from '../../models/carrito-entero';
import { BicisCantidad } from '../../models/bicis-cantidad';
import { ActivatedRoute } from '@angular/router';
import CheckoutService from '../../service/checkout.service';
import { CatalogoService } from '../../service/catalogo.service';
import { Bicicletas } from '../../models/catalogo';

@Component({
  selector: 'app-correo',
  standalone: true,
  imports: [],
  templateUrl: './correo.component.html',
})
export class CorreoComponent {
  constructor(
    private carritoService: CarritoService,
    private otroservice: AuthService,
    private checkoutservice: CheckoutService,
    private route: ActivatedRoute,
    private catalogoService: CatalogoService
  ) {}

  res: string;
  precio: number
  idUsuario: number
  carrito: CarritoEntero
  bicis: Bicicletas[] = []
  nombre: string

  async ngOnInit() {
      this.res = this.route.snapshot.queryParamMap.get('reserva_id');
      this.precio = this.carritoService.getTotal()
      this.idUsuario = await this.otroservice.getIdUser()
  }

  async DevolverOrden(){
    const request = await this.checkoutservice.DevolverOrden(this.res)
    request.data.forEach(e => {
      this.datosBici(e.id)
    });
  }

  async datosBici(id: number){
    const bicicleta = await this.catalogoService.showOneBike(id.toString());
    console.log(bicicleta.cantidad)
    this.bicis.push(bicicleta)
  }

  calcularTotal(): number {
    const total = this.bicis.reduce((total, bici) => total + bici.precio * bici.cantidad, 0);
    this.carritoService.setTotal(total);
    return total;
  }
}

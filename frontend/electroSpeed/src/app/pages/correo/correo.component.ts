import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { CarritoService } from '../../service/carrito.service';
import { CarritoEntero } from '../../models/carrito-entero';
import { BicisCantidad } from '../../models/bicis-cantidad';

@Component({
  selector: 'app-correo',
  standalone: true,
  imports: [],
  templateUrl: './correo.component.html',
  styleUrl: './correo.component.css'
})
export class CorreoComponent {
  constructor(
    private carritoService: CarritoService,
    private otroservice: AuthService
  ) {}

  precio: number
  idUsuario: number
  carrito: CarritoEntero
  bicis: BicisCantidad[]

  async ngOnInit() {
      this.precio = this.carritoService.getTotal()
      this.idUsuario = await this.otroservice.getIdUser()
      this.carrito = await this.carritoService.devolverCarritoPorUsuario(this.idUsuario)
      this.bicis = this.carrito.bicisCantidad

  }
}

import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { CarritoService } from '../../service/carrito.service';

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

  async ngOnInit() {
      this.precio = this.carritoService.getTotal()
  }
}

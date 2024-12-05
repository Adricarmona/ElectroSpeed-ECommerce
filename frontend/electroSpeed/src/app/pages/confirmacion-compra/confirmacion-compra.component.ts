import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Bicicletas } from '../../models/catalogo';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-confirmacion-compra',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './confirmacion-compra.component.html',
  styleUrl: './confirmacion-compra.component.css'
})
export class ConfirmacionCompraComponent {

  nombre: string = ""

  tipoPago = "targeta"
  entrega = "mi casa"

  productos: Bicicletas[] = [{
    id: 0,
    marcaModelo: 'nombreBisi',
    descripcion: '',
    precio: 400,
    stock: 0,
    urlImg: 'https://carbonbikes.es/cdn/shop/files/biciamarillanegra_800x.jpg?v=1694606380',
    cantidad: 5
  },
  {
    id: 0,
    marcaModelo: 'nombreBisi',
    descripcion: '',
    precio: 400,
    stock: 0,
    urlImg: 'https://carbonbikes.es/cdn/shop/files/biciamarillanegra_800x.jpg?v=1694606380',
    cantidad: 5
  },
]

  constructor(private authService: AuthService) {
    this.devolverNombre()
  }

  async devolverNombre() {
    this.nombre = await this.authService.getNameUser()
  }
}

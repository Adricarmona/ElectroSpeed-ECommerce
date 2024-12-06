import { Component } from '@angular/core';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-administrador',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './administrador.component.html',
  styleUrl: './administrador.component.css'
})
export class AdministradorComponent {

  // bicicleta
  nombreYMarca = "nombreYMarca"
  descripcion = "descripcion"
  precio = 0
  stock  = 0
  foto = "administrador/chinoEnBici.gif"

  // usuario
  id = 0
  name = "nombre"
  email = "email"
  direccion = "direccion"


  // usuario o bici
  usuarioOBici: string = "usuario"
}
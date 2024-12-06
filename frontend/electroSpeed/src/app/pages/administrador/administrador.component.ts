import { Component } from '@angular/core';
import { FooterComponent } from '../footer/footer.component';
import { Bicicletas } from '../../models/catalogo';
import { Usuarios } from '../../models/usuarios';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-administrador',
  standalone: true,
  imports: [FooterComponent,FormsModule],
  templateUrl: './administrador.component.html',
  styleUrl: './administrador.component.css'
})
export class AdministradorComponent {

  boolean: boolean = true

  // bicicleta
  bicicletaSeleccionada: Bicicletas = {
    id: 0,
    marcaModelo: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  }

  // usuario
  usuarioSeleccionado: Usuarios = {
    id: 1,
    name: 'nombre',
    username: 'usuario',
    email: 'correo',
    rol: true
  }

  // bicicletas
  bicicletasTotales: Bicicletas[] = [{
    id: 0,
    marcaModelo: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  },
  {
    id: 0,
    marcaModelo: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  }]

  bicicletasFiltradas: Bicicletas[] = [{
    id: 0,
    marcaModelo: 'marcaModelo',
    descripcion: '',
    precio: 0,
    stock: 0,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  },
  {
    id: 0,
    marcaModelo: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  }]


  // usuarios

  usuariosTotales: Usuarios[] = [{
    id: 1,
    name: 'nombre',
    username: 'usuario',
    email: 'correo',
    rol: false
  },
  {
    id: 2,
    name: 'nombre',
    username: 'usuario',
    email: 'correo',
    rol: true
  }]

  usuariosFiltrados: Usuarios[] = [{
    id: 1,
    name: 'nombre',
    username: 'usuario',
    email: 'correo',
    rol: false
  },
  {
    id: 2,
    name: 'nombre',
    username: 'usuario',
    email: 'correo',
    rol: true
  }]

    // usuario o bici
    usuarioOBici: number = 0 // 0 usuario , 1 bici
    seleccionado: number = 0 // si clica en un usuario o bici (como arriba)

  cambiarUsuarioBici(){
    if (this.usuarioOBici == 1) {
      this.usuarioOBici = 0
    } else {
      this.usuarioOBici = 1
    }
  }
}
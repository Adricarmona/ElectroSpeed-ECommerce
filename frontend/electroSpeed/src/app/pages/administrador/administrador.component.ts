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
    id: 0,
    name: '',
    username: '',
    email: '',
    rol: false
  }



  // bicicletas
  bicicletasTotales: Bicicletas[] = [{
    id: 1,
    marcaModelo: 'marcaModelo',
    descripcion: 'descripcion',
    precio: 1,
    stock: 1,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  },
  {
    id: 2,
    marcaModelo: 'marcaModelo1',
    descripcion: 'descripcion1',
    precio: 2,
    stock: 2,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  },
  {
    id: 3,
    marcaModelo: 'marcaModelo2',
    descripcion: 'descripcion2',
    precio: 3,
    stock: 3,
    urlImg: 'administrador/chinoEnBici.gif',
    cantidad: 0
  }]

  bicicletasFiltradas: Bicicletas[]


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
    name: 'nombre1',
    username: 'usuario1',
    email: 'correo1',
    rol: true
  },
  {
    id: 3,
    name: 'nombre2',
    username: 'usuario2',
    email: 'correo2',
    rol: true
  }]

  usuariosFiltrados: Usuarios[]

    // usuario o bici
    usuarioOBici: number = 1 // 0 usuario , 1 bici
    seleccionado: number = 1 // si clica en un usuario o bici (como arriba)

    // id par menu
    idBuscarMenu: number = 1

  ngOnInit(): void {
    this.sumarRestarEjecutar(0)
  }

  cambiarUsuarioBici(){
    if (this.usuarioOBici == 1) { // si es bici
      this.usuarioOBici = 0
    } else {
      this.usuarioOBici = 1
    }

    this.sumarRestarEjecutar(0)
  }

  SeleccionadorBicisUsuarios() {
    if (this.usuarioOBici == 1) { // si es bici

      this.bicicletasFiltradas = []

      this.bicicletasTotales.forEach(bici => {
        if (bici.id == this.idBuscarMenu || bici.id == (this.idBuscarMenu + 1)) {
          this.bicicletasFiltradas.push(bici)
        }
      });

    } else {

      this.usuariosFiltrados = []

      this.usuariosTotales.forEach(bici => {
        if (bici.id == this.idBuscarMenu || bici.id == (this.idBuscarMenu + 1)) {
          this.usuariosFiltrados.push(bici)
        }
      });

    }
  }

  sumarRestarEjecutar(number: number) {

    this.idBuscarMenu = this.idBuscarMenu + number
    
    if (this.idBuscarMenu < 1) {
      this.idBuscarMenu = 1
    }

    this.SeleccionadorBicisUsuarios()
  }

  seleccionarDestacado(idSeleccionado: number, seleccionado: number) {

    this.seleccionado = seleccionado;

    if (this.seleccionado == 0) { // 0 es usuario y 1 es bici

      this.usuariosFiltrados.forEach(usuario =>{

        if(idSeleccionado == usuario.id) {
          this.usuarioSeleccionado = usuario
        }

      })

    } else {
      
      this.bicicletasFiltradas.forEach(bici =>{

        if(idSeleccionado == bici.id) {
          this.bicicletaSeleccionada = bici
        }

      })

    }

  }

}
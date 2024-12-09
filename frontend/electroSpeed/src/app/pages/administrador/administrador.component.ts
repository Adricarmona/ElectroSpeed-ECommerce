import { Component } from '@angular/core';
import { FooterComponent } from '../footer/footer.component';
import { Bicicletas } from '../../models/catalogo';
import { Usuarios } from '../../models/usuarios';
import { FormsModule } from '@angular/forms';
import { CatalogoService } from '../../service/catalogo.service';
import { AuthService } from '../../service/auth.service';
import { NavbarService } from '../../service/navbar.service';

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
    direccion: '',
    email: '',
    admin: false
  }



  // bicicletas
  bicicletasTotales: Bicicletas[] = []
  bicicletasFiltradas: Bicicletas[]


  // usuarios
  usuariosTotales: Usuarios[] = []
  usuariosFiltrados: Usuarios[]

    // usuario o bici
    usuarioOBici: number = 1 // 0 usuario , 1 bici
    seleccionado: number = 3 // si clica en un usuario o bici (como arriba)

    // id par menu
    idBuscarMenu: number = 1

  constructor(
    private catalogoService: CatalogoService,
    private authService: AuthService,
    private navbarService: NavbarService
  ){}

  async ngOnInit(): Promise<void> {
    this.navbarService.cambiarCss(0)

    await this.ObtenerBicis()
    await this.ObtenerUsuarios()

    this.sumarRestarEjecutar(0)
  }


  //
  //  las funciones que hacen que funcione el menu
  //
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


  //
  // utilizando los datos
  //
  async ObtenerBicis(){
    this.bicicletasTotales = await this.catalogoService.todasLasBicis()
  }

  async ObtenerUsuarios() {
    this.usuariosTotales = await this.authService.getUsersDto()
  }

  async EliminarUsuarios(id: number) {
    await this.authService.deleteUser(id)
    await this.ObtenerUsuarios()
    this.sumarRestarEjecutar(0)
  }

  async EditarUsuarios(usuario: Usuarios){
    await this.authService.updateUser(usuario)
    await this.ObtenerUsuarios()
    this.sumarRestarEjecutar(0)
  }
}
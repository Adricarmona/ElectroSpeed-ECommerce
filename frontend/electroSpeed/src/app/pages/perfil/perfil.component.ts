import { Component, OnInit } from '@angular/core';
import { NavbarService } from '../../service/navbar.service';
import { Usuarios } from '../../models/usuarios';
import { BicisCantidad } from '../../models/bicis-cantidad';
import { AuthService } from '../../service/auth.service';
import { FormsModule } from '@angular/forms';
import { Bicicletas } from '../../models/catalogo';
import { CatalogoService } from '../../service/catalogo.service';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {

  // el usuario
  usuario: Usuarios = {
    id: 0,
    name: '',
    direccion: '',
    email: '',
    admin: false
  }

  usuarioEditar: Usuarios = {
    id: 0,
    name: '',
    direccion: '',
    email: '',
    admin: false
  }

  password: string 

  // si que al elegir entre los 3 botones
  opcion: number = 0 // 0 nada, 1 cambiar contraseña, 2 cambiar datos, 3 Eliminar usuario
  pedidos: Bicicletas[] = []
  constructor(
    private navbarService : NavbarService,
    private catalogoService: CatalogoService,
    private authService: AuthService
  ) {
    navbarService.cambiarCss(5)
  }

  async ngOnInit() {
    
    await this.authService.getIdUser()
    this.getPedidos()
    this.usuario = this.authService.Usuarios
    this.usuarioEditar.id = this.usuario.id
    this.usuarioEditar.name = this.usuario.name
    this.usuarioEditar.email = this.usuario.email
    this.usuarioEditar.direccion = this.usuario.direccion
    this.usuarioEditar.admin = this.usuario.admin
  }


  eliminarUsuario() {
    this.authService.deleteUser(this.usuario.id)
    this.vaciarToken()
    alert("Usuario Eliminado")
  }

  vaciarToken() {
    this.authService.setTokenLocal("")
    this.authService.setTokenSesion("")
    location.assign("#")
  }

  actualizarPasword() {
    this.authService.updatePasword(this.usuario.id, this.password)
    alert("Contraseña cambiada")
  }

  async actualizarUsuario() {
    console.log("s8i")
    await this.authService.updateUser(this.usuarioEditar)
    alert("Vuelve a iniciar sesion")
    this.vaciarToken()
  }

  async getPedidos(){
    const request = await this.authService.getPedidos();
    request.data.forEach(e => {
      this.datosBici(e.id)
    });
  }

  async datosBici(id: number){
    const bicicleta = await this.catalogoService.showOneBike(id.toString());
    console.log(bicicleta.cantidad)
    this.pedidos.push(bicicleta)
  }

}

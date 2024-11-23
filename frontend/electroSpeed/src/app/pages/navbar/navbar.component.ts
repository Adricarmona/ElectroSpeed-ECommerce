import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Usuarios } from '../../models/usuarios';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService) { }
  
  usuario: Usuarios = {
    id: 0,
    name: "",
    email: "",
    username: ""
  };

  async ngOnInit(): Promise<void> {
    this.usuario = await this.authService.getIdUserEmail(this.authService.getEmailUserToken())
  }
  /* cogemos el token para ver si existe o quien es */
  usuarioToken() {
    const token = this.authService.getToken()
    return token
  }

  vaciarToken() {
    this.authService.setTokenLocal("")
    this.authService.setTokenSesion("")
    location.reload()
  }

  // Creo que esto no se utiliza pero lo dejo aqui porsi las moscas -adrian
  async nombreToken() {
    const nombreNavBar = document.getElementById("nombreUsuario")
    if (nombreNavBar) {
      nombreNavBar.innerText = this.usuario.name
    }
  }

  desplegable() {
    const desplegable = document.getElementById("desplegableUsuarios")
    if (desplegable != null) {
      const displayStyle = window.getComputedStyle(desplegable).display;
      if (displayStyle == "none") {
        desplegable.style.display = "block"
      } else if (displayStyle == "block") {
        desplegable.style.display = "none"
      }
    }
  }
}

import { query } from '@angular/animations';
import { Block } from '@angular/compiler';
import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service'

@Component({
  selector: 'app-navbar-white',
  standalone: true,
  imports: [],
  templateUrl: './navbar-white.component.html',
  styleUrl: './navbar-white.component.css'
})
export class NavbarWhiteComponent {
  constructor(private authService: AuthService) { }

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

  nombreToken() {
    const nombreNavBar = document.getElementById("nombreUsuario")
    if (nombreNavBar) {
      nombreNavBar.innerText = this.authService.getNameUserToken()
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

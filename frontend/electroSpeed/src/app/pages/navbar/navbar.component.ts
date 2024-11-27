import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Usuarios } from '../../models/usuarios';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService) { }
  
  nombre : string = ""

  async ngOnInit(): Promise<void> {
    this.pintarNombre()
  }

  async pintarNombre() {
    await this.authService.getNameUser()
    this.nombre = this.authService.Usuarios.name
  }


  /* cogemos el token para ver si existe o quien es */
  usuarioToken() {
    const token = this.authService.getToken()
    this.pintarNombre()
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
      nombreNavBar.innerText = this.nombre
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

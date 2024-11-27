import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Usuarios } from '../../models/usuarios';
import { RedirectionService } from '../../service/redirection.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService, private service: RedirectionService,) { }
  
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
    this.service.logout
    this.authService.setTokenLocal("")
    this.authService.setTokenSesion("")
    location.reload()
  }

  // Creo que esto no se utiliza pero lo dejo aqui porsi las moscas -adrian
  async nombreToken() {
    console.log(this.usuario.name)
    return this.usuario.name
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

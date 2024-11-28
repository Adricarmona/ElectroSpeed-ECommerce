import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Usuarios } from '../../models/usuarios';
import { lastValueFrom } from 'rxjs';
import { RedirectionService } from '../../service/redirection.service';
import { NavbarService } from '../../service/navbar.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(
    private authService: AuthService, 
    private service: RedirectionService,
    private navBarService: NavbarService) { }
  
  nombre : string = ""

  async ngOnInit(): Promise<void> {
    if(this.authService.loged()){
      this.nombre = await this.authService.getNameUser()
    }
  }



  //
  //    PARA LOS BOTONES DEL NAVBAR
  //
  /* cogemos el token para ver si existe o quien es */
  usuarioToken() {
    const token = this.authService.getToken()
    //this.pintarNombre()
    return token
  }

  vaciarToken() {
    this.authService.setTokenLocal("")
    this.authService.setTokenSesion("")
    location.reload()
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

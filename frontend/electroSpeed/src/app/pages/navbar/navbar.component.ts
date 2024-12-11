import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Usuarios } from '../../models/usuarios';
import { lastValueFrom } from 'rxjs';
import { RedirectionService } from '../../service/redirection.service';
import { NavbarService } from '../../service/navbar.service';
import { RouterModule } from '@angular/router'; // Importa el RouterModule

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(
    private authService: AuthService, 
    private service: RedirectionService,
    private navBarService: NavbarService) 
    {}
  
  nombre : string = ""
  admin : boolean = false
  productosCarrito = false
  productosCarritoNumero = 0

  async ngOnInit(): Promise<void> {

    await this.objetosNavbar()

    if(this.authService.loged()){
      this.nombre = await this.authService.getNameUser()
      this.admin = await this.authService.getAdminUserToken()
    }
  }

  async ngOnChanges() {
    await this.objetosNavbar()
  }

  //
  //    PARA LOS BOTONES DEL NAVBAR
  //

  /* cogemos productos para la navbar */
  async objetosNavbar(){
    await this.navBarService.cogerProductos()

    this.productosCarrito = this.navBarService.productosCarrito
    this.productosCarritoNumero = this.navBarService.productosCarritoCantidad
  }

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

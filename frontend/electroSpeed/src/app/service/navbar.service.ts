import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  nombre = ""

  constructor(
    private authService: AuthService
  ) { }

  async pintarNombre() {
    await this.authService.getNameUserToken()
    this.nombre = this.authService.Usuarios.name
  }


}

  import { Component } from '@angular/core';
  import { BaseLoginRegistroComponent } from '../base-login-registro.component';
  import { NavbarComponent } from '../../navbar/navbar.component';
  import { AuthService } from '../../../service/auth.service'; // Importa el modelo de usuario
  import { AuthRequest } from '../../../models/auth-request';
  import { Usuarios } from '../../../models/usuarios';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [BaseLoginRegistroComponent, NavbarComponent],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  name: string = 'Godofredo';
  password: string = '123456';
  role: string = 'admin'; 
  jwt: string = '';
  user: Usuarios | null = null;

  constructor(private authService: AuthService) {}

  async submit() {
    const authData: AuthRequest = { username: this.name, password: this.password, role: this.role }; // Cambia a AuthRequest
    const result = await this.authService.login(authData); // Llama al método login

    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      this.user = await this.authService.getUser(this.name); // Llama a getUser para obtener datos del usuario
      console.log('Usuario autenticado:', this.user?.name);
  } else {
      console.error('Error en la autenticación');
  }
  }
}


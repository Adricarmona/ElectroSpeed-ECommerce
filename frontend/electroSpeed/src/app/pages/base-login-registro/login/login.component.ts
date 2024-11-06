  import { Component } from '@angular/core';
  import { BaseLoginRegistroComponent } from '../base-login-registro.component';
  import { NavbarBlackComponent } from '../../navbar-black/navbar-black.component';
  import { AuthService } from '../../../service/auth.service'; // Importa el modelo de usuario
  import { AuthRequest } from '../../../models/auth-request';
  import { Usuarios } from '../../../models/usuarios';
  import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [BaseLoginRegistroComponent, NavbarBlackComponent, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  email: string = '';
  password: string = '';
  jwt: string = '';
  user: Usuarios | null = null;

  constructor(private authService: AuthService) {}

  async submit() {
    const authData: AuthRequest = { username: this.email, password: this.password };
    const result = await this.authService.login(authData); // Llama al método login

    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      this.user = await this.authService.getUser(this.email); // Llama a getUser para obtener datos del usuario
      console.log('Usuario autenticado:', this.user?.name);
    } else {
        console.error('Error en la autenticación');
    }
  }
}


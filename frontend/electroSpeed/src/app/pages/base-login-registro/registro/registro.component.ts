import { Component } from '@angular/core';
import { BaseLoginRegistroComponent } from '../base-login-registro.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../service/auth.service';
import { AuthSend } from '../../../models/auth-send';
import { Usuarios } from '../../../models/usuarios';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [BaseLoginRegistroComponent, FormsModule],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css'
})
export class RegistroComponent {
  email = "";
  username = "";
  fullName = "";
  password = "";
  jwt: string = '';
  user: Usuarios | null = null;

  constructor(private authService: AuthService) {}


  async submit(){
    console.log(this.email);
    console.log(this.username);
    console.log(this.fullName);
    console.log(this.password);
    const registerData: AuthSend = 
    {  
      Name : this.fullName,
      Username: this.username,
      Email: this.email,  
      Password: this.password 
    };  
    const result = await this.authService.register(registerData);
    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      //this.user = await this.authService.getUser(this.username); // Llama a getUser para obtener datos del usuario
      console.log('Usuario autenticado:', this.user?.name);
    } else {
        console.error('El usuario ya existe');
    }
  }

}

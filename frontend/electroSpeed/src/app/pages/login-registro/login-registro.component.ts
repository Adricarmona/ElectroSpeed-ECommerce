import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { AuthSend } from '../../models/auth-send';
import { Usuarios } from '../../models/usuarios';
import { AuthRequest } from '../../models/auth-request';

@Component({
  selector: 'app-login-registro',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login-registro.component.html',
  styleUrl: './login-registro.component.css'
})
export class LoginRegistroComponent {
  

  emailR = "";
  usernameR = "";
  fullNameR = "";
  passwordR = "";

  emailLog = "";
  passwordLog = "";
  jwt: string = '';

  remember: boolean = false;
  user: Usuarios | null = null;

  constructor(private authService: AuthService) {}

  

  ngAfterViewInit(): void {
    const triggerElements = document.querySelectorAll('.trigger');
    const modalWrapper = document.querySelector('.modal-wrapper');
    const pageWrapper = document.querySelector('.page-wrapper');

    triggerElements.forEach(trigger => {
      trigger.addEventListener('click', (event) => {
        event.preventDefault();
        modalWrapper?.classList.toggle('open');
        pageWrapper?.classList.toggle('blur-it');
      });
    });
  }

  async submitLogin() {

    console.log(this.emailLog);
    console.log(this.passwordLog);
    const authData: AuthRequest = { username: this.emailLog, password: this.passwordLog };
    const result = await this.authService.login(authData); // Llama al método login

    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      
      if (this.remember) {
        localStorage.setItem('token', this.jwt);
      } else {
        sessionStorage.setItem('token', this.jwt);
      }

      console.log('Usuario autenticado:', this.user?.name);
    } else {
        console.error('Error en la autenticación');
    }
  }


  async submitRegistro(){
    console.log(this.emailR);
    console.log(this.usernameR);
    console.log(this.fullNameR);
    console.log(this.passwordR);
    const registerData: AuthSend = 
    {  
      Name : this.fullNameR,
      Username: this.usernameR,
      Email: this.emailR,  
      Password: this.passwordR 
    };  
    const result = await this.authService.register(registerData);
    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken

      if (this.remember) {
        localStorage.setItem('token', this.jwt);
      } else {
        sessionStorage.setItem('token', this.jwt);
      }

      console.log('Usuario autenticado:', this.user?.name);
    } else {
        console.error('El usuario ya existe');
    }
  }
}


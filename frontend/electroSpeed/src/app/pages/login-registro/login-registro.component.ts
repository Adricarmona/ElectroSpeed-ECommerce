import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { AuthSend } from '../../models/auth-send';
import { Usuarios } from '../../models/usuarios';
import { AuthRequest } from '../../models/auth-request';

@Component({
  selector: 'app-login-registro',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule ],
  templateUrl: './login-registro.component.html',
  styleUrl: './login-registro.component.css'
})
export class LoginRegistroComponent implements OnInit {

  myForm: FormGroup;
  constructor(private authService: AuthService, public fb: FormBuilder) {
    this.myForm = this.fb.group({
      emailR: ['', [Validators.required, Validators.email]],
      direccionR: ['', [Validators.required]],
      fullNameR: ['', [Validators.required]],
      passwordR: ['', [Validators.required]],
      passwordRepR: ['', [Validators.required]],
      emailLog: ['', [Validators.required, Validators.email]],
      passwordLog: ['', [Validators.required]]
    });
  }
 
  ngOnInit() {}


  jwt: string = '';
  passwordConfirmation: boolean = true;

  remember: boolean = false;
  user: Usuarios | null = null;

  

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

    console.log(this.myForm.get('emailLog')?.value);
    console.log(this.myForm.get('passwordLog')?.value);
    
    const authData: AuthRequest = { 
      email: this.myForm.get('emailLog')?.value, 
      password: this.myForm.get('passwordLog')?.value 
    };

    const result = await this.authService.login(authData); // Llama al método login

    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      
      if (this.remember) {
        localStorage.setItem('token', this.jwt);
      } else {
        sessionStorage.setItem('token', this.jwt);
      }

    } else {
        console.error('Error en la autenticación');
    }
  }


  async submitRegistro(){

    if(this.myForm.get('passwordR')?.value != this.myForm.get('passwordRepR')?.value){
      this.passwordConfirmation = false;
      return;
    }

    const registerData: AuthSend = 
    {  
      Name : this.myForm.get('fullNameR')?.value,
      Direccion: this.myForm.get('direccionR')?.value,
      Email: this.myForm.get('emailR')?.value,  
      Password: this.myForm.get('passwordR')?.value 
    };  
    const result = await this.authService.register(registerData);
    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken

      if (this.remember) {
        localStorage.setItem('token', this.jwt);
      } else {
        sessionStorage.setItem('token', this.jwt);
      }

    } else {
        console.error('El usuario ya existe');
    }
  }
}


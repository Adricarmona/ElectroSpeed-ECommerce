import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthRequest } from '../../models/auth-request';
import { AuthService } from '../../service/auth.service';
import { timeInterval } from 'rxjs';
import { CarritoService } from '../../service/carrito.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  myForm: FormGroup;
  constructor(private authService: AuthService, public fb: FormBuilder, private carrito: CarritoService) {
    this.myForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit() {}

  jwt: string = '';
  passwordConfirmation: boolean = true;

  remember: boolean = false;

  async submit() {  
    const authData: AuthRequest = { 
      email: this.myForm.get('email')?.value, 
      password: this.myForm.get('password')?.value 
    };
    const result = await this.authService.login(authData); // Llama al método login

    if (result) { // Verificamos que result no sea nulo
      this.jwt = result.accessToken; // Asignamos el accessToken
      if (this.remember) {
        localStorage.setItem('token', this.jwt);
      } else {
        sessionStorage.setItem('token', this.jwt);
      }

      await this.carrito.pasarCarritoLocalABBDD()

      this.volverInicio()

    } else {
        console.error('Error en la autenticación');
    }
  }

  volverInicio(){
    window.location.href = '#'
  }
}

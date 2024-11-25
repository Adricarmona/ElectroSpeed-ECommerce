import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthRequest } from '../../models/auth-request';
import { AuthService } from '../../service/auth.service';
import { timeInterval } from 'rxjs';
import { CarritoService } from '../../service/carrito.service';
import { RedirectionService } from '../../service/redirection.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  readonly PARAM_KEY: string = 'redirectTo';
  private redirectTo: string = null;

  myForm: FormGroup;
  constructor(

    private authService: AuthService, 
    public fb: FormBuilder, 
    private carrito: CarritoService,  
    private service: RedirectionService,
    private activatedRoute: ActivatedRoute,  
    private router: Router

  ) {

    this.myForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });

  }
  
  ngOnInit(): void {
    const queryParams = this.activatedRoute.snapshot.queryParamMap;

    if (queryParams.has(this.PARAM_KEY)) {
      this.redirectTo = queryParams.get(this.PARAM_KEY);
    }
  }

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

      this.login()

    } else {
        console.error('Error en la autenticación');
    }
  }

  login() {
    // Iniciamos sesión

    console.log("esoty en login")
    this.service.login();

    // Si tenemos que redirigir al usuario, lo hacemos
    if (this.redirectTo != null) {
      this.router.navigateByUrl(this.redirectTo);
    }
  }

  volverInicio(){
    window.location.href = '#'
  }
}

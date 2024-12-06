import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthRequest } from '../../models/auth-request';
import { AuthService } from '../../service/auth.service';
import { RedirectionService } from '../../service/redirection.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../service/api-service';
import { CarritoService } from '../../service/carrito.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { NavbarService } from '../../service/navbar.service';

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
  protected errorAlIniciar: Boolean = false;

  myForm: FormGroup;
  constructor(
    private authService: AuthService, 
    public fb: FormBuilder,   
    private service: RedirectionService,
    private activatedRoute: ActivatedRoute,  
    private router: Router,
    private carrito: CarritoService,
    private navbarService: NavbarService
  ) {
    navbarService.cambiarCss(0)

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
  remember: boolean = false;

  async submit() {  
    const authData: AuthRequest = { 
      email: this.myForm.get('email')?.value, 
      password: this.myForm.get('password')?.value,
      remember: this.remember
    };
    const error = await this.authService.login(authData); // Llama al método login
    if (error == null) {
      this.errorAlIniciar = true
    } else {
      this.errorAlIniciar = false
    }

    await this.carrito.pasarCarritoLocalABBDD()
    
    
    // sin esto "await" el location.reload() se recarga antes que el 
    // router cambia de pagina y no funciona 
    await this.router.navigateByUrl(this.redirectTo)

    location.reload()
  }

  volverInicio(){
    window.location.href = '#'
  }
}

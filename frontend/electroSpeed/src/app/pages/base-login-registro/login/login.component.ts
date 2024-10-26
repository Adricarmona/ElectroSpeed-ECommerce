import { Component } from '@angular/core';
import { BaseLoginRegistroComponent } from '../base-login-registro.component';
import { NavbarComponent } from '../../navbar/navbar.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [BaseLoginRegistroComponent, NavbarComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

}

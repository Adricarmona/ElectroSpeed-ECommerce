import { Component } from '@angular/core';
import { BaseLoginRegistroComponent } from '../base-login-registro.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [BaseLoginRegistroComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

}

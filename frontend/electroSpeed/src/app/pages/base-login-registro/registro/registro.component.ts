import { Component } from '@angular/core';
import { BaseLoginRegistroComponent } from '../base-login-registro.component';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [BaseLoginRegistroComponent],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css'
})
export class RegistroComponent {

}

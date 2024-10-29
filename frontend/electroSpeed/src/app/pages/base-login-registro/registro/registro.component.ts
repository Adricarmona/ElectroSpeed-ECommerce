import { Component } from '@angular/core';
import { BaseLoginRegistroComponent } from '../base-login-registro.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../service/auth.service';
import { AuthSend } from '../../../models/auth-send';

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
    console.log(registerData)
    this.authService.register(registerData);
  }

}

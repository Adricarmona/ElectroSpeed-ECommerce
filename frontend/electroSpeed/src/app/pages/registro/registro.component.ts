import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { AuthSend } from '../../models/auth-send';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule ],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css'
})
export class RegistroComponent {

  myForm: FormGroup;
  constructor(private authService: AuthService, public fb: FormBuilder) {
    this.myForm = this.fb.group({
      email: ['', [Validators.email, Validators.required]],
      direccion: ['', [Validators.required]],
      fullName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      passwordRep: ['', [Validators.required]]
    });
  }

  jwt: string = '';
  passwordConfirmation: boolean = true;

  remember: boolean = false;

  async submit(){

    if(this.myForm.get('password')?.value != this.myForm.get('passwordRep')?.value){
      this.passwordConfirmation = false;
      return;
    }

    const registerData: AuthSend = 
    {  
      Name : this.myForm.get('fullName')?.value,
      Direccion: this.myForm.get('direccion')?.value,
      Email: this.myForm.get('email')?.value,  
      Password: this.myForm.get('password')?.value, 
      Remember: this.remember
    };  
    await this.authService.register(registerData);
  }

  volverInicio(){
    window.location.href = '#'
  }
}

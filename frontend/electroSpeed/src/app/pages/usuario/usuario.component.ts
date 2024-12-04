import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../service/api-service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {
  usuario: any;
  form: { name: string, email: string, direccion: string, password: string } = {
    name: '',
    email: '',
    direccion: '',
    password: ''
  };

  constructor(private apiService: ApiService, private fb: FormBuilder) {}

  ngOnInit(): void {
    // Cargar los detalles del usuario aquí
    this.apiService.getUsuarioDetalle().then(result => {
      if (result.success) {
        this.usuario = result.data;
        this.form.name = this.usuario.name;
        this.form.email = this.usuario.email;
        this.form.direccion = this.usuario.direccion;
      }
    });
  }

  actualizarUsuario() {
    // Lógica para actualizar el usuario
    this.apiService.actualizarUsuario(this.form).then(response => {
      alert('Información actualizada correctamente');
    });
  }
}

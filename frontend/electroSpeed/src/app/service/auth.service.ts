import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthRequest } from '../models/auth-request'; 
import { AuthResponse } from '../models/auth-response'; 
import { jwtDecode } from 'jwt-decode';
import { AuthSend } from '../models/auth-send';
import { Usuarios } from '../models/usuarios';
import { ApiService } from './api-service';
import { RedirectionService } from './redirection.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CarritoService } from './carrito.service';
import { BicisCantidad } from '../models/bicis-cantidad';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(
    private http: HttpClient,
    private service: RedirectionService,  
    private apiService: ApiService
  ) { }

  Usuarios: Usuarios = {
    id: 0,
    name: '',
    direccion: '',
    email: '',
    admin: false
  }

  jwt: string = '';

  async login(authData: AuthRequest): Promise<AuthResponse | null> {
    try {
      const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}login`, authData);
      const result: AuthResponse = await lastValueFrom(request);

      this.apiService.token = result.accessToken;

        this.jwt  = result.accessToken; // Asignamos el accessToken
        if (authData.remember) { //Guardamos el token en local o session en funcion si le ha dado a que le recuerde
          localStorage.setItem('token', this.jwt);
        } else {
          sessionStorage.setItem('token', this.jwt);
        }

      return result;
    } catch (error) {

      console.error('Error during login:', error);
      return null;
    }
  }

  async register(registerData: AuthSend): Promise<AuthResponse | null> {
    try {
        const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}register`, registerData);
        const result: AuthResponse = await lastValueFrom(request);

          this.jwt = result.accessToken; // Asignamos el accessToken

          if (registerData.Remember) {
            localStorage.setItem('token', this.jwt);
          } else {
            sessionStorage.setItem('token', this.jwt);
          }

        return result;
    } catch (error: any) {
        // Si el mensaje es una cadena, validamos directamente su contenido
        if (error.status === 400 && error.error === "El nombre de usuario ya está en uso") {
            console.log("El usuario ya existe. Por favor, elige otro nombre de usuario.");
        } else {
            console.log("Falla general en el registro");
        }
        return null;
    }
}

  loged(){
    if (this.getToken()) {
      return true
    }
    return false
  }

  // Método para recuperar el token
  getToken(): string | null {
    if (localStorage.getItem('token') != "" && localStorage.getItem('token') != null) {
      return localStorage.getItem('token')
    } else if (sessionStorage.getItem('token') != "" && sessionStorage.getItem('token') != null) {
      return sessionStorage.getItem('token')
    }
    return null;
  }

  setTokenLocal(token: string) {
    if (token != "") {
      localStorage.setItem('token',token)
    } else {
      localStorage.setItem('token', "")
    }
  }

  setTokenSesion(token: string) {
    if (token != "") {
      sessionStorage.setItem('token',token)
    } else {
      sessionStorage.setItem('token', "")
    }
  }

  getEmailUserToken() {
    const token = this.getToken()
    if (token != null) {
      const tokenDecodificado: any = jwtDecode(token)
      const email = tokenDecodificado.email
      return email
    }
    return null
  }

  getNameUserToken() {
    const token = this.getToken()
    if (token != null) {
      const tokenDecodificado: any = jwtDecode(token)
      const name = tokenDecodificado.unique_name
      return name
    }
    return "error"
  }

  getAdminUserToken() {
    const token = this.getToken()
    if (token != null) {
      const tokenDecodificado: any = jwtDecode(token)
      const role = tokenDecodificado.role
      return role
    }
    return null
  }

  async getIdUserEmail(correo :string) {
    const resultado: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
    const request: Usuarios = await lastValueFrom(resultado)
    if (request) {
      this.Usuarios = request
      return request
    }
    return null
  }

  async getIdUser() {
    const correo = this.getEmailUserToken()
    if (correo) {
      const resultado: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
      const request: Usuarios = await lastValueFrom(resultado)   
      this.Usuarios = request
      return request.id
    }
    return null
  }

  async getNameUser() {
    const correo = this.getEmailUserToken()
    if (correo) {
      const resultado: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
      if (resultado) {
        const request: Usuarios = await lastValueFrom(resultado)
        this.Usuarios = request
        return request.name
      }
    }
    return null
  }


  async getUsersDto() {
    try {
      const request: Observable<Usuarios[]> = this.http.get<Usuarios[]>(`${this.BASE_URL}AdminView`);
      const result: Usuarios[] = await lastValueFrom(request);

      return result
    } catch (error) {
      console.error("Error al buscar el usuario: ", error)
      return null
    }
  }

  async deleteUser(id: number) {
    try {
      await this.apiService.delete("deleteUsuarioId?id="+id)
    } catch (error) {
      console.log(error)
    }
  }

  async updateUser(usuario: Usuarios) {
    try {
      await this.apiService.post("updateUsuario",usuario)
    } catch (error) {
      console.log(error)
    }
  }

  async updatePasword(id: number, password: string) {
    try {
      await this.apiService.post("updateUsuarioPasword?id="+id+"&password="+password)
    } catch (error) {
      console.log(error)
    }
  }

  async getPedidos(){
    return this.apiService.get<BicisCantidad[]>(`listapedidos`)
  }

}




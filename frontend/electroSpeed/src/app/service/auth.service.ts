import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { environment } from '../environments/enviroments.developments';
import { AuthRequest } from '../models/auth-request'; 
import { AuthResponse } from '../models/auth-response'; 
import { jwtDecode } from 'jwt-decode';
import { AuthSend } from '../models/auth-send';
import { Usuarios } from '../models/usuarios';
import { RedirectionService } from './redirection.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CarritoService } from './carrito.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(
    private http: HttpClient,
    private service: RedirectionService,
    private activatedRoute: ActivatedRoute,  
    private router: Router,
    private carrito: CarritoService
  ) { }

  jwt: string = '';

  async login(authData: AuthRequest): Promise<AuthResponse | null> {
    try {

      const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}login`, authData);
      const result: AuthResponse = await lastValueFrom(request);
      console.log("Token recibido: " + result.accessToken);//escribimos el token por consola

      const decodedToken = jwtDecode(result.accessToken);//decodificamos el token usando la biblioteca jwtDecode
      console.log("Decoded Token:", decodedToken);//escribimos por consola el token decodificado

        this.jwt  = result.accessToken; // Asignamos el accessToken

        if (authData.remember) { //Guardamos el token en local o session en funcion si le ha dado a que le recuerde
          localStorage.setItem('token', this.jwt);
        } else {
          sessionStorage.setItem('token', this.jwt);
        }
  
        await this.carrito.pasarCarritoLocalABBDD()
        
  
      this.service.login()

      return result;
    } catch (error) {

      console.error('Error during login:', error);
      return null;
    }
  }

  async register(registerData: AuthSend): Promise<AuthResponse | null> {
    try {
        console.log('Datos a enviar:', registerData);
        const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}register`, registerData);
        const result: AuthResponse = await lastValueFrom(request);
        console.log("Token recibido: " + result.accessToken);

        const decodedToken = jwtDecode(result.accessToken);
        console.log("Decoded Token:", decodedToken);

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


  // Método para recuperar el token
  getToken(): string | null {

    if (localStorage.getItem('token') != "") {
      return localStorage.getItem('token')
    } else if (sessionStorage.getItem('token') != "") {
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
    return "error"
  }

  async getIdUserEmail(correo :string) {
    const resultado: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
    const request: Usuarios = await lastValueFrom(resultado)
    return request
  }

  async getIdUser() {
    const correo = this.getEmailUserToken()
    const resultado: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
    const request: Usuarios = await lastValueFrom(resultado)
    return request.id
  }

}




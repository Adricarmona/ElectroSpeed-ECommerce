import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { environment } from '../environments/enviroments.developments';
import { AuthRequest } from '../models/auth-request'; 
import { AuthResponse } from '../models/auth-response'; 
import { jwtDecode } from 'jwt-decode';
import { AuthSend } from '../models/auth-send';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async login(authData: AuthRequest): Promise<AuthResponse | null> {
    try {

      const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}login`, authData);
      const result: AuthResponse = await lastValueFrom(request);
      console.log("Token recibido: " + result.accessToken);//escribimos el token por consola

      localStorage.setItem('token', result.accessToken);//guardamos el token en el local storage

      const decodedToken = jwtDecode(result.accessToken);//decodificamos el token usando la biblioteca jwtDecode
      console.log("Decoded Token:", decodedToken);//escribimos por consola el token decodificado

      return result;
    } catch (error) {

      console.log("falla")
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

        localStorage.setItem('token', result.accessToken);

        const decodedToken = jwtDecode(result.accessToken);
        console.log("Decoded Token:", decodedToken);

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


/* 
   async getUser(username: string): Promise<Usuarios | null> {
    try {
      const request: Observable<Object> = this.http.get(`${this.BASE_URL}User`);
      const dataraw: any = await lastValueFrom(request);

      const user: Usuarios = {
        id: dataraw.id,
        name: dataraw.name,
        username: dataraw.username,
        email: dataraw.email,
        picture: dataraw.picture
      };

      return user;
    } catch (error) {
      console.error('Error fetching user:', error);
      return null;
    }
  }  */

  // Método para recuperar el token
  getToken(): string | null {
    return localStorage.getItem('token');
  }
}




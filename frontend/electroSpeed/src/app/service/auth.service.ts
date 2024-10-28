import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { environment } from '../environments/enviroments.developments';
import { AuthRequest } from '../models/auth-request'; 
import { AuthResponse } from '../models/auth-response'; 
import { Usuarios } from '../models/usuarios';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async login(authData: AuthRequest): Promise<AuthResponse | null> {
    try {
      console.log("1")
      const request: Observable<AuthResponse> = this.http.post<AuthResponse>(`${this.BASE_URL}login`, authData);
      console.log("2")
      const result: AuthResponse = await lastValueFrom(request);
      console.log("3")

      return result;
    } catch (error) {

      console.log("falla")
      console.error('Error during login:', error);
      return null;
    }
  }

  async getUser(username: string): Promise<Usuarios | null> {
    try {
      const request: Observable<Object> = this.http.get(`${this.BASE_URL}/User`);
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
  }
}


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { environment } from '../environments/enviroments.developments';
import { Usuarios } from '../models/usuarios';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private BASE_URL = environment.apiUrl;

  constructor(private http: HttpClient) { }

  async getUser(username: String): Promise<Usuarios> {
    const request: Observable<Object> = this.http.get(`${this.BASE_URL}${username}`)
    const dataraw: any = await lastValueFrom(request)

    const user: Usuarios = {
      id: dataraw.id,
      name: dataraw.name,
      username: dataraw.username,
      email: dataraw.email,
      picture: dataraw.picture
    }

    return user
  }

}

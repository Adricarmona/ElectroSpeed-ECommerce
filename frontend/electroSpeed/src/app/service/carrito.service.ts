import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { lastValueFrom, Observable } from 'rxjs';
import { Carrito } from '../models/carrito';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async getIdCarrito(idUsuario: number){
    try{
      const request: Observable<Carrito> = this.http.get<Carrito>(`${this.BASE_URL}carrito/${idUsuario}`);
      const result: Carrito = await lastValueFrom(request);
      return result
    }catch(error){
      console.error("Error por bobo: ",error)
      return null
    }


  }
}

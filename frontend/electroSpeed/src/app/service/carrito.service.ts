import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { lastValueFrom, Observable } from 'rxjs';
import { Carrito } from '../models/carrito';
import { Bicicletas } from '../models/catalogo';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async getIdCarrito(idUsuario: number) {
    try {
      const request: Observable<Carrito> = this.http.post<Carrito>(`${this.BASE_URL}ShoppingCart/idDelUsuario?idUsuario=${idUsuario}`, idUsuario);
      const result: Carrito = await lastValueFrom(request);
      return result
    } catch (error) {
      console.error("Error por bobo: ", error)
      return null
    }
  }

  async bicisAlCarrito(Bici: Bicicletas){

  }

  async eliminarBiciCarrito(idBici: number, idCarrito: number){
    try {
      const request: Observable<Carrito> = this.http.post<Carrito>(`${this.BASE_URL}ShoppingCart/${idCarrito}?bicicletaId${idBici}`, idBici);
      const result: Carrito = await lastValueFrom(request);
      return result
    } catch (error) {
      console.error("Error por bobo2: ", error)
      return null
    }
  }
}

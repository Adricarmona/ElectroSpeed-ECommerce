import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { lastValueFrom, Observable } from 'rxjs';
import { Carrito } from '../models/carrito';
import { CarritoEntero } from '../models/carrito-entero';

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

  async enviarCarrito(idBici: number, idCarrito: number) {
    try {
      this.http.put(`${this.BASE_URL}ShoppingCart/addProduct?carritoId=${idCarrito}&idBicicleta=${idBici}`,{});
    } catch (error) {
      console.log("error al enviar al carrito")
    }

  }

  async devolverCarritoPorUsuario(id: number): Promise<CarritoEntero> {
    try {
      const resultado: Observable<CarritoEntero> = this.http.get<CarritoEntero>(`${this.BASE_URL}ShoppingCart?idusuario=${id}`);
      const request: CarritoEntero = await lastValueFrom(resultado)
      return request
    } catch {
      console.log("error")
      return null
    }
  }
}

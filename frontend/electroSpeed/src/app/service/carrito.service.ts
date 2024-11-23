import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { lastValueFrom, Observable } from 'rxjs';
import { Carrito } from '../models/carrito';
import { CarritoEntero } from '../models/carrito-entero';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient,
    private auth: AuthService
  ) { }

  async getIdCarrito(idUsuario: number){
    try{
      const request: Observable<CarritoEntero> = this.http.get<CarritoEntero>(`${this.BASE_URL}ShoppingCart?idusuario=${idUsuario}`);
      const result: CarritoEntero = await lastValueFrom(request);
      return result
    } catch (error) {
      console.error("Error por bobo: ", error)
      return null
    }
  }

  async enviarCarrito(idBici: number, idCarrito: number) {
    try {
      this.http.put(
        `${this.BASE_URL}ShoppingCart/addProduct?carritoId=${idCarrito}&idBicicleta=${idBici}`, 
        {}, 
        { responseType: 'text' }
      ).subscribe({
        error: err => console.error("Error al agregar al carrito:", err)
      });
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

  async borrarBiciCarrito(idCarrito: number, idBici: number){
    this.http.delete(
      `${this.BASE_URL}ShoppingCart/${idCarrito}?bicicletaId=${idBici}`
      , { responseType: 'text' }
    )
    .subscribe({
      next: (response) => console.log('za borrao:', response),
      error: (error) => console.error('no za borrao:', error),
    })
  }

  async pasarCarritoLocalABBDD(){
    const idBicisLocal = localStorage.getItem('idbici')
    const ids = idBicisLocal ? idBicisLocal.split(',').map((id) => id.trim()) : [];

    const carrito: CarritoEntero = await this.devolverCarritoPorUsuario((await this.auth.getIdUserEmail(this.auth.getEmailUserToken())).id)

    ids.forEach(idBicis => {
      this.enviarCarrito(parseInt(idBicis),carrito.id)
    });
    
    localStorage.removeItem('idbici')
  }
}

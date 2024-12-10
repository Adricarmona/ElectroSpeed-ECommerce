import { Injectable } from '@angular/core';
import { Result } from '../models/result';
import { CheckoutSessionStatus } from '../models/checkout-session-status';
import { ApiService } from './api-service';
import { CheckoutSession } from '../models/checkout.session';
import { Product } from '../models/product';
import { Bicicletas } from '../models/catalogo';
import { CarritoEntero } from '../models/carrito-entero';
import { OrdenTemporal } from '../models/orden-temporal';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private api: ApiService) { }

  getEmbededCheckout(): Promise<Result<CheckoutSession>> {
    return this.api.get<CheckoutSession>('api/checkout/embedded');
  }

  getStatus(sessionId: string): Promise<Result<CheckoutSessionStatus>> {
    return this.api.get<CheckoutSessionStatus>(`api/Checkout/status/${sessionId}`);
  }

  async postOrdenTemporalLocal(carrito : string){
    var result = await this.api.post<number>(`api/checkout/OrdenTemporalLocal/${carrito}`)
    return result;
  }
  async postOrdenTemporalCarrito(id : number){
    var result = await this.api.post<number>(`api/checkout/OrdenTemporalCarrito/${id}`)
    return result;
  }

  async cambiarIdUser(reserva: number){
     await this.api.post<number>(`api/checkout/OrdenTAñadirUsuario/${reserva}`)
  }

   eliminarOrden(){
    console.log('4. Finalmente esto (cuando el componente sea destruido)');
     this.api.delete<number>(`api/checkout/EliminarOrdenTemporal`)
 }

  postPedido(){

  }
}

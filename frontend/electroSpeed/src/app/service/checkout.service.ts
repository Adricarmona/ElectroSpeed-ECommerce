import { Injectable } from '@angular/core';
import { Result } from '../models/result';
import { CheckoutSessionStatus } from '../models/checkout-session-status';
import { ApiService } from './api-service';
import { CheckoutSession } from '../models/checkout.session';
import { Product } from '../models/product';
import { Bicicletas } from '../models/catalogo';
import { CarritoEntero } from '../models/carrito-entero';
import { OrdenTemporal } from '../models/orden-temporal';
import { TemporalOrder } from '../models/temporalorder';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private api: ApiService) { }

  getEmbededCheckout(res: string): Promise<Result<CheckoutSession>> {
    return this.api.get<CheckoutSession>(`api/checkout/embedded/${res}`);
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

   eliminarOrden(res: string){
     this.api.delete<number>(`api/checkout/EliminarOrdenTemporal/${res}`)
 }

  async postPedido(res: string){
    await this.api.post<number>(`api/checkout/guardarcomprar/${res}`)
  }
  
  async restaurarStock(res: string){
    await this.api.post<string>(`api/checkout/RestaurarStock/${res}`)
  }

  async elimiarCarrito(res: string){
    await this.api.post<string>(`api/checkout/eliminarDelCarrito/${res}`)
  }

  async DevolverOrden(res: string){
    return await this.api.post<TemporalOrder>(`api/checkout/DevolverOrden/${res}`)
  }
}

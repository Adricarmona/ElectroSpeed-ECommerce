import { Injectable } from '@angular/core';
import { Result } from '../models/result';
import { CheckoutSessionStatus } from '../models/checkout-session-status';
import { ApiService } from './api-service';
import { CheckoutSession } from '../models/checkout.session';
import { Product } from '../models/product';
import { Bicicletas } from '../models/catalogo';
import { CarritoEntero } from '../models/carrito-entero';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private api: ApiService) { }

  getAllProducts(): Promise<Result<CarritoEntero>> {
    return this.api.get<CarritoEntero>('ShoppingCart/idDelUsuario?idUsuario=1');
  }

  getEmbededCheckout(idUsuario: number): Promise<Result<CheckoutSession>> {
    return this.api.get<CheckoutSession>('embedded?idUsuario='+idUsuario);
  }

  getStatus(sessionId: string): Promise<Result<CheckoutSessionStatus>> {
    return this.api.get<CheckoutSessionStatus>(`checkout/status/${sessionId}`);
  }

  postPedido(){

  }
}

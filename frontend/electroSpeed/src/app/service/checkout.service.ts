import { Injectable } from '@angular/core';
import { Result } from '../models/result';
import { CheckoutSessionStatus } from '../models/checkout-session-status';
import { Product } from '../models/Product';
import { CheckoutSession } from '../models/checkout.session';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/enviroments.developments';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private BASE_URL = `${environment.apiUrl}`
  constructor(private http: HttpClient) { }

  async getAllProducts(authData: AuthRequest): Promise<AuthResponse | null> {
    return this.http.post<>(`${this.BASE_URL}login`, );
  }

  getEmbededCheckout(): Promise<Result<CheckoutSession>> {
    return this.api.get<CheckoutSession>('checkout/embedded');
  }

  getStatus(sessionId: string): Promise<Result<CheckoutSessionStatus>> {
    return this.api.get<CheckoutSessionStatus>(`checkout/status/${sessionId}`);
  }
}

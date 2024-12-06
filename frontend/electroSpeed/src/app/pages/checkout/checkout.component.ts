import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { StripeService } from 'ngx-stripe';
import { AuthService } from '../../service/auth.service';
import { CheckoutService } from '../../service/checkout.service';
import { Subscription } from 'rxjs';
import { StripeComponent } from '../stripe/stripe.component';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [StripeComponent],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  routeQueryMap$: Subscription;
  metodoPago: string = '';
  queryMap: ParamMap;

  constructor(
    private auth: AuthService,
    private service: CheckoutService, 
    private route: ActivatedRoute, 
    private router: Router,
    private stripe: StripeService) {}

  ngOnInit() {
    this.routeQueryMap$ = this.route.queryParamMap.subscribe(queryMap => this.init(queryMap));
  }

  async init(queryMap: ParamMap) {
    this.metodoPago = queryMap.get('metodo_pago');
    console.log(this.metodoPago)
/*     if (this.sessionId) {
      const request = await this.service.getStatus(this.sessionId);
      if (request.success) {
        console.log(request.data);
      }
    } else {
      const request = await this.service.getAllProducts();
      console.log(request);

      if (request.success) {
        // Accede directamente a `data` porque no es un arreglo
        this.product = request.data;
      }
    } */
  }
}

import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StripeService } from 'ngx-stripe';
import { AuthService } from '../../service/auth.service';
import { CheckoutService } from '../../service/checkout.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  routeQueryMap$: Subscription;
  metodoPago: string = '';

  constructor(
    private auth: AuthService,
    private service: CheckoutService, 
    private route: ActivatedRoute, 
    private router: Router,
    private stripe: StripeService) {}

  ngOnInit() {

  }

}

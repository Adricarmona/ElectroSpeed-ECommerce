import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { StripeEmbeddedCheckout, StripeEmbeddedCheckoutOptions } from '@stripe/stripe-js';
import { StripeService } from 'ngx-stripe';
import { CheckoutService } from '../../service/checkout.service';
import { Product } from '../../models/product';
import { BiciPagina } from '../../models/bici-pagina';
import { Bicicletas } from '../../models/catalogo';
import { CarritoEntero } from '../../models/carrito-entero';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-stripe',
  standalone: true,
  imports: [],
  templateUrl: './stripe.component.html',
  styleUrl: './stripe.component.css'
})
export class StripeComponent implements OnInit, OnDestroy {

  @ViewChild('checkoutDialog')
  checkoutDialogRef: ElementRef<HTMLDialogElement>;

  product: CarritoEntero = null;
  sessionId: string = '';
  routeQueryMap$: Subscription;
  stripeEmbedCheckout: StripeEmbeddedCheckout;

  constructor(
    private auth: AuthService,
    private service: CheckoutService, 
    private route: ActivatedRoute, 
    private router: Router,
    private stripe: StripeService) {}

   ngOnInit() {
    // El evento ngOnInit solo se llama una vez en toda la vida del componente.
    // Por tanto, para poder captar los cambios en la url nos suscribimos al queryParamMap del route.
    // Cada vez que se cambie la url se llamará al método onInit
    this.routeQueryMap$ = this.route.queryParamMap.subscribe(queryMap => this.init(queryMap));
    this.embeddedCheckout()
  }

  ngOnDestroy(): void {
    // Cuando este componente se destruye hay que cancelar la suscripción.
    // Si no se cancela se seguirá llamando aunque el usuario no esté ya en esta página
    this.routeQueryMap$.unsubscribe();
  }

  async init(queryMap: ParamMap) {
    this.sessionId = queryMap.get('session_id');

    if (this.sessionId) {
      const request = await this.service.getStatus(this.sessionId);

      if (request.success) {
        console.log(request.data);
      }
    } else {
      const request = await this.service.getAllProducts();

      if (request.success) {
        // Accede directamente a `data` porque no es un arreglo
        this.product = request.data;
      
        // Si quieres trabajar con `bicisCantidad` específicamente
        const bicisCantidad = request.data.bicisCantidad;
      }
    }
  }

  async embeddedCheckout() {
    const idUsuario = await this.auth.getIdUser()

    const request = await this.service.getEmbededCheckout(idUsuario);

    if (request.success) {
      const options: StripeEmbeddedCheckoutOptions = {
        clientSecret: request.data.clientSecret
      };

      this.stripe.initEmbeddedCheckout(options)
        .subscribe((checkout) => {
          this.stripeEmbedCheckout = checkout;
          checkout.mount('#checkout');
          this.checkoutDialogRef.nativeElement.showModal();
        });
      }
  }

  reload() {
    this.router.navigate(['checkout']);
  }

  cancelCheckoutDialog() {
    this.stripeEmbedCheckout.destroy();
    this.checkoutDialogRef.nativeElement.close();
  }
}

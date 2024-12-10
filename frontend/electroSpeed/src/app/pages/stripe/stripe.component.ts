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
import { NavbarService } from '../../service/navbar.service';
import { CarritoComponent } from '../carrito/carrito.component';

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
  reservaId: string = '';
  res: string;
  routeQueryMap$: Subscription;
  stripeEmbedCheckout: StripeEmbeddedCheckout;
  private intervalId: any;

  constructor(
    private auth: AuthService,
    private service: CheckoutService, 
    private route: ActivatedRoute, 
    private router: Router,
    private stripe: StripeService,
    private navbarService: NavbarService) {
      navbarService.cambiarCss(0)
    }

   ngOnInit() {
    // El evento ngOnInit solo se llama una vez en toda la vida del componente.
    // Por tanto, para poder captar los cambios en la url nos suscribimos al queryParamMap del route.
    // Cada vez que se cambie la url se llamará al método onInit
    this.intervalId = setInterval(() => {
    this.routeQueryMap$ = this.route.queryParamMap.subscribe(queryMap => this.init());
  }, 10000);
    this.res = this.route.snapshot.queryParamMap.get('reserva_id');
    this.embeddedCheckout()
  }

  ngOnDestroy(): void {
    // Cuando este componente se destruye hay que cancelar la suscripción.
    // Si no se cancela se seguirá llamando aunque el usuario no esté ya en esta página
    this.routeQueryMap$.unsubscribe();
  }

  async init() {
    if (this.reservaId) {
      const request = await this.service.getStatus(this.reservaId);
      console.log(this.reservaId)
      if (request.success) {
        console.log(request.data);
      }else {
        console.log("request null");
      }
    }
  }

  async embeddedCheckout() {

    const request = await this.service.getEmbededCheckout(this.res);
    if (request.success) {
      console.log(request.data.sesionid)
      this.reservaId = request.data.sesionid;
      const options: StripeEmbeddedCheckoutOptions = {
        clientSecret: request.data.clientSecret,
        
        onComplete: () => this.irConfirmacion()
      };

      this.stripe.initEmbeddedCheckout(options)
        .subscribe((checkout) => {
          this.stripeEmbedCheckout = checkout;
          checkout.mount('#checkout');
          this.checkoutDialogRef.nativeElement.showModal();
        });
      }


  }

  irConfirmacion(){
    this.router.navigateByUrl("confirmacion")
  }

}

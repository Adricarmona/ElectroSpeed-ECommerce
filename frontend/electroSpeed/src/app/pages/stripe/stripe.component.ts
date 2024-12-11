import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {
  StripeEmbeddedCheckout,
  StripeEmbeddedCheckoutOptions,
} from '@stripe/stripe-js';
import { StripeService } from 'ngx-stripe';
import CheckoutService from '../../service/checkout.service';
import { Product } from '../../models/product';
import { BiciPagina } from '../../models/bici-pagina';
import { Bicicletas } from '../../models/catalogo';
import { CarritoEntero } from '../../models/carrito-entero';
import { AuthService } from '../../service/auth.service';
import { NavbarService } from '../../service/navbar.service';
import { CarritoComponent } from '../carrito/carrito.component';
import { BlockchainService } from '../../service/blockchain.service';
import { HttpClient } from '@angular/common/http';
import { CatalogoService } from '../../service/catalogo.service';

@Component({
  selector: 'app-stripe',
  standalone: true,
  imports: [],
  templateUrl: './stripe.component.html',
  styleUrl: './stripe.component.css',
})
export class StripeComponent implements OnInit, OnDestroy {
  @ViewChild('checkoutDialog')
  checkoutDialogRef: ElementRef<HTMLDialogElement>;

  product: CarritoEntero = null;
  reservaId: string = '';
  res: string;
  routeQueryMap$: Subscription;
  stripeEmbedCheckout: StripeEmbeddedCheckout;
  bicis: Bicicletas[] = []
  private intervalId: any;

  constructor(
    private auth: AuthService,
    private service: CheckoutService,
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private stripe: StripeService,
    private navbarService: NavbarService,
    private blockchainservice: BlockchainService,
    private catalogoService: CatalogoService
  ) {
    navbarService.cambiarCss(0);
  }

  ngOnInit() {
    // El evento ngOnInit solo se llama una vez en toda la vida del componente.
    // Por tanto, para poder captar los cambios en la url nos suscribimos al queryParamMap del route.
    // Cada vez que se cambie la url se llamará al método onInit
    this.routeQueryMap$ = this.route.queryParamMap.subscribe((queryMap) =>
      this.init()
    );
    this.res = this.route.snapshot.queryParamMap.get('reserva_id');
    this.embeddedCheckout();
  }

  ngOnDestroy(): void {
    // Cuando este componente se destruye hay que cancelar la suscripción.
    // Si no se cancela se seguirá llamando aunque el usuario no esté ya en esta página
    this.init();
    this.routeQueryMap$.unsubscribe();
  }

  restaurarStock() {
    this.service.restaurarStock(this.res);
  }

  async init() {
    if (this.reservaId) {
      const request = await this.service.getStatus(this.reservaId);
      console.log(this.reservaId);
      if (request.success) {
        console.log(request.data.status);
        if (request.data.status != 'complete') {
          this.restaurarStock();
        }
      } else {
        console.log('request null');
      }
    }
  }

  async embeddedCheckout() {
    const request = await this.service.getEmbededCheckout(this.res);
    if (request.success) {
      this.reservaId = request.data.sesionid;
      const options: StripeEmbeddedCheckoutOptions = {
        clientSecret: request.data.clientSecret,

        onComplete: () => this.irConfirmacion(),
      };

      this.stripe.initEmbeddedCheckout(options).subscribe((checkout) => {
        this.stripeEmbedCheckout = checkout;
        checkout.mount('#checkout');
        this.checkoutDialogRef.nativeElement.showModal();
      });
    }
  }

  irConfirmacion() {
    this.service.postPedido(this.res);
    this.service.elimiarCarrito(this.res);
    let totalGeneral = 0;

    // Generar las filas de la tabla
    const filas = this.bicis
      .map((bici) => {
        const total = bici.cantidad * bici.precio;
        totalGeneral += total; // Sumar al total general
        return `
      <tr>
        <td>${bici.marcaModelo}</td>
        <td>${bici.cantidad}</td>
        <td>€${bici.precio.toFixed(2)}</td>
        <td>€${total.toFixed(2)}</td>
      </tr>
    `;
      })
      .join('');

    // Plantilla completa del correo con las filas generadas dinámicamente
    const correoBody = `
  <!DOCTYPE html>
  <html lang="es">
  <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Factura Adjunta</title>
  </head>
  <body style="margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4;">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #f4f4f4; padding: 20px;">
      <tr>
        <td align="center">
          <table width="600px" cellpadding="0" cellspacing="0" border="0" style="background: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);">
            <!-- Encabezado -->
            <tr>
              <td style="background-color: #007bff; color: #ffffff; text-align: center; padding: 20px;">
                <h1 style="margin: 0; font-size: 24px;">Factura Adjunta</h1>
              </td>
            </tr>
            <!-- Cuerpo del correo -->
            <tr>
              <td style="padding: 20px;">
                <p style="font-size: 16px; color: #555;">Estimado/a <strong>Cliente</strong>,</p>
                <p style="font-size: 16px; color: #555;">
                  Adjuntamos a continuación el detalle de su factura. Si tiene alguna consulta, no dude en ponerse en contacto con nosotros.
                </p>
                <!-- Tabla de Factura -->
                <table width="100%" cellpadding="10" cellspacing="0" border="1" style="border-collapse: collapse; font-size: 16px; color: #555; margin-top: 20px;">
                  <tr style="background-color: #007bff; color: #ffffff;">
                    <th>Producto</th>
                    <th>Cantidad</th>
                    <th>Precio Unitario</th>
                    <th>Total</th>
                  </tr>
                  ${filas}
                  <tr>
                    <td colspan="3" style="text-align: right; font-weight: bold;">Total</td>
                    <td>€${totalGeneral.toFixed(2)}</td>
                  </tr>
                </table>
              </td>
            </tr>
            <!-- Pie de página -->
            <tr>
              <td style="background-color: #f4f4f4; color: #777; text-align: center; padding: 20px; font-size: 14px;">
                <p style="margin: 0;">Gracias por confiar en nosotros.</p>
                <p style="margin: 0;">[Nombre de la Empresa]</p>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
  </html>
`;
    
        const correofactura = {
          to: 'hectordogarcia@gmail.com',
          //to: this.otroservice.getEmailUserToken(),
          subject: 'Compra ElectroSpeed',
          body: correoBody,
          isHtml: true,
        };
        this.blockchainservice.sendEmail(correofactura);
        this.router.navigate(['/confirmacion'], { queryParams: { id: this.res } });
  }

  async DevolverOrden(){
    const request = await this.service.DevolverOrden(this.res)
    request.data.forEach(e => {
      this.datosBici(e.id)
    });
  }

  async datosBici(id: number){
    const bicicleta = await this.catalogoService.showOneBike(id.toString());
    console.log(bicicleta.cantidad)
    this.bicis.push(bicicleta)
  }
}
    

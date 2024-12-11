import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlockchainService } from '../../service/blockchain.service';
import { EthTransactionRequest } from '../../models/eth-transaction-request';
import { CarritoService } from '../../service/carrito.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { HttpClient } from '@angular/common/http';
import { CatalogoService } from '../../service/catalogo.service';
import { Bicicletas } from '../../models/catalogo';
import { BicisCantidad } from '../../models/bicis-cantidad';
import { CheckoutService } from '../../service/checkout.service';

@Component({
  selector: 'app-ethereum',
  standalone: true,
  imports: [],
  templateUrl: './ethereum.component.html',
  styleUrl: './ethereum.component.css',
})
export class EthereumComponent implements OnInit {
  constructor(
    private carritoService: CarritoService,
    private route: ActivatedRoute,
    private router: Router,
    private otroservice: AuthService,
    private http: HttpClient,
    private checkoutservice: CheckoutService,
    private service: BlockchainService,
    private catalogoService: CatalogoService
  ) {}
  reservaId: string = '';
  res: string;
  eurosToSend: number;
  addressToSend: string = '0xDBd229EBae72064CD86B213908fc4a7e0c12D65d';
  bicis: Bicicletas[] = []
  bicicantidad: BicisCantidad[] = []

  ngOnInit() {
    this.res = this.route.snapshot.queryParamMap.get('reserva_id');
  }
  async createTransaction() {
    this.eurosToSend = this.carritoService.getTotal();

    // Si no está instalado Metamask se lanza un error y se corta la ejecución
    if (!window.ethereum) {
      throw new Error('Metamask not found');
    }

    // Obtener la cuenta de metamask del usuario
    const accounts = await window.ethereum.request({
      method: 'eth_requestAccounts',
    });
    const account = accounts[0];

    // Pedimos permiso al usuario para usar su cuenta de metamask
    await window.ethereum.request({
      method: 'wallet_requestPermissions',
      params: [
        {
          eth_accounts: { account },
        },
      ],
    });

    // Obtenemos los datos que necesitamos para la transacción:
    // gas, precio del gas y el valor en Ethereum
    const transactionRequest: EthTransactionRequest = {
      euros: this.eurosToSend,
    };
    const ethereumInfoResult = await this.service.getEthereumInfo(
      transactionRequest
    );
    const ethereumInfo = ethereumInfoResult.data;
    console.log(ethereumInfo);

    // Creamos la transacción y pedimos al usuario que la firme
    const transactionHash = await window.ethereum.request({
      method: 'eth_sendTransaction',
      params: [
        {
          from: account,
          to: this.addressToSend,
          value: ethereumInfo.value,
          gas: ethereumInfo.gas,
          gasPrice: ethereumInfo.gasPrice,
        },
      ],
    });

    // Pedimos al servidor que verifique la transacción.
    // CUIDADO: si el cliente le manda todos los datos,
    // podría engañar al servidor.
    const checkTransactionRequest = {
      hash: transactionHash,
      from: account,
      to: this.addressToSend,
      value: ethereumInfo.value,
    };

    const checkTransactionResult = await this.service.checkTransaction(
      checkTransactionRequest
    );

    // Notificamos al usuario si la transacción ha sido exitosa o si ha fallado.
    if (checkTransactionResult.success && checkTransactionResult.data) {
      alert('Transacción realizada con éxito');
      this.irConfirmacion();
    } else {
      alert('Transacción fallida');
    }
  }

  async irConfirmacion() {
    await this.checkoutservice.postPedido(this.res);
    await this.checkoutservice.elimiarCarrito(this.res);
    await this.DevolverOrden()
    if (this.bicis.length === 0) {
      console.error('No se cargaron las bicicletas asociadas al pedido.');
      return;
    }

    let totalGeneral = 0;

    const filas = this.bicis
      .map((bici, index) => {
        const biciCantidad = this.bicicantidad[index]; 
        const cantidad = biciCantidad.cantidad; 
        const total = cantidad * bici.precio;
        totalGeneral += total; 
        return `
      <tr>
        <td><img src="${bici.urlImg}"></td>
        <td>${bici.marcaModelo}</td>
        <td>${cantidad}</td>
        <td>€${bici.precio}</td>
        <td>€${total}</td>
      </tr>
    `;
      })
      .join('');

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
                    <th>Foto</th>
                    <th>Producto</th>
                    <th>Cantidad</th>
                    <th>Precio Unitario</th>
                    <th>Total</th>
                  </tr>
                  ${filas}
                  <tr>
                    <td colspan="3" style="text-align: right; font-weight: bold;">Total</td>
                    <td>€${totalGeneral}</td>
                  </tr>
                </table>
              </td>
            </tr>
            <!-- Pie de página -->
            <tr>
              <td style="background-color: #f4f4f4; color: #777; text-align: center; padding: 20px; font-size: 14px;">
                <p style="margin: 0;">Gracias por confiar en nosotros.</p>
                <p style="margin: 0;">ElectroSpeed</p>
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
        await this.service.sendEmail(correofactura);
    this.router.navigate(['/confirmacion'], {
      queryParams: { reserva_id: this.res },
    });
  }

  async DevolverOrden(){
    const request = await this.checkoutservice.DevolverOrden(this.res);
    this.bicicantidad = request.data

    const promises = request.data.map((e) => this.datosBici(e.id));
    await Promise.all(promises);
  }

  async datosBici(id: number){
    const bicicleta = await this.catalogoService.showOneBike(id.toString());
    console.log(bicicleta.cantidad)
    this.bicis.push(bicicleta)
  }
}


declare global {
  interface Window {
    ethereum: any;
  }
}

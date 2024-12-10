import { Component, OnInit } from '@angular/core';
import { BlockchainService } from '../../service/blockchain.service';
import { EthTransactionRequest } from '../../models/eth-transaction-request';
import { CarritoService } from '../../service/carrito.service';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ethereum',
  standalone: true,
  imports: [],
  templateUrl: './ethereum.component.html',
  styleUrl: './ethereum.component.css'
})
export class EthereumComponent {

  constructor(
    private carritoService: CarritoService,
    private router: Router,
    private otroservice: AuthService,
    private http: HttpClient,
    private service: BlockchainService

  ) {}
  eurosToSend: number ;
  addressToSend: string = "0xDBd229EBae72064CD86B213908fc4a7e0c12D65d";
  async createTransaction() {

    this.eurosToSend = this.carritoService.getTotal()

    // Si no está instalado Metamask se lanza un error y se corta la ejecución
    if (!window.ethereum) {
      throw new Error('Metamask not found');
    }

    // Obtener la cuenta de metamask del usuario
    const accounts = await window.ethereum.request({method:'eth_requestAccounts'});
    const account = accounts[0];

    // Pedimos permiso al usuario para usar su cuenta de metamask
    await window.ethereum.request({
      method: 'wallet_requestPermissions',
      params: [{
        "eth_accounts": {account}
      }]
    });

    // Obtenemos los datos que necesitamos para la transacción: 
    // gas, precio del gas y el valor en Ethereum
    const transactionRequest: EthTransactionRequest = { 
      euros: this.eurosToSend 
    };
    const ethereumInfoResult = await this.service.getEthereumInfo(transactionRequest);
    const ethereumInfo = ethereumInfoResult.data;
    console.log(ethereumInfo)

    // Creamos la transacción y pedimos al usuario que la firme
    const transactionHash = await window.ethereum.request({
      method: 'eth_sendTransaction',
      params: [{
        from: account,
        to: this.addressToSend,
        value: ethereumInfo.value,
        gas: ethereumInfo.gas,
        gasPrice: ethereumInfo.gasPrice
      }]
    });

    // Pedimos al servidor que verifique la transacción.
    // CUIDADO: si el cliente le manda todos los datos,
    // podría engañar al servidor.
    const checkTransactionRequest = { 
      hash: transactionHash,
      from: account,
      to: this.addressToSend,
      value: ethereumInfo.value
    }
    
    const checkTransactionResult = await this.service.checkTransaction(checkTransactionRequest);

    // Notificamos al usuario si la transacción ha sido exitosa o si ha fallado.
    if (checkTransactionResult.success && checkTransactionResult.data) {
      alert('Transacción realizada con éxito');
      this.http.get('pages/correo/correo.component.html', { responseType: 'text' }).subscribe((htmlContent) => {
        const correofactura = {
          to: this.otroservice.getEmailUserToken(),
          subject: "Compra ElectroSpeed",
          body: htmlContent,
          isHtml: true 
        };
      });
      this.irConfirmacion()
      
    } else {
      alert('Transacción fallida');
    }
  }  

  irConfirmacion(){
    this.router.navigateByUrl("confirmacion")
  }
}

declare global {
  interface Window {
    ethereum: any;
  }
}  







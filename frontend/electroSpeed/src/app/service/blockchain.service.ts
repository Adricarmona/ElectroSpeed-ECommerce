import { Injectable } from '@angular/core';
import { ApiService } from './api-service';
import { EthTransactionRequest } from '../models/eth-transaction-request';
import { Result } from '../models/result';
import { Ethereuminfo } from '../models/ethereuminfo';
import { CheckEthTransactionRequest } from '../models/check-eth-transaction-request';
import { CorreoFactura } from '../models/correo-factura';

@Injectable({
  providedIn: 'root'
})
export class BlockchainService {

  constructor(private api: ApiService) { }
  getEthereumInfo(data: EthTransactionRequest): Promise<Result<Ethereuminfo>> {
    return this.api.post<Ethereuminfo>(`api/Blockchain/transaccion`, data)
  }

  checkTransaction(data: CheckEthTransactionRequest): Promise<Result<boolean>> {
    return this.api.post<boolean>(`api/Blockchain/checkeaetabaina`, data)
  }

  sendEmail(data: CorreoFactura) {
    return this.api.post(`api/Blockchain/enviofactura`, data)
  }

}

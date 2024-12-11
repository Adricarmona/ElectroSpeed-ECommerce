import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Bicicletas } from '../../models/catalogo';
import { FooterComponent } from '../footer/footer.component';
import CheckoutService from '../../service/checkout.service';
import { ActivatedRoute } from '@angular/router';
import { CatalogoService } from '../../service/catalogo.service';
import { BiciTemporal } from '../../models/bici-temporal';


@Component({
  selector: 'app-confirmacion-compra',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './confirmacion-compra.component.html',
  styleUrl: './confirmacion-compra.component.css'
})
export class ConfirmacionCompraComponent {
  nombre: string = ""

  tipoPago = "tarjeta"
  entrega = "Tu casa"
  res: string;

  productos: Bicicletas[] = []

  constructor(private authService: AuthService,    private service: CheckoutService,  private route: ActivatedRoute,private catalogoService: CatalogoService ) {
    this.devolverNombre()
  }

  ngOnInit(){
    this.res = this.route.snapshot.queryParamMap.get('id');
    //this.DevolverOrden()
  }

  async devolverNombre() {
    this.nombre = await this.authService.getNameUser()
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
    this.productos.push(bicicleta)
  }
}

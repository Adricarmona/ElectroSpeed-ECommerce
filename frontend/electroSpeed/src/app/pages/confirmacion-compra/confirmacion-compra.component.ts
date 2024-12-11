import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Bicicletas } from '../../models/catalogo';
import { FooterComponent } from '../footer/footer.component';
import { CheckoutService } from '../../service/checkout.service';
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
private BiciTemporal: BiciTemporal[]
  nombre: string = ""

  tipoPago = "targeta"
  entrega = "mi casa"
  res: string;

  productos: Bicicletas[] = [{
    id: 0,
    marcaModelo: 'nombreBisi',
    descripcion: '',
    precio: 400,
    stock: 0,
    urlImg: 'https://carbonbikes.es/cdn/shop/files/biciamarillanegra_800x.jpg?v=1694606380',
    cantidad: 5
  },
  {
    id: 0,
    marcaModelo: 'nombreBisi',
    descripcion: '',
    precio: 400,
    stock: 0,
    urlImg: 'https://carbonbikes.es/cdn/shop/files/biciamarillanegra_800x.jpg?v=1694606380',
    cantidad: 5
  },
]
  constructor(private authService: AuthService,    private service: CheckoutService,  private route: ActivatedRoute,private catalogoService: CatalogoService ) {
    this.devolverNombre()
  }

  ngOnInit(){
    this.res = this.route.snapshot.queryParamMap.get('id');
    this.DevolverOrden()
  }

  async devolverNombre() {
    this.nombre = await this.authService.getNameUser()
  }
  async DevolverOrden(){
    const request = await this.service.DevolverOrden(this.res)
    console.log(request.data)
    const bici= request.data
    bici.forEach(e => {
      
    });
  }

/*   datosBici(id: number){
    const bicicleta = await this.catalogoService.showOneBike(id2.idBici.toString());
  } */
}

import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogoService } from '../../service/catalogo.service';
import { PreciosPipe } from '../../pipes/precios.pipe';
import { ReseniasService } from '../../service/resenias.service';

@Component({
  selector: 'app-vista-detalle',
  standalone: true,
  imports: [PreciosPipe],
  templateUrl: './vista-detalle.component.html',
  styleUrl: './vista-detalle.component.css'
})
export class VistaDetalleComponent {

  constructor(private route: ActivatedRoute, private catalogoService: CatalogoService, private resenia: ReseniasService) {}

  codigoIdentificador: string = "";

  nombreModelo: string = "Modelo bicicleta - Marca bicicleta";
  descripcionBici: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea  ";
  precioBici: number = 1;
  stockBici: number = 0;
  fotoBici: string = "";

  // resenias
  idResenia: number = 0;
  textoResenia: string = "";
  resultadoResenia: number = 0;
  resultadoReseniaArray: string[] = [];
  fechaResenia: Date = new Date;
  

  async ngOnInit() {

    this.route.params.subscribe(params => {
      this.codigoIdentificador = params['id'];
    })

    /*
    *   traemos los datos de la bici con el id dado
    */
    const bicicleta = await this.catalogoService.showOneBike(this.codigoIdentificador)
    if (bicicleta == null) {
      //this.rickRoll() // rick roll si no existe
    } 
    else 
    {
      this.nombreModelo = bicicleta.marcaModelo
      this.descripcionBici = bicicleta.descripcion
      this.precioBici = bicicleta.precio
      this.stockBici = bicicleta.stock
      this.fotoBici = bicicleta.urlImg
    }

    const resenias = this.resenia.devolverResenia(0);
    this.idResenia = resenias.id;
    this.textoResenia = resenias.texto;
    this.resultadoResenia = resenias.resultado;
    this.fechaResenia = resenias.fechaResenia;

    for (let index = 0; index < this.resultadoResenia; index++) {
      this.resultadoReseniaArray.push("detalle/full.png");
    }

    for (let index = 0; index < 5-this.resultadoResenia; index++) {
      this.resultadoReseniaArray.push("detalle/empty.png");
    }

  }

  rickRoll() {
    window.location.href = 'https://youtu.be/dQw4w9WgXcQ';
  }
}
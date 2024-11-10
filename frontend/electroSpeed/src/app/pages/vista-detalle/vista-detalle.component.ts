import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vista-detalle',
  standalone: true,
  imports: [],
  templateUrl: './vista-detalle.component.html',
  styleUrl: './vista-detalle.component.css'
})
export class VistaDetalleComponent {

  constructor(private route: ActivatedRoute) {}

  codigoIdentificador: string = "";

  ngOnInit() {

    this.route.params.subscribe(params => {
      this.codigoIdentificador = params['id'];
    })

    /*
    *   traemos los datos de la bici con el id dado
    */


    /*
    *   hacemos un rick roll si no es la bici indicada
    */
    if( this.codigoIdentificador == "69") {
      this.rickRoll()
    }
  }

  rickRoll() {
    window.location.href = 'https://youtu.be/dQw4w9WgXcQ';
  }
}

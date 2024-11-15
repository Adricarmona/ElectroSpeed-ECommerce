import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogoService } from '../../service/catalogo.service';
import { PreciosPipe } from '../../pipes/precios.pipe';
import { ReseniasService } from '../../service/resenias.service';
import { Resenias } from '../../models/resenias';
import { Usuarios } from '../../models/usuarios';
import { Bicicletas } from '../../models/catalogo';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-vista-detalle',
  standalone: true,
  imports: [PreciosPipe],
  templateUrl: './vista-detalle.component.html',
  styleUrl: './vista-detalle.component.css'
})
export class VistaDetalleComponent {

  constructor(private route: ActivatedRoute, private catalogoService: CatalogoService, private resenia: ReseniasService, private authService: AuthService) {}

  codigoIdentificador: string = "";

  nombreModelo: string = "Modelo bicicleta - Marca bicicleta";
  descripcionBici: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea  ";
  precioBici: number = 1;
  stockBici: number = 0;
  fotoBici: string = "";

  // resenias
  resenias: Resenias[] = [];

  async ngOnInit() {

    this.route.params.subscribe(params => {
      this.codigoIdentificador = params['id'];
    })

    /*
    *   traemos los datos de la bici con el id dado
    */
    //const bicicleta = await this.catalogoService.showOneBike(this.codigoIdentificador)
    const bicicletas: Bicicletas[] = (await this.catalogoService.todasLasBicis()) ?? [];
    const bicicleta = bicicletas[0]
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

    this.resenias = this.resenia.devolverResenia(0);
    
  }

  rickRoll() {
    window.location.href = 'https://youtu.be/dQw4w9WgXcQ';
  }

  arrayResultados(resultado: number) {
    const resultadoReseniaArray: string[] = [];
    for (let index = 0; index < resultado; index++) {
      resultadoReseniaArray.push("detalle/full.png");
    }

    for (let index = 0; resultado + index < 5; index++) {
      resultadoReseniaArray.push("detalle/empty.png");
    }

    return resultadoReseniaArray
  }

  devolverUsuario(id: number) {
    return this.resenia.devolverUsuario(id)
  }

  devolverMediaResenias(): number {
    return this.resenia.devolverMediaResenias()
  }

  anadirCarrito(){
    if(localStorage.getItem("idbici")){
      localStorage.setItem("idbici", this.codigoIdentificador+","+localStorage.getItem("idbici"))
    } else {
      localStorage.setItem("idbici", this.codigoIdentificador)
    }
    
  }

  usuarioToken() {
    const token = this.authService.getToken()
    return token
  }

  verResenias(){
    const ponerResenias = document.getElementById("escribirReseñaForm")
    if (ponerResenias) {
      ponerResenias.style.display = "flex";
    }
  }
}
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogoService } from '../../service/catalogo.service';
import { PreciosPipe } from '../../pipes/precios.pipe';
import { ReseniasService } from '../../service/resenias.service';
import { Resenias } from '../../models/resenias';
import { AuthService } from '../../service/auth.service';
import { ReseniasYUsuario } from '../../models/resenias-yusuario';
import { FormsModule } from '@angular/forms';
import { AnadirResenias } from '../../models/anadir-resenias';
import { CarritoService } from '../../service/carrito.service';
import { Usuarios } from '../../models/usuarios';
import { CarritoEntero } from '../../models/carrito-entero';

@Component({
  selector: 'app-vista-detalle',
  standalone: true,
  imports: [PreciosPipe, FormsModule],
  templateUrl: './vista-detalle.component.html',
  styleUrl: './vista-detalle.component.css'
})
export class VistaDetalleComponent implements OnInit {

  constructor(private route: ActivatedRoute, private catalogoService: CatalogoService, private resenia: ReseniasService, private authService: AuthService, private carrito: CarritoService) {}

  codigoIdentificador: string = "";

  nombreModelo: string = "Modelo bicicleta - Marca bicicleta";
  descripcionBici: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea  ";
  precioBici: number = 1;
  stockBici: number = 0;
  fotoBici: string = "";
  mediaResenia: number = 0;

  // resenias
  resenias: ReseniasYUsuario[] = [];

  // enviar resenias
  textoResenia: string = "";

  async ngOnInit() {

    this.route.params.subscribe(params => {
      this.codigoIdentificador = params['id'];
    })

    /*
    *   traemos los datos de la bici con el id dado
    */
    const bicicleta = await this.catalogoService.showOneBike(this.codigoIdentificador)
    if (bicicleta == null) {
      this.rickRoll() // rick roll si no existe
    } 
    else 
    {
      this.nombreModelo = bicicleta.marcaModelo
      this.descripcionBici = bicicleta.descripcion
      this.precioBici = bicicleta.precio
      this.stockBici = bicicleta.stock
      this.fotoBici = bicicleta.urlImg
    }

    this.devolverMediaResenias()
    this.devolverTodasResenias()
    
  }

  rickRoll() {
    window.location.href = 'https://youtu.be/dQw4w9WgXcQ';
  }


  /*
  *   UNA FUNCION QUE HACE LA CANTIDAD DE PIEZAS EN UN ARRAY QUE INDICA UN NUMERO
  */
  arrayResultados(resultado: number) {
    const resultadoReseniaArray: string[] = [];
    for (let index = 0; index < resultado; index++) {
      resultadoReseniaArray.push("detalle/full.png");
    }

    for (let index = resultado; index <= 4; index++) {
      resultadoReseniaArray.push("detalle/empty.png");
    }

    return resultadoReseniaArray
  }

  /*
  *     CARRITO 
  */
  async anadirCarrito(){
    if (this.usuarioToken()) {

      const Usuario: Usuarios = await this.authService.getIdUserEmail(this.authService.getEmailUserToken())

      const carritoUsuarioActual: CarritoEntero = await this.carrito.devolverCarritoPorUsuario(Usuario.id)

      this.carrito.enviarCarrito(parseInt(this.codigoIdentificador), carritoUsuarioActual.id)

    } else {
      if(localStorage.getItem("idbici")){
        localStorage.setItem("idbici", this.codigoIdentificador+","+localStorage.getItem("idbici"))
      } else {
        localStorage.setItem("idbici", this.codigoIdentificador)
      }
    }
  }

  /*
  *   USUARIOS
  */
  async devolverUsuarioNombre(id: number) {
    return (await this.resenia.devolverUsuario(id)).name
  }

  usuarioToken() {
    const token = this.authService.getToken()
    return token
  }

  /*
  *   RESEÑAS
  */
  verResenias(){
    const ponerResenias = document.getElementById("escribirReseñaForm")
    if (ponerResenias) {
      ponerResenias.style.display = "flex";
    }
  }

  devolverMediaResenias() {
    this.resenia.devolverMediaResenias(this.codigoIdentificador).then(value => 
      this.mediaResenia = value
    )
  }

  async devolverTodasResenias() {
    const reseniasAhora: Resenias[] = await this.resenia.devolverResenia(this.codigoIdentificador);

    for(const element of reseniasAhora){
      const nombreUsuario = await this.devolverUsuarioNombre(element.usuarioId)

      const ReseniasYUsuario : ReseniasYUsuario = {
        resenias: element,
        usuario: nombreUsuario
      }

      this.resenias.push(ReseniasYUsuario)
    }
  }

  /*
  *   ENVIAR RESENIAS
  */
  async enviarResenias(){
    const reseniasEnviar : AnadirResenias = {
      texto: this.textoResenia,
      usuarioId: await this.resenia.devolverIdUsuario(this.authService.getEmailUserToken()),
      bicicletaId: parseInt(this.codigoIdentificador)
    }
    
    this.resenia.enviarResenas(reseniasEnviar)
    location.reload()
  }
}
import { Component } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { CarritoService } from '../../service/carrito.service';
import { Bicicletas } from '../../models/catalogo';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [],
  templateUrl: './carrito.component.html',
  styleUrl: './carrito.component.css',
})
export class CarritoComponent {
  constructor(
    private catalogoService: CatalogoService,
    private carritoService: CarritoService,
    private auth: AuthService
  ) {}
  
  codigoIdentificador: string[] = [];
  nombreModelo: string = 'Modelo bicicleta - Marca bicicleta';
  precioBici: number = 1;
  fotoBici: string = '';
  idUser: number = 0;
  bicicletaCarrito: Bicicletas[] = [];

  async ngOnInit() {
    const iddata = localStorage.getItem('idbici');
    const tokenDataSession = sessionStorage.getItem('token');
    const tokenDataLocal = localStorage.getItem('token');
    if (iddata || tokenDataSession || tokenDataLocal) {
      const ids = iddata ? iddata.split(',').map((id) => id.trim()) : [];

      this.idUser = await (await this.auth.getIdUserEmail(this.auth.getEmailUserToken())).id

      console.log(this.idUser)
      const carrito = await this.carritoService.getIdCarrito(this.idUser);
      console.log(carrito)
      this.codigoIdentificador = ids;

      for (const id of this.codigoIdentificador) {
        const bicicleta = await this.catalogoService.showOneBike(id);
        if (bicicleta) {
          this.bicicletaCarrito.push(bicicleta);
        } else {
          console.log(`No se encontró bicicleta con ID ${id}`);
        }
      }
    }
  }

  calcularTotal(): number {
    return this.bicicletaCarrito.reduce((total, bici) => total + bici.precio, 0);
  }

  eliminarBici(id: number){
    const idsUpdated = this.bicicletaCarrito.map((bici) => bici.id)
    localStorage.setItem('idbici', JSON.stringify(idsUpdated));
    this.bicicletaCarrito.splice(id, 1);
  }
}



import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Filtro } from '../models/filtro';
import { lastValueFrom, Observable } from 'rxjs';
import { BiciPagina } from '../models/bici-pagina';
import { Bicicletas } from '../models/catalogo';
import { ApiService } from './api-service';
import { BicisFile } from '../models/bicis-file';

@Injectable({
  providedIn: 'root'
})
export class CatalogoService {
  private BASE_URL = `${environment.apiUrl}`;

  constructor(
    private http: HttpClient,
    private api: ApiService
  ) {}

  async showBikes(formaDeVer: Filtro){
    try{
      const request: Observable<BiciPagina> = this.http.post<BiciPagina>(`${this.BASE_URL}filtroBicis`,formaDeVer);
      const result: BiciPagina = await lastValueFrom(request);

      return result;
    }
    catch(error: any)
    {
    console.error("Error al usar el filtro: ", error);
    return null;
    }
  }


  async showOneBike(id: string){
    try {
      const request: Observable<Bicicletas> = this.http.get<Bicicletas>(`${this.BASE_URL}bicicleta?id=${id}`);
      const result: Bicicletas = await lastValueFrom(request);
      return result
    } catch (error) {
      console.error("Error al buscar la bici: ",error)
      return null
    }
  }

  async todasLasBicis(): Promise<Bicicletas[] | null> {
    try {
        // Cambia el tipo de solicitud para obtener un array de `Bicicletas` en lugar de un solo objeto.
        const request: Observable<Bicicletas[]> = this.http.get<Bicicletas[]>(`${this.BASE_URL}api/Bike`);
        const result: Bicicletas[] = await lastValueFrom(request);

        return result; // Devolverá un array, ya sea vacío o con elementos.
    } catch (error) {
        console.error("Error al buscar la bici: ", error);
        return null; // Devuelve `null` en caso de error.
    }
  }

  anadirBicicleta(bicicletas :BicisFile) {

    const formData = new FormData();

    formData.append('Id', bicicletas.id.toString())
    formData.append('MarcaModelo',bicicletas.marcaModelo)
    formData.append('Descripcion',bicicletas.descripcion)
    formData.append('Stock',bicicletas.stock.toString())
    formData.append('Precio',bicicletas.precio.toString())
    formData.append('UrlImg',bicicletas.urlImg)

    try {
      this.api.post("anadirBici",formData)
    } catch (error) {
      console.error("Error al buscar la bici: ", error);
    }
  }

  async editarBicicleta(bicicletas :BicisFile) {

    const formData = new FormData();

    formData.append('Id', bicicletas.id.toString())
    formData.append('MarcaModelo',bicicletas.marcaModelo)
    formData.append('Descripcion',bicicletas.descripcion)
    formData.append('Stock',bicicletas.stock.toString())
    formData.append('Precio',bicicletas.precio.toString())
    formData.append('UrlImg',bicicletas.urlImg)

    try {
      const resultado = await this.api.post("editarBici",formData)
    } catch (error) {
      console.error("Error al buscar la bici: ", error);
    }
  }

  eliminarBicicleta(id :number){
    try {
      this.api.delete("deleteBikeId?id="+id)
    } catch (error) {
      console.error("Error eliminar la bici: ", error);
    }
    
  }

}

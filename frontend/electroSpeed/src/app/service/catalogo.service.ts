import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { HttpClient } from '@angular/common/http';
import { Filtro } from '../models/filtro';
import { lastValueFrom, Observable } from 'rxjs';
import { BiciPagina } from '../models/bici-pagina';
import { Bicicletas } from '../models/catalogo';

@Injectable({
  providedIn: 'root'
})
export class CatalogoService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

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


  async showOneBike(id: String){
    try {
      const request: Observable<Bicicletas> = this.http.get<Bicicletas>(`${this.BASE_URL}bici/${id}`);
      const result: Bicicletas = await lastValueFrom(request);

      return result
    } catch (error) {
      console.error("Error al buscar la bici: ",error)
      return null
    }
  }
}

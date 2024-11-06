import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { HttpClient } from '@angular/common/http';
import { Filtro } from '../models/filtro';
import { lastValueFrom, Observable } from 'rxjs';
import { BiciPagina } from '../models/bici-pagina';

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
}

///https://localhost:7189/filtroBicis

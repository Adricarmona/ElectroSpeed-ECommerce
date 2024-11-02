import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { HttpClient } from '@angular/common/http';
import { Filtro } from '../models/filtro';

@Injectable({
  providedIn: 'root'
})
export class CatalogoService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async showBikes(formaDeVer: Filtro){
    try{

      const request

    }
  } catch(error){
    console.error("Error al usar el filtro: ", error);
    return null;
  }
}

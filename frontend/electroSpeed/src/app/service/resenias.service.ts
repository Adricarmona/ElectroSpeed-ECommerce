import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { Resenias } from '../models/resenias';

@Injectable({
  providedIn: 'root'
})
export class ReseniasService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  devolverResenia(id:  number){
    //          const request: Observable<BiciPagina> = this.http.post<BiciPagina>(`${this.BASE_URL}filtroBicis`,formaDeVer);
    //          const result: BiciPagina = await lastValueFrom(request);

    
    const resenia: Resenias = 
    {
      id: id,
      texto: "texto",
      resultado: 3,
      fechaResenia: new Date,
      idUsuario: 1,
    }

    return resenia
  }

}

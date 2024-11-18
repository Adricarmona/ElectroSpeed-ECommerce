import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { Resenias } from '../models/resenias';
import { Usuarios } from '../models/usuarios';
import { lastValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReseniasService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  devolverResenia(id:  number){
    //  const request: Observable<number> = this.http.get<number>(`${this.BASE_URL}mediaResenia?id=${id}`);
    //  const result: BiciPagina = await lastValueFrom(request);

    
    const resenias: Resenias[] = [];
    resenias.push(
    {
      id: id,
      texto: "texto",
      resultado: 3,
      fechaResenia: new Date,
      idUsuario: 1,
    },
    {
      id: id+1,
      texto: "texto1",
      resultado: 2,
      fechaResenia: new Date,
      idUsuario: 2,
    },
    {
      id: id+2,
      texto: "texto2",
      resultado: 4,
      fechaResenia: new Date,
      idUsuario: 3,
    },
    {
      id: id+3,
      texto: "La zowii en la casaa",
      resultado: 3,
      fechaResenia: new Date,
      idUsuario: 0,
    },
  )

    return resenias
  }

  devolverUsuario(id:  number){
    //          const request: Observable<BiciPagina> = this.http.post<BiciPagina>(`${this.BASE_URL}filtroBicis`,formaDeVer);
    //          const result: BiciPagina = await lastValueFrom(request);

    
    const usuarios: Usuarios =
    {
      id: id,
      name: "noe",
      email: "noe@electrospeed.es",
      username: "noexxnoe",
      picture: "detalle/noe.png",
    }


    return usuarios
  }

  async devolverMediaResenias(id: string){
    const request: Observable<number> = this.http.get<number>(`${this.BASE_URL}mediaResenia?id=${id}`);
    const result: number = await lastValueFrom(request);

    return result
  }
}

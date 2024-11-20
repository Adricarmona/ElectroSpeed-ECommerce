import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/enviroments.developments';
import { Resenias } from '../models/resenias';
import { Usuarios } from '../models/usuarios';
import { lastValueFrom, Observable } from 'rxjs';
import { AnadirResenias } from '../models/anadir-resenias';

@Injectable({
  providedIn: 'root'
})
export class ReseniasService {

  private BASE_URL = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  async devolverResenia(id:  string): Promise<Resenias[]>{
    const request: Observable<Resenias[]> = this.http.get<Resenias[]>(`${this.BASE_URL}idBici?id=${id}`);
    const result: Resenias[] = await lastValueFrom(request);

    return result
  }

  async devolverUsuario(id:  number) :Promise<Usuarios>{
    const request: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioId?id=${id}`);
    const result: Usuarios = await lastValueFrom(request);

    return result
  }

  async devolverMediaResenias(id: string){
    const request: Observable<number> = this.http.get<number>(`${this.BASE_URL}mediaResenia?id=${id}`);
    const result: number = await lastValueFrom(request);

    return result
  }

  async enviarResenas(enviar :AnadirResenias) {
    try {
      const respuesta = this.http.post(`${this.BASE_URL}IAanadir`, enviar,{ responseType: 'text' })
      const result = await lastValueFrom(respuesta)
    } catch (error) {
      console.log(error)
    }
    
  }

  async devolverIdUsuario(correo: string){
    const request: Observable<Usuarios> = this.http.get<Usuarios>(`${this.BASE_URL}usuarioEmail?email=${correo}`);
    const result: Usuarios = await lastValueFrom(request);

    return result.id
  }
}

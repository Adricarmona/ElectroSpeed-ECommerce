import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RedirectionService {

  public isLogged: boolean = false;

  constructor() { 
    console.log("Se crea servicio auth"); 
  }

  login() {  
    console.log(this.isLogged);
    this.isLogged = true;
    console.log(this.isLogged);
  }
}

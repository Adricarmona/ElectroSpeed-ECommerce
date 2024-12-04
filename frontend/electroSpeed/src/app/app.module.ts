import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UsuarioComponent } from './pages/usuario/usuario.component';

@NgModule({
  declarations: [
    UsuarioComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
  ],
  providers: [],
})
export class AppModule { }

import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InicioComponent } from "./pages/inicio/inicio.component";
import { NavbarComponent } from './pages/navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, InicioComponent, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'electroSpeed';
  ngOnInit(): void {
    console.log(window.ethereum);

  }
}
declare global {
  interface Window {
    ethereum: any;
  }
}


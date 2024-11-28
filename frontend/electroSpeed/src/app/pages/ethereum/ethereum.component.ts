import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ethereum',
  standalone: true,
  imports: [],
  templateUrl: './ethereum.component.html',
  styleUrl: './ethereum.component.css'
})
export class EthereumComponent implements OnInit {
  
  async ngOnInit() {
    const accounts = await window.ethereum.request({method:'eth_requestAccounts'});
    console.log(accounts)
  }
}



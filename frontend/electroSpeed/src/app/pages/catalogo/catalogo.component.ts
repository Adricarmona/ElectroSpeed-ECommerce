import { Component, Input } from '@angular/core';
import { CatalogoService } from '../../service/catalogo.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent {
  isDropdownVisible = false;
  nombre: boolean = false;
  precio: boolean = false;
  asc: boolean = false;
  desc: boolean = false;

  @Input() items: { label: string }[] = []; 
  @Input() groupName: string = '';
  @Input() groupPrice: string = ''; 
  selectedIndex: number | null = null; 
  selectedIndex1: number | null = null; 

  onCheckboxClickPrice(index: number) {
    this.selectedIndex1 = this.selectedIndex1 === index ? null : index;
  }
  onCheckboxClickName(index1: number) {
    this.selectedIndex = this.selectedIndex === index1 ? null : index1;
  }

  constructor(private catalogoService: CatalogoService) {}

  toggleDropdown(): void {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  async submitFiltro() {}

}

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'precios',
  standalone: true
})
export class PreciosPipe implements PipeTransform {

  transform(value: number): string { // el toFixed redondea a 2 decimales
    return (value).toFixed(2).replace('.', ',');
  }

}

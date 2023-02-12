import { Pipe, PipeTransform } from '@angular/core';

//ng generate pipe is-urgent

@Pipe({
  name: 'isUrgent',
})
export class IsUrgentPipe implements PipeTransform {
  transform(value: boolean): string {
    return value ? '!' : '';
  }
}

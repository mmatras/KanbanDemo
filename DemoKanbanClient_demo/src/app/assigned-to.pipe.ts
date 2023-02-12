import { Pipe, PipeTransform } from '@angular/core';

//ng generate pipe assigned-to

@Pipe({
  name: 'assignedTo',
})
export class AssignedToPipe implements PipeTransform {
  transform(value: string): string {
    return value && value != '' ? value : '-';
  }
}

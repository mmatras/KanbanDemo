import { Component, Input } from '@angular/core';
import { IIssueDto } from 'src/dtos/project';

//ng generate component tile

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.css'],
})
export class TileComponent {
  @Input() issue?: IIssueDto;
}

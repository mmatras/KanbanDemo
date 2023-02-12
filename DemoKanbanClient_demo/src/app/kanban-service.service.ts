import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IIssueDto, IPersonSelectDto } from 'src/dtos/project';
import { Observable } from 'rxjs';

//ng generate service kanban-service

@Injectable({
  providedIn: 'root',
})
export class KanbanServiceService {
  constructor(private http: HttpClient) {}

  getIssues(): Observable<IIssueDto[]> {
    return this.http.get<IIssueDto[]>('/api/issue');
  }

  getPersonSelect(): Observable<IPersonSelectDto[]> {
    return this.http.get<IPersonSelectDto[]>('/api/person/personSelect');
  }
}

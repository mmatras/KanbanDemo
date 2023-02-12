import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  IAccecTokenDto,
  IEditIssueDto,
  IIssueDto,
  ILoginDto,
  IPersonSelectDto,
} from 'src/dtos/project';
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

  getIssue(id: number): Observable<IIssueDto> {
    return this.http.get<IIssueDto>(`/api/issue/${id}`);
  }

  getPersonSelect(): Observable<IPersonSelectDto[]> {
    return this.http.get<IPersonSelectDto[]>('/api/person/personSelect');
  }

  putIssue(issue: IEditIssueDto) {
    return this.http.put(`/api/issue/${issue.id}`, issue);
  }

  postIssue(issue: IEditIssueDto) {
    return this.http.post('/api/issue', issue);
  }

  userAuth(login: ILoginDto): Observable<IAccecTokenDto> {
    return this.http.post<IAccecTokenDto>('api/auth', login);
  }
}

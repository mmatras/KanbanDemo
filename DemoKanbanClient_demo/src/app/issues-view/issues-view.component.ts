import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IIssueDto, IssueState } from 'src/dtos/project';
import { KanbanServiceService } from '../kanban-service.service';

@Component({
  selector: 'app-issues-view',
  templateUrl: './issues-view.component.html',
  styleUrls: ['./issues-view.component.css'],
})
export class IssuesViewComponent implements OnInit {
  public issuesTodo: IIssueDto[] = [];
  public issuesDoing: IIssueDto[] = [];
  public issuesDone: IIssueDto[] = [];

  constructor(
    private kanbanService: KanbanServiceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.kanbanService.getIssues().subscribe({
      next: (result) => {
        this.issuesTodo = result.filter(
          (issue) => issue.state == IssueState.Todo
        );
        this.issuesDoing = result.filter(
          (issue) => issue.state == IssueState.Doing
        );
        this.issuesDone = result.filter(
          (issue) => issue.state == IssueState.Done
        );
      },
      error: (err: HttpErrorResponse) => {
        if (err.status === 401) this.router.navigate(['/login']);
      },
    });
  }
}

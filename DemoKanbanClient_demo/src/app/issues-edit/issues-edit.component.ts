import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IPersonSelectDto, IssueState } from 'src/dtos/project';
import { KanbanServiceService } from '../kanban-service.service';

@Component({
  selector: 'app-issues-edit',
  templateUrl: './issues-edit.component.html',
  styleUrls: ['./issues-edit.component.css'],
})
export class IssuesEditComponent implements OnInit {
  public issueForm: FormGroup = new FormGroup({
    id: new FormControl(0),
    title: new FormControl('', Validators.required),
    notes: new FormControl(''),
    state: new FormControl(IssueState.Todo),
    isUrgent: new FormControl(false),
    deadline: new FormControl(new Date().toISOString().substring(0, 10)),
    assignedToId: new FormControl(0),
  });

  public issueStates: { name: string; value: string }[] = [];
  public personSelect: IPersonSelectDto[] = [];

  constructor(
    private kanbanService: KanbanServiceService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    for (const key in Object.keys(IssueState)) {
      const statusName = IssueState[key];
      if (typeof statusName === 'string')
        this.issueStates.push({ name: statusName, value: key });
    }
  }

  ngOnInit(): void {
    this.activatedRoute.url.subscribe((url) => {
      this.kanbanService.getIssue(parseInt(url[2].path)).subscribe((iss) => {
        iss.deadline = iss.deadline.substring(0, 10);
        this.issueForm.patchValue(iss);
      });
    });

    this.kanbanService
      .getPersonSelect()
      .subscribe((personSelect) => (this.personSelect = personSelect));
  }

  onSubmit() {
    console.log(this.issueForm.value);

    if (this.issueForm.valid) {
      this.kanbanService
        .putIssue(this.issueForm.value)
        .subscribe((result) => this.router.navigate(['']));
    }
  }
}

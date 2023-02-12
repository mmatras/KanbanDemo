import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { KanbanServiceService } from '../kanban-service.service';

//ng generate component login

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  public loginForm: FormGroup = new FormGroup({
    login: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(
    private kanbanServcie: KanbanServiceService,
    private router: Router
  ) {}

  onSubmit() {
    if (this.loginForm.valid) {
      this.kanbanServcie.userAuth(this.loginForm.value).subscribe((result) => {
        window.localStorage.setItem('accessToken', result.value);
        this.router.navigate(['']);
      });
    }
  }
}

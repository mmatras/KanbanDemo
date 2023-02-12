import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IssuesViewComponent } from './issues-view/issues-view.component';
import { IssuesEditComponent } from './issues-edit/issues-edit.component';
import { IsUrgentPipe } from './is-urgent.pipe';
import { AssignedToPipe } from './assigned-to.pipe';
import { TileComponent } from './tile/tile.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    IssuesViewComponent,
    IssuesEditComponent,
    IsUrgentPipe,
    AssignedToPipe,
    TileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
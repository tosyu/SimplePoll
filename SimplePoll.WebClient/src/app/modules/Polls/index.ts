import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from "@angular/material/button";
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from "@angular/material/dialog";
import { MatIconModule } from "@angular/material/icon";
import { PollsRoutingModule } from "./routing";
import { PollsListComponent } from "./components/PollsList/polls-list.component";
import { PollSubmissionFormComponent } from "./components/PollSubmissionForm/poll-submission-form.component";
import { ReactiveFormsModule } from "@angular/forms";
import { PollSubmissionDialogComponent } from "./components/PollSubmissionDialog/poll-submission-dialog.component";

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatRadioModule,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatListModule,
    MatDialogModule,
    PollsRoutingModule
  ],
  declarations: [
    PollsListComponent,
    PollSubmissionFormComponent,
    PollSubmissionDialogComponent
  ]
})
export class PollsModule { }

import { map, tap } from 'rxjs/operators';
import { Component, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { MatDialog } from '@angular/material/dialog';
import { PollFormElement } from "src/app/models/polls-forms.types";
import { PollConfigurationDto, PollSubmissionRequestDto } from "src/app/models/polls.types";
import { PollsFormsService } from "src/app/services/polls-forms.service";
import { PollsService } from "src/app/services/polls.service";
import { PollSubmissionDialogComponent } from '../PollSubmissionDialog/poll-submission-dialog.component';



@Component({
  selector: 'poll-submission-form',
  templateUrl: './poll-submission-form.component.html',
  styleUrls: ['./poll-submission-form.component.css']
})
export class PollSubmissionFormComponent implements OnInit {
  pollForm: FormGroup = new FormGroup({});
  controls: PollFormElement[] = [];
  pollId?: number;
  configuration?: PollConfigurationDto;

  constructor(
    private activatedRoute: ActivatedRoute,
    private pollService: PollsService,
    private pollFormService: PollsFormsService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.pollId = parseInt(this.activatedRoute.snapshot.paramMap.get("id") as unknown as string, 10); // TODO fix this casting!

    this.pollService.getPoll(this.pollId) // TODO fix this casting!
      .pipe(
        tap(configuration => this.configuration = configuration),
        map(configuration => this.pollFormService.parsePollConfigurationToFormElements(configuration))
      )
      .subscribe(response => this.updateFormControls(response));
  }

  submitForm(): void {
    this.pollForm.updateValueAndValidity();
    if (this.configuration && this.pollId !== undefined && this.pollForm.valid) {
      var request: PollSubmissionRequestDto = {
        userName: 'test', // TODO not required in task but supplied anyway
        elements: this.pollFormService.parseFormToSubmissionRequestElements(this.pollForm, this.configuration)
      };

      this.pollService.submit(this.pollId, request)
        .subscribe(result => {
          const dialogRef = this.dialog.open(PollSubmissionDialogComponent, { data: result })
          dialogRef.afterClosed().subscribe(() => {
            this.router.navigate(["/polls-list"]);
          });
        }, console.error); // TODO handle this
    }
  }

  resetForm(): void {
    this.pollForm.reset();
  }

  private updateFormControls(formElements: PollFormElement[]) {
    this.controls = formElements;

    formElements.forEach(element => this.pollForm.addControl(element.name, element.formControl));
  }
}

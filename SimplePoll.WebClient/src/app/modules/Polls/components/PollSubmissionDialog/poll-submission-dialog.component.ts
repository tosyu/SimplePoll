import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  pollResultId: number;
  createdAt: string;
}
@Component({
  selector: 'poll-submittion-dialog',
  templateUrl: './poll-submission-dialog.component.html',
})
export class PollSubmissionDialogComponent {
  createdAt?: string;
  pollResultId?: number;

  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.createdAt = data.createdAt;
    this.pollResultId = data.pollResultId;
  }
}

import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { PollsService } from "./polls.service";
import { PollsFormsService } from "./polls-forms.service";
import { MatSnackBarModule } from "@angular/material/snack-bar";

@NgModule({
  imports: [
    HttpClientModule,
    MatSnackBarModule
  ],
  providers: [
    PollsService,
    PollsFormsService
  ]
})
export class ServicesModule {}

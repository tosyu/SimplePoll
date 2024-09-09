import { NgModule } from "@angular/core";
import { PollsListComponent } from "./components/PollsList/polls-list.component";
import { RouterModule, Routes } from "@angular/router";
import { PollSubmissionFormComponent } from "./components/PollSubmissionForm/poll-submission-form.component";

const pollRoutes: Routes = [
  { path: 'polls-list', component: PollsListComponent },
  { path: 'poll/:id', component: PollSubmissionFormComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(pollRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class PollsRoutingModule {}

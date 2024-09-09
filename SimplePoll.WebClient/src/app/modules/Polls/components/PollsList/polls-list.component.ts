import { Component } from "@angular/core";
import { PollConfigurationDto } from "src/app/models/polls.types";
import { PollsService } from "src/app/services/polls.service";

@Component({
  selector: 'polls-list',
  templateUrl: './polls-list.component.html',
  styleUrls: ['./polls-list.component.css']
})
export class PollsListComponent {
  polls: PollConfigurationDto[] = [];

  constructor(private pollService: PollsService) {
    this.pollService.getPolls().subscribe(response => this.polls = response)
  }
}

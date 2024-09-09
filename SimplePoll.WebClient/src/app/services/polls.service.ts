import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PollConfigurationDto, PollSubmissionRequestDto, PollSubmissionResultDto } from "../models/polls.types";

import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable({providedIn: "root"})
export class PollsService {
  constructor(private httpClient: HttpClient) {}

  getPolls(): Observable<PollConfigurationDto[]> {
    return this.httpClient
      .get<PollConfigurationDto[]>(`${environment.webApiUrl}/Poll/All`);
  }

  getPoll(id: number): Observable<PollConfigurationDto> {
    return this.httpClient
      .get<PollConfigurationDto>(`${environment.webApiUrl}/Poll/${id}`);
  }

  submit(id: number, data: PollSubmissionRequestDto): Observable<PollSubmissionResultDto> {
    return this.httpClient
      .post<PollSubmissionResultDto>(`${environment.webApiUrl}/Poll/${id}`, data);
  }
}

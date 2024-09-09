export interface PollValueConfigurationDto {
  id: number;
  name: string;
}

export interface PollElementConfigurationDto {
  id: number;
  name: string;
  type: string;
  subtype?: string;
  min?: number;
  max?: number;
  values: PollValueConfigurationDto[];
}

export interface PollConfigurationDto {
  id: number;
  name: string;
  elements: PollElementConfigurationDto[];
}

export interface PollSubmissionRequestElementDto {
  id: number;
  value: string;
}

export interface PollSubmissionRequestDto {
  userName: string;
  elements: PollSubmissionRequestElementDto[];
}

export interface PollSubmissionResultDto {
  pollResultId: number;
  createdAt: Date | string | number;
}

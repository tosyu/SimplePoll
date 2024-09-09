import { FormControl } from "@angular/forms";
import { PollValueConfigurationDto } from "./polls.types";

export interface PollFormElement {
  formControl: FormControl;
  name: string;
  description: string;
  id: number;
  type: string;
  subtype: string;
  values: PollValueConfigurationDto[];
}

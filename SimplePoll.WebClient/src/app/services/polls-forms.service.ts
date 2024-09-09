import { Injectable } from "@angular/core";
import { PollFormElement } from "../models/polls-forms.types";
import { PollConfigurationDto,  PollSubmissionRequestElementDto, PollValueConfigurationDto } from "../models/polls.types";
import { FormControl, FormGroup, ValidatorFn, Validators } from "@angular/forms";

@Injectable({providedIn: "root"})
export class PollsFormsService {

  public parsePollConfigurationToFormElements(formConfiguration: PollConfigurationDto): PollFormElement[] {
    return formConfiguration.elements.map(element => {
      const type = element.type?.toLowerCase();
      const subType = element.subtype?.toLowerCase();
      const validators: ValidatorFn[] = [];

      // TODO force all required for now
      // should be configurable but not
      // specified by task
      validators.push(Validators.required);

      // TODO rework validator setup to something more
      // readable
      if (type === "textarea" || subType === "text") {
        if (element.min) {
          validators.push(Validators.minLength(element.min));
        }

        if (element.max) {
          validators.push(Validators.maxLength(element.max));
        }
      } else {
        if (element.min) {
          validators.push(Validators.min(element.min));
        }

        if (element.max) {
          validators.push(Validators.max(element.max));
        }
      }

      const control: PollFormElement = {
        formControl: new FormControl('', validators),
        id: element.id,
        description: element.name,
        name: `control_${element.id}`,
        values: element.values,
        type,
        subtype: element.subtype?.toLowerCase() ?? ''
      };

      return control;
    });
  }

  public parseFormToSubmissionRequestElements(form: FormGroup, formConfiguration: PollConfigurationDto): PollSubmissionRequestElementDto[] {
    const values = form.getRawValue();

    return Object.keys(values).map(key => {
      const id = parseInt(key.replace("control_", ""), 10);
      const configuration = formConfiguration.elements.find(p => p.id === id);
      const value = Array.isArray(values[key]) ? JSON.stringify(this.getValueFromValueList(values[key], configuration?.values as PollValueConfigurationDto[])) : values[key];
      return { id, value };
    });
  }

  private getValueFromValueList(ids: number[], list: PollValueConfigurationDto[]): string[] {
    const names = ids.map(id => {
      return list.find(p => p.id === id)?.name
    }).filter(p => p !== undefined) as string[];

    return names;
  }
}

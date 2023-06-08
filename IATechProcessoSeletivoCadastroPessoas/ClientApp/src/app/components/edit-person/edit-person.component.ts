import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { createMask } from '@ngneat/input-mask';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-edit-person',
  templateUrl: './edit-person.component.html',
  styleUrls: ['./edit-person.component.scss']
})
export class EditPersonComponent{
  @Input() editPersonForm!: FormGroup<any>;
  @Input() personList!: IPerson[];
  cpfInputMask = createMask({
    alias: '999.999.999-99',
    inputFormat: '999.999.999-99',
    parser: (value: string) => {
      const parsedValue = value.replaceAll('.', '').replaceAll('-', '');
      return parsedValue;
    }
  })
  phoneMask = createMask({
    alias: '(99) 99999-9999',
    nullable: false,
    parser: (value: string) => {
      const parsedValue = value.replaceAll(' ', '').replaceAll('-', '').replace('(', '').replace(')', '');
      return Number(parsedValue);
    }
  })

  constructor(private fb:FormBuilder, private personService: PersonApiService) {
  }

  phones() : FormArray {
    return this.editPersonForm.get("phones") as FormArray
  }

  newPhone(): FormGroup {
    return this.fb.group({
      number: ''
    })
  }

  addPhone() {
    this.phones().push(this.newPhone());
  }

  removePhone(i:number) {
    this.phones().removeAt(i);
  }

  saveEditedPerson() {
    const modal = document.querySelector('#modal');
    const {editBirth, editName, editCpf, id, phones} = this.editPersonForm.value;
    const person = {
      id,
      birth: new Date(editBirth),
      name: editName,
      cpf: Number(editCpf),
      phones,
    }

    this.personService.updatePerson(person, person.id).subscribe();
    location.reload();
  }
}

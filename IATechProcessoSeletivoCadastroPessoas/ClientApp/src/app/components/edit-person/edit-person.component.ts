import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-edit-person',
  templateUrl: './edit-person.component.html',
  styleUrls: ['./edit-person.component.scss']
})
export class EditPersonComponent{
  @Input() editPersonForm!: FormGroup<any>;

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
    const {editBirth, editName, editCpf, id, phones} = this.editPersonForm.value;
    const person = {
      id,
      birth: new Date(editBirth),
      name: editName,
      cpf: Number(editCpf),
      phones,
    }

    this.personService.updatePerson(person, person.id);
  }
}

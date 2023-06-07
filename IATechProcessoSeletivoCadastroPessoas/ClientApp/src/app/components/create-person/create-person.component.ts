import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-create-person',
  templateUrl: './create-person.component.html',
  styleUrls: ['./create-person.component.scss']
})
export class CreatePersonComponent {
  name = '';
  cpf = '';
  birth = '';
  phoneForm: FormGroup;

  constructor(private fb:FormBuilder, private personService: PersonApiService) {
    const { birth, cpf, name } = this;
    this.phoneForm = this.fb.group({
      name,
      cpf,
      birth,
      phones: this.fb.array([]),
    });
  }

  phones() : FormArray {
    return this.phoneForm.get("phones") as FormArray
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

  createPerson() {
    const {birth, name, cpf, phones} = this.phoneForm.value;
    const person = {
      birth: new Date(birth),
      name,
      cpf: Number(cpf),
      phones
    }

    this.personService.createPerson(person).subscribe();
    location.reload();
  }
}

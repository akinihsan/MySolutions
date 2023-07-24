import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AnimalClient, AnimalDto } from 'src/app/client/apiclient';



@Component({
  selector: 'app-animal-edit',
  templateUrl: './animal-edit.component.html',
  styleUrls: ['./animal-edit.component.scss']
})
export class AnimalEditComponent implements OnInit {

  animal: AnimalDto = {};


  submitted: boolean = false;
  formAnimal: FormGroup;

  selectedItemExternalId?: string | undefined;

  constructor(private messageService: MessageService, private animalClient: AnimalClient,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder) {

    this.formAnimal = this.formBuilder.group({
      name: [null, [Validators.required]],
    });

  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.selectedItemExternalId = params['animalId'];
    });

    this.editAnimal(this.selectedItemExternalId);
  }


  cancel() {
    this.router.navigateByUrl('/admin/farm/animals');
  }

  editAnimal(animalId: string | undefined) {
    if (animalId != null) {
      this.animalClient.getById(animalId).subscribe(result => {
        if (result.isSuccess) {
          this.animal.externalId = result.data?.externalId;
          this.animal.name = result.data?.name;
        }
        else {

          this.messageService.add({ severity: 'error', summary: 'Error', detail: result.message });
        }}
        );
    } 
  }

  saveAnimal() {

    this.submitted = true;
    if (this.selectedItemExternalId != null)
    {
      this.animal.externalId = this.selectedItemExternalId;

      this.animalClient.put(this.animal).subscribe({
        next: s => {
          if (s.isSuccess) {
            this.animal.externalId = s.data;
            this.messageService.add({
              severity: 'success', summary: 'Success',
              detail: "Updated successfully.", life: 3000
            });
            this.router.navigateByUrl('/admin/farm/animals');
          }
          else {
            this.submitted = false;
            this.messageService.add({ severity: 'error', summary: 'Error', detail: s.message, life: 3000 });
          }
        }
      });
    }
    else {
      
      this.animalClient.post(this.animal.name).subscribe({
        next: s => {
          if (s.isSuccess) {
            this.animal.externalId = s.data;
            this.messageService.add({
              severity: 'success', summary: 'Success',
              detail: "Added successfully.", life: 3000
            });
            this.router.navigateByUrl('/admin/farm/animals');
          }
          else {
            this.submitted = false;
            this.messageService.add({ severity: 'error', summary: 'Error', detail: s.message, life: 3000 });
          }
        }
      });
    }
  
  }

  isFieldValid(form: FormGroup, field: string) {
    if (!form)
      return true;
    return !form.get(field)!.valid && form!.get(field)!.touched;
  }
  getform(formName: string): FormGroup {
    let form: FormGroup = Object.create(null);
    if (formName == 'formAnimal')
      form = this.formAnimal;
    return form
  }
  displayFieldCss(formName: string, field: string) {
    let form = this.getform(formName);
    return {
      'ng-dirty ng-invalid': this.isFieldValid(form, field)
    };
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AnimalClient, AnimalDto } from 'src/app/client/apiclient';

@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.scss']
})
export class AnimalListComponent implements OnInit {


  animals: AnimalDto[] = [];
  animal: AnimalDto = {};
  selectedAnimals: AnimalDto[] = [];

  constructor(private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private animalClient: AnimalClient,
    private router: Router) {

  }


  ngOnInit() {
    this.getBranches();
  }

  getBranches() {
    this.animalClient.get().subscribe(result => {
      if (result.isSuccess) {
        this.animals = result.data!
      }
      else {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors![0].message });
      }
    });
  }

  openNew() {
    this.router.navigateByUrl('/admin/farm/animal-edit');
  }

  editAnimal(selectedAnimal: any) {
    this.router.navigate(
      ['/admin/farm/animal-edit'],
      { queryParams: { animalId: selectedAnimal.externalId } }
    );
  } 

  deleteAnimal(animal: any) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this record?',
      header: 'Question',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.animalClient.delete(animal.externalId).subscribe(result => {
          if (result.isSuccess) {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: "Deleted Successfully", life: 3000 });
            this.getBranches();
          }
          else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: "Unexpected error occurred", life: 3000 });
          }
        }
        );
      }
    });
  }
  deleteSelectedProducts() { 
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected animals?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => { 
        this.messageService.add({ severity: 'info', summary: 'Info', detail: 'Under construction. This feature will be relased next month', life: 3000 });
      }
    });
  }
}

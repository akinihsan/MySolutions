import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { AuthHelper } from 'src/app/helpers/auth.helper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  displayTOS: boolean = false;
  email: string = '';
  password: string = '';
  buttonDisabled: boolean = false;

  form: FormGroup;
  constructor(private authHelper: AuthHelper, private formBuilder: FormBuilder,
    private messageService: MessageService) {

    this.form = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required],
    });

  }
  ngOnInit(): void {

  }
  isFieldValid(field: string) {
    return !this.form!.get(field)!.valid && this.form!.get(field)!.touched;
  }
  displayFieldCss(field: string) {
    return {
      'ng-dirty ng-invalid': this.isFieldValid(field)
    };
  }
  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
  showDialog() {
    this.displayTOS = true;
  }

  login() { 
    if (this.form.valid) {
      this.buttonDisabled = true;
      this.authHelper.login(this.email, this.password).subscribe(res =>
        this.buttonDisabled = false
      );
    } else {
      this.validateAllFormFields(this.form);
      this.messageService.add({ severity: 'error', summary: 'Error', detail: "Please enter your e-mail and password" });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import {PasswordValidator} from '../password-validator';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';




@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountService: AccountService, private route: Router) { }
   signInForm: FormGroup;
   invalidRegister: boolean;
   errorMessage: string[];



  ngOnInit() {

    this.signInForm = this.fb.group({
      EmailAddress: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]

    }, {validator: PasswordValidator} );
  }

  onSubmit() {
    console.log(this.signInForm.value);
    this.accountService.Register(this.signInForm.value).subscribe(
      result => {
        this.invalidRegister = false;
        console.log('success', result);
        this.route.navigate(['/login']);

      },
      error => {
        this.invalidRegister = true;

        this.errorMessage = error.error.value;
        console.error(error);

      }


    );



  }


}

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
loginForm : FormGroup;


constructor(private accountService: AccountService, private formBuilder : FormBuilder){
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password : ['', Validators.required]
    })
}

onSubmit(){
  this.accountService.login(this.loginForm.value).subscribe({
    next: user => console.log(user)
  })
}
}

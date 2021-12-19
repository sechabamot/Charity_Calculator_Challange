import { LocalStorageService } from './../../../library/services/local-storage-service';
import { InputValidator } from './../../../library/utilities/input-validator';
import { Component, OnInit } from '@angular/core';
import { InputType } from 'src/app/library/utilities/input-validator';
import { UserService } from 'src/app/library/services/user-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  email:InputValidator = new InputValidator(InputType.Email, "");
  password:InputValidator = new InputValidator(InputType.Password, "");

  constructor(private _userService:UserService, private _localStorageService:LocalStorageService
    ,private router: Router) 
  { 

  }
  
  ngOnInit(): void {

  }

  login(){

    if(this.loginFormIsValid()){

      this._userService.login(this.email.Value, this.password.Value).subscribe(
        (response) => {                           
          console.log('Login response received')
          console.log(response)
          this._localStorageService.storeAuthenticationToken(response.body as string)
          this.router.navigate(['home']);
          if(response.status == 200){
            console.log(response.body)
            this._localStorageService.storeAuthenticationToken(response.body as string)
            this.router.navigate(['home']);
          }
        },
        (error) => {                              //error() callback
          console.log('Login response received')
          console.log(error);

          if(error.status == 401){
            alert("Wrong email or password")
          }
          if(error.status == 500){
            alert("Something went wrong and it's not your fault")
          }
          if(error.status == 400){
            alert("You sent a bad request")
          }
        },      
      );
    }else{
      alert("Form Incorrect")
    }

  }

  loginFormIsValid(){
   
    if(this.email.IsValid && this.password.IsValid){
      return true;
    }
    else
    {
      return false;
    }

  }

}

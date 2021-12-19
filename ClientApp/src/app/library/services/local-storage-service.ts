import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

    private authenticationToken:string = "x-authenticationToken";
    private userName:string = "x-userName"
  
    constructor() {
  
    }
  
    storeUserName(userName:string){
      localStorage.setItem(this.userName, this.userName)
      console.log('Username stored')
    }
  
    getUserName(){
      console.log('Getting username')
      return window.localStorage.getItem(this.userName)
    }

    forgetAuthenticatedUser(){
      window.localStorage.clear();
      console.log("storage cleared")
    }

    getAuthenticationToken(){
      console.log('Getting authentication token...')
      return window.localStorage.getItem(this.authenticationToken) ?? "";   
    }

    storeAuthenticationToken(token:string){
      window.localStorage.setItem(this.authenticationToken, token)
      console.log('Authentication token stored')
    }   
}

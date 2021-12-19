import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UserService } from "./user-service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService {
    
  constructor(private auth: UserService, private router: Router) { }
  
  async canActivate(){
   
   const authenticated = await this.auth.IsAuthenticated();
   console.log(authenticated);

   if (!authenticated) {
     
     this.router.navigate(['login']);
     return false;

   }

   return true;
 }

}

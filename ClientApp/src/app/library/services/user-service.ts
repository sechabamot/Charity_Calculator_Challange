import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    
    readonly baseUrl:string = "https://localhost:44315/user";

    constructor(private _httpClient:HttpClient) 
    {
       
    }

    IsAuthenticated(){    
        
        return this._httpClient.get(`${this.baseUrl}/authenticated`, { observe: 'response' });
    }

    login(email:string, password:string){
        
        return this._httpClient.post(`${this.baseUrl}/login`,{
            email:email,
            password:password
        },{ observe: 'response', responseType: 'text'});
        
    }

}

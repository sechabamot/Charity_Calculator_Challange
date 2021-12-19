import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    readonly baseUrl:string = "https://localhost:10862/api";
    constructor(private _httpClient:HttpClient) 
    {
       
    }
    
    IsAuthenticated(){    
        return this._httpClient.get(`${this.baseUrl}/Authenticated`);
    }
}

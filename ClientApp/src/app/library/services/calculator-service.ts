import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class CalculatorService {
    
    readonly baseUrl:string = "https://localhost:44315/calculator/returns";

    constructor(private _httpClient:HttpClient) 
    {
       
    }

    calculateDeductableReturns(){
        // return this._httpClient.get(`${this.baseUrl}?`)
    }
}

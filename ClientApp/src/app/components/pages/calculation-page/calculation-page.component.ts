import { InputType } from 'src/app/library/utilities/input-validator';
import { InputValidator } from './../../../library/utilities/input-validator';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calculation-page',
  templateUrl: './calculation-page.component.html',
  styleUrls: ['./calculation-page.component.css']
})
export class CalculationPageComponent implements OnInit {

  donation:InputValidator = new InputValidator(InputType.Currency, 0);
  deductable:number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  donationChanged(value:number){
    // alert(value);
  }
}

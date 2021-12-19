import { UserService } from 'src/app/library/services/user-service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  role: "admin" | "donor" | "sponser" | "none" = "none";

  constructor(private _userService: UserService) {

   }

  ngOnInit(): void {
    
  }

  getRole(){
    this.role = 'donor';
  }

}

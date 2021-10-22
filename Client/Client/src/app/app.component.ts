import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Client';
  data:any;
  /**
   *
   */
  constructor(private http:HttpClient,private accountService:AccountService) {

  }
  ngOnInit(){
    this.http.get("https://localhost:5001/api/Users").subscribe(
      resp=>this.data=resp
    );
    this.setCurrentUser();
  }
  setCurrentUser(){
    const user:User=JSON.parse(localStorage.getItem("user"));
    this.accountService.setCurrentUser(user);
  }
}

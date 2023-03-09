import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  constructor(private accountService: AccountService) {
    //constructor is considered too early to fetch data from an API
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    // const user: User = JSON.parse(localStorage.getItem('user')!);
    //^the exclamation mark here essentially turns off type safte and says.. "I know there is an error, but I know best, just do it"... use carefully
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}

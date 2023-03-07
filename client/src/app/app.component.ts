import { HttpClient } from '@angular/common/http';
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
  users: any;

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {
    //constructor is considered too early to fetch data from an API
  }

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (data) => (this.users = data),
      error: (error) => console.log(error),
      complete: () => console.log('Request completed! :)'),
    });
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

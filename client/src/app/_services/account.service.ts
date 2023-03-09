import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //responsible for making http requests from client to server
  //because this is provided in the root, the service is created when the app is initialized and is not destroyed until the application is done/closed
  //^this is different from components which are created and destroyed dynamically as the user navagates through the app

  baseUrl = 'https://localhost:5001/api/';

  //becasue many parts of the app will need access to the "logged-in" property that is stored in the nav right now, I am going to move that here so the whole app will have access
  //I am going to use a special type of observable to store it: a behavior subject
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  //^the dollar is a convention to show that this is an observable

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      //this will happen before the info gets to the subscription
      //I am going to use it here to save the object to borwser storage so I can persist login data accross browser refreshes and even closes
      map((response: User) => {
        const user = response;
        if (user) {
          //when I am setting here, I need to turn the JSON response to a string bc that is what the browser can store
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}

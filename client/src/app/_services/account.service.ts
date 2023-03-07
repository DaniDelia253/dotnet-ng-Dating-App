import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //responsible for making http requests from client to server
  //because this is provided in the root, the service is created when the app is initialized and is not destroyed until the application is done/closed
  //^this is different from components which are created and destroyed dynamically as the user navagates through the app

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}

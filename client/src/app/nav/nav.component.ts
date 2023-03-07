import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  // currentUser$: Observable<User | null> = of(null);
  //the dollar represents that this property is an observable
  //the type of the property is an observable of type user
  //when ng is in strict mode, this will error bc it has no initialized value...
  //...to get around this, I need to assign a value...at this stage I don;t know if there is a user so I can use the union type (<User | null>) to specify that it can be either one
  //BUT i still need to assign it to something, and I can't just say "= null" becaseu this is not going to satisfy the fact that this is an observable
  //for this reason, I use the RxJs oppeprator "of" and this tells that is is an observable OF something (in this case, initialized to an observable OF null)
  //ULTIMATELY, I ended up not using this bc I just made the accountService public and accessed it directlyf rom the template
  constructor(public acountService: AccountService) {}

  ngOnInit(): void {
    // this.currentUser$ = this.acountService.currentUser$;
  }

  login() {
    this.acountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => console.log(error),
    });
  }

  logout() {
    this.acountService.logout();
  }
}

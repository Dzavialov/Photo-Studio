import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn: boolean = false;
  private isLoggedInSubscription: Subscription = Subscription.EMPTY;
  isUserAdmin: boolean = false;


  /**
   *
   */
    constructor(private authService: AuthService) {

    }
  ngOnInit(): void {
    this.isLoggedInSubscription = this.authService.isLoggedIn$.subscribe(
      (isLoggedIn) => {
        this.isLoggedIn = isLoggedIn;
      }
    );

    this.isUserAdmin = this.authService.isAdmin();
    console.log('is user admin? ' + this.isUserAdmin);
  }

  public getAuthService(): AuthService{
    return this.authService;
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onLogoutClick(): void{
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.isLoggedInSubscription.unsubscribe();
  }
}

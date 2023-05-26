import { Component, Inject } from '@angular/core';
import { LoginDto } from '../models/auth.dto';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginDto: LoginDto = { username: '', password: '' };
  errorMessage: string = '';
  isUserAdmin = false;

  constructor(@Inject(AuthService) private authService: AuthService, private router: Router) { }

  login(): void {
    this.authService.login(this.loginDto.username, this.loginDto.password).subscribe({
      next: response => {
        localStorage.setItem('token', response.token); // save token to local storage
        if (this.authService.isAdmin()) {
          console.log('isAdmin if statement');
          this.router.navigateByUrl('/admin-panel');
          this.isUserAdmin = true;
        }
        else{
          console.log('not admin');
          this.router.navigateByUrl('/counter'); // navigate to dashboard page on success
        }
        this.authService.isLoggedInSubject.next(true);
      },
      error: err => {
        this.errorMessage = 'Invalid username or password.'; // set error message on failure
      }
    });
  }
}

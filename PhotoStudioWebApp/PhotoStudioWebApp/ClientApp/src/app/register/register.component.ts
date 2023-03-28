import { Component, Inject } from '@angular/core';
import { LoginDto, RegisterDto } from '../auth/auth.dto';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerDto: RegisterDto = { username: '', email: '', password: '' };
  passwordError = '';
  emailError = '';
  validationResult = true;
  usernameError = '';

  constructor(@Inject(AuthService) private authService: AuthService, private router: Router) { }

  register(): void {
    this.usernameError = '';
    this.validationResult = true;
    if (!this.validatePassword(this.registerDto.password)){
      this.passwordError = 'Passwords must have at least one non alphanumeric character, one uppercase letter, and one digit.';
      console.log('ERROR');
      this.validationResult = false;
    }

    if (!this.validateEmail(this.registerDto.email)) {
      this.emailError = 'Invalid email address format.';
      console.log('ERROR');
      this.validationResult = false;
    }
    console.log('NO ERROR');

    if(this.validationResult === true){
      this.authService.register(this.registerDto).subscribe({
        next: response => {
          console.log(response); // handle response from backend
          this.router.navigateByUrl('/login');
        },
        error: err => {
          console.log(err); // handle error from backend
          if (err.status === 500) {
            this.usernameError = 'Username already exists.';
          }
        }
      });
    }
  }

  validatePassword(password: string): boolean{
    this.passwordError = '';
    const regex = /^(?=.*[^a-zA-Z0-9])(?=.*[A-Z])(?=.*[0-9]).+$/;
    return regex.test(password);
  }

  validateEmail(email: string): boolean {
    this.emailError = '';
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterDto } from '../models/auth.dto';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7026/Authenticate';
  private jwtHelper = new JwtHelperService();
  public isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const loginDto = { username: username, password: password };
    return this.http.post(`${this.baseUrl}/login`, loginDto);
  }

  register(registerDto: RegisterDto): Observable<any> {
    console.log(registerDto);
    return this.http.post<any>(`${this.baseUrl}/register`, registerDto);
  }

  isAdmin(): boolean {
    console.log('isAdmin is called');
    const token = localStorage.getItem('token');
    if (!token) {
      return false;
    }
    const decodedToken = this.jwtHelper.decodeToken(token);
    console.log(decodedToken);
    console.log(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
    return decodedToken && decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin';
  }

  checkLoginStatus() {
    const token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.isLoggedInSubject.next(true);
    } else {
      this.isLoggedInSubject.next(false);
    }
  }

  logout(){
    localStorage.removeItem('token');
    this.isLoggedInSubject.next(false);
  }

  getCurrentUserId(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      console.log('token = ', decodedToken);
      return decodedToken.sub;
    }
    return null;
  }
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AuthService } from './auth/auth.service';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { RoomManagerComponent } from './admin-panel/room-manager/room-manager.component';
import { AdminGuard } from './admin.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegisterComponent,
    LoginComponent,
    AdminPanelComponent,
    RoomManagerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'admin-panel', component: AdminPanelComponent, canActivate: [AdminGuard]},
      { path: 'admin-panel/room-manager', component: RoomManagerComponent, canActivate: [AdminGuard] },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent },
    ])
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }

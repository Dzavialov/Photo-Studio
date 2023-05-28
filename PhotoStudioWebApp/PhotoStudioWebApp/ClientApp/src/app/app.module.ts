import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthService } from './auth/auth.service';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { RoomComponent } from './admin-panel/room/room.component';
import { AdminGuard } from './admin.guard';
import { ApiserviceService } from './apiservice.service';
import { RoomCreateComponent } from './admin-panel/room/room-create/room-create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RoomEditComponent } from './admin-panel/room/room-edit/room-edit.component';
import { RoomsComponent } from './rooms/rooms.component';
import { GetRoomComponent } from './rooms/get-room/get-room.component';
import { BookingComponent } from './booking/booking.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    AdminPanelComponent,
    RoomComponent,
    RoomCreateComponent,
    RoomEditComponent,
    RoomsComponent,
    GetRoomComponent,
    BookingComponent
],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'admin-panel', component: AdminPanelComponent, canActivate: [AdminGuard]},
      { path: 'admin-panel/room', component: RoomComponent, canActivate: [AdminGuard] },
      { path: 'admin-panel/room/room-create', component: RoomCreateComponent, canActivate: [AdminGuard] },
      { path: 'admin-panel/room/room-edit/:id', component: RoomEditComponent, canActivate: [AdminGuard] },
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent },
      { path: 'rooms', component: RoomsComponent},
      { path: 'rooms/get-room/:id', component: GetRoomComponent},
      { path: 'booking/:id', component: BookingComponent}
    ])
  ],
  providers: [AuthService, ApiserviceService],
  bootstrap: [AppComponent]
})
export class AppModule { }

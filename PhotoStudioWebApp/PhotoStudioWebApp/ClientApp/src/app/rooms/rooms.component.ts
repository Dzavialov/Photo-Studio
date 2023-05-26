import { Component, OnInit } from '@angular/core';
import { RoomDto } from '../models/room.dto';
import { Observable } from 'rxjs';
import { ApiserviceService } from '../apiservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit{

  rooms$!: Observable<RoomDto[]>;
  constructor(private roomService: ApiserviceService, private router: Router){}


  ngOnInit(): void {
    this.rooms$ = this.roomService.getAllRooms();
  }

  navigateToRoom(id: string) {
    this.router.navigate(['rooms/get-room', id]);
  }
}

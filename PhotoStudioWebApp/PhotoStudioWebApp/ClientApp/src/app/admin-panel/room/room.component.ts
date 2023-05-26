import { Component, Inject } from '@angular/core';
import { RoomDto } from '../../models/room.dto';
import { ApiserviceService } from 'src/app/apiservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent {

  rooms: RoomDto[] = [];
  images: File[] = [];

  constructor(private apiService: ApiserviceService, private router: Router) {}

  ngOnInit(): void {
    this.getRooms();
  }

  newRoomBtn(){
    this.router.navigate(['admin-panel/room/room-create']);
  }

  getRooms() {
    this.apiService.getAllRooms().subscribe(
      (response: RoomDto[]) => {
        this.rooms = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  // Example method to delete a room
  onDelete(room: RoomDto) {
    this.apiService.deleteRoom(room.id).subscribe(
      response => {
        console.log(response); // Do something with the response
        console.log('DELETING ROOM');
        this.rooms = this.rooms.filter(r => r.id !== room.id);
      },
      error => {
        console.log(error); // Handle the error
      }
    );
  }

  onEdit(room: RoomDto) {
    console.log('Passing room to edit!!!');
    console.log(room);
    this.router.navigate(['admin-panel/room/room-edit', room.id]);
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiserviceService } from 'src/app/apiservice.service';
import { RoomDto } from 'src/app/models/room.dto';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-get-room',
  templateUrl: './get-room.component.html',
  styleUrls: ['./get-room.component.css'],
})
export class GetRoomComponent implements OnInit{

  room!: RoomDto;
  imagePath!: SafeUrl[];
  modifiedPath!: string;
  isLoggedIn!: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private apiService: ApiserviceService, private sanitizer: DomSanitizer,
    private authService: AuthService) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id')!;
    console.log("ID: ", this.activatedRoute.snapshot.params.id);
    if(id){
      this.apiService.getRoom(id).subscribe((room: RoomDto) => {
        this.room = room;
        console.log(this.room);
        if (room.images && room.images.length > 0) {
          this.imagePath = room.images
          .filter(image => image.roomId === room.id)
          .map(image => {
            this.modifiedPath = image.imagePath.replace(/^.*(\/|\\)assets/, '../../../assets/');
            console.log(this.modifiedPath)
            return this.sanitizer.bypassSecurityTrustUrl(this.modifiedPath);
          });
          console.log(this.imagePath);
        }
      });
    }
  }

  onButtonClicked() {
    if (this.authService.isLoggedInSubject.value) {
      this.router.navigate(['/booking', this.room.id]);
    } else {
      this.router.navigate(['/login']);
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoomDto } from '../../../models/room.dto';
import { ApiserviceService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-room-create',
  templateUrl: './room-create.component.html',
  styleUrls: ['./room-create.component.css']
})
export class RoomCreateComponent implements OnInit {
  roomForm!: FormGroup;
  selectedFiles: File[] = [];
  validationResult: boolean = true;
  nameError = '';
  descError = '';
  addInfoError = '';

  constructor(private formBuilder: FormBuilder, private apiService: ApiserviceService, private router: Router){}

   ngOnInit(): void {
    this.roomForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      additionalInformation: [''],
      images: ['']
    });
  }

  onSubmit() {
    if (this.roomForm.invalid) {
      return;
    }

    const room: RoomDto = {
      id: '',
      name: this.roomForm.controls.name.value,
      description: this.roomForm.controls.description.value,
      additionalInformation: this.roomForm.controls.additionalInformation.value,
      booking: []
    };

    this.validateRoom(room);
    console.log(room.additionalInformation.length);

    if(this.validationResult === true){
    this.apiService.addRoom(room, this.selectedFiles).subscribe(
      response => {
        console.log(response); // Do something with the response
        console.log('CREATING ROOM');
        this.router.navigate(['admin-panel/room']);
      },
      error => {
        console.log(error); // Handle the error
      }
    );
    this.selectedFiles = [];
  }
  }

  onFileChange(event: any) {
    console.log('onFileChange is called');
    const files = event.target.files;
    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files[i]);
    }
    console.log(this.selectedFiles);
  }

  validateRoom(room: RoomDto): void {
    this.nameError = '';
    this.descError = '';
    this.addInfoError = '';
    this.validationResult = true;
    const nameRegex = /^.{1,50}$/;
    const descriptionRegex = /^.{10,5000}$/;
    const additionalInfoRegex = /^.{5,1000}$/;

    if (!nameRegex.test(room.name)) {
      this.validationResult = false;
      this.nameError = 'Name length should be greater than 1 and less than 50';
    }

    if (!descriptionRegex.test(room.description)) {
      this.validationResult = false;
      this.descError = 'Description length should be greater than 10 and less than 5000';
    }

    if (!additionalInfoRegex.test(room.additionalInformation)) {
      this.validationResult = false;
      this.addInfoError = 'Additional Info length should be greater than 5 and less than 1000';
    }
  }
}

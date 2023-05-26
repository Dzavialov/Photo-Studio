import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomDto } from '../../../models/room.dto';
import { ApiserviceService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-room-edit',
  templateUrl: './room-edit.component.html',
  styleUrls: ['./room-edit.component.css']
})
export class RoomEditComponent implements OnInit {
  roomForm!: FormGroup;
  selectedFiles: File[] = [];
  roomToEdit!: RoomDto;
  validationResult: boolean = true;
  nameError = '';
  descError = '';
  addInfoError = '';

  constructor(private formBuilder: FormBuilder, private apiService: ApiserviceService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.roomForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      additionalInformation: [''],
      images: ['']
    });
}

ngOnInit(): void {
  const id = this.activatedRoute.snapshot.paramMap.get('id')!;
  console.log("ID: ", this.activatedRoute.snapshot.params.id);
  this.apiService.getRoom(id).subscribe(room => {
    this.roomToEdit = room;
    this.roomForm = this.formBuilder.group({
      name: [this.roomToEdit.name, Validators.required],
      description: [this.roomToEdit.description, Validators.required],
      additionalInformation: [this.roomToEdit.additionalInformation],
      images: [this.roomToEdit.images]
    });
  });
}

  onSubmit() {
    if (this.roomForm.invalid) {
      return;
    }

    const updatedRoom: RoomDto = {
      id: this.roomToEdit.id,
      name: this.roomForm.value.name,
      description: this.roomForm.value.description,
      additionalInformation: this.roomForm.value.additionalInformation,
      booking: this.roomToEdit.booking // You can leave the booking array as is since it is not being updated
    };

    const formData = new FormData();
    formData.append('id', updatedRoom.id);
    formData.append('name', updatedRoom.name);
    formData.append('description', updatedRoom.description);
    formData.append('additionalInformation', updatedRoom.additionalInformation);

    for (let i = 0; i < this.selectedFiles.length; i++) {
      formData.append('images', this.selectedFiles[i]);
    }

    this.validateRoom(updatedRoom);

    if(this.validationResult === true){
    this.apiService.updateRoom(updatedRoom, formData).subscribe(
      response => {
        console.log(response); // Do something with the response
        console.log('UPDATING ROOM');
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
    const additionalInfoRegex = /^.{10,1000}$/;

    if (!nameRegex.test(room.name)) {
      this.validationResult = false;
      this.nameError = 'Name length should be greater than 1 and less than 50';
    }

    if (!descriptionRegex.test(room.description)) {
      this.validationResult = false;
      this.descError = 'Description should be greater than 10 and less than 5000';
    }

    if (!additionalInfoRegex.test(room.additionalInformation)) {
      this.validationResult = false;
      this.addInfoError = 'Additional Info should be greater than 10 and less than 1000';
    }
  }
}

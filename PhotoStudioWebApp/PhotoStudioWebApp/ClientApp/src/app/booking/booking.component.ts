import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiserviceService } from 'src/app/apiservice.service';
import { Router, ActivatedRoute } from '@angular/router';
import { BookingDto, RoomDto } from 'src/app/models/room.dto';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {

  bookingForm!: FormGroup;
  room!: RoomDto;
  bookingDto!: BookingDto;
  bookings!: BookingDto[];
  validationResult = true;
  bookFromError = '';
  bookToError = '';
  overlapError = '';

  constructor(private formBuilder: FormBuilder, private apiService: ApiserviceService, private router: Router,
    private activatedRoute: ActivatedRoute, private authService: AuthService){}

  ngOnInit(): void {
    this.bookingForm = this.formBuilder.group({
      startDate: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
    });
    console.log('onInit works');
    this.getBookings();
  }

  onSubmit() {
    console.log('onSubmit works');


    const logginedUserId = this.authService.getCurrentUserId() ?? '';
    console.log('booking user id is ', logginedUserId);
    const startDate = this.bookingForm.get('startDate')!.value;
    const startTime = this.bookingForm.get('startTime')!.value;
    const endTime = this.bookingForm.get('endTime')!.value;
    console.log('startDate is ', startDate);
    console.log('startTime is ', startTime);
    console.log('endTime is ', endTime);

    const startDateTime = new Date(`${startDate} ${startTime}`);
    const endDateTime = new Date(`${startDate} ${endTime}`);

    const roomId = this.activatedRoute.snapshot.paramMap.get('id')!;
    console.log('id is ', roomId);
      this.apiService.getRoom(roomId).subscribe((room: RoomDto) => {
        this.room = room;
        console.log('this room is ', this.room);
        console.log('BOOKED ROOM ID IS:', this.room.id);

        console.log('before creating booking');
        this.bookingDto = {
          id: '',
          bookFrom: startDateTime, // Convert to ISO string format
          bookTo: endDateTime, // Convert to ISO string format
          roomId: this.room.id, // Assign the ID of the booked room
          userId: logginedUserId // Set the user ID if applicable
        };

        console.log('booking dto bookFrom = ', this.bookingDto.bookFrom);
        console.log('BEFORE VALIDATION');
        this.validateBooking(this.bookingDto);
        console.log('validation result = ', this.validationResult);

        console.log('this booking is ', this.bookingDto);
        console.log('before addBooking in component');

        if(this.validationResult === true){



        this.apiService.addBooking(this.bookingDto).subscribe(
          response => {
            console.log(response); // Do something with the response
            console.log('INSIDE ADDBOOKING IN COMPONENT');
            this.router.navigate(['home']);
          },
          error => {
            console.log(error); // Handle the error
          }
        );}
      });
  }

  validateBooking(booking: BookingDto): boolean {
    this.validationResult = true;
    this.bookFromError = '';
    this.bookToError = '';
    this.overlapError = '';
    console.log('inside validation.')
    console.log('bookFrom = ', booking.bookFrom);
    if (!booking.bookFrom || isNaN(booking.bookFrom.getTime())) {
      this.bookFromError = 'Date is required.';
      this.validationResult = false;
    } else if (new Date(booking.bookFrom) < new Date()) {
      this.bookFromError = 'Date should be greater than or equal to today.';
      this.validationResult = false;
    }

    if (!booking.bookTo || isNaN(booking.bookTo.getTime())) {
      this.bookToError = 'Date is required.';
      this.validationResult = false;
    } else if (new Date(booking.bookTo) < new Date(booking.bookFrom)) {
      this.bookToError = 'Ending time should be greater than start time.';
      this.validationResult = false;
    } else if (new Date(booking.bookTo) > new Date(booking.bookFrom.getTime() + (5 * 60 * 60 * 1000))) {
      this.bookToError = 'Ending time should be within 5 hours of the start time.';
      this.validationResult = false;
    }

    console.log('All bookings: ', this.bookings);
    const overlaps = this.bookings.some(existingBooking => {


      const start = new Date(booking.bookFrom);
      const end = new Date(booking.bookTo);
      const startUTC = new Date(start.getTime() + start.getTimezoneOffset() * 60000);
      const endUTC = new Date(end.getTime() + end.getTimezoneOffset() * 60000);

      return (
        this.checkForOverlap(
          existingBooking.bookFrom,
          existingBooking.bookTo,
          startUTC,
          endUTC
        ) &&
        existingBooking.roomId === booking.roomId
      );});

      if (overlaps) {
        console.log('Overlap detected!');
        this.overlapError = '';
        this.validationResult = false;
      } else {
        console.log('No overlap.');
      }

      return this.validationResult;
}


  getBookings() {
    this.apiService.getAllBookings().subscribe((bookings: BookingDto[]) => {
      this.bookings = bookings;
      console.log('All bookings: ', this.bookings);
    });
  }

  checkForOverlap(start1: Date, end1: Date, start2: Date, end2: Date): boolean {
    return start1 < end2 && end1 > start2;
  }
}

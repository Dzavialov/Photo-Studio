import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RoomDto, BookingDto } from './models/room.dto';

@Injectable({
  providedIn: 'root'
})

export class ApiserviceService {
  readonly roomUrl = 'https://localhost:7026/Room';
  readonly bookingUrl = 'https://localhost:7026/Booking';

  constructor(private http: HttpClient) {}

  getAllRooms() : Observable<any[]> {
    return this.http.get<any[]>(this.roomUrl + '/get-rooms');
  }

  getRoom(id: string): Observable<RoomDto> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<RoomDto>(`${this.roomUrl}/${id}`, { headers: headers });
  }

  addRoom(room: RoomDto, images: File[]): Observable<any> {
    const formData = new FormData();
    formData.append('name', room.name);
    formData.append('description', room.description);
    formData.append('additionalInformation', room.additionalInformation);
    console.log('ADDED INFO');
    console.log('Number of images:', images.length);

    for (let i = 0; i < images.length; i++) {
      formData.append('images', images[i]);
      console.log('IMAGE IS ADDED');
    }

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });

    return this.http.post<any>(this.roomUrl + '/create-room', formData, { headers: headers });
  }

updateRoom(room: RoomDto, formData: FormData): Observable<any> {
  const headers = new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  });

  return this.http.put<any>(`${this.roomUrl}/edit-room/${room.id}`, formData, { headers: headers });
}


  deleteRoom(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });

    const deleteUrl = `${this.roomUrl}/${id}`;
    return this.http.delete<any>(deleteUrl, { headers: headers });
  }

  addBooking(booking: BookingDto): Observable<any> {
    console.log('inside addbooking in service 1');
    const formData = new FormData();
    formData.append('bookFrom', booking.bookFrom.toISOString());
    formData.append('bookTo', booking.bookTo.toISOString());
    formData.append('roomId', booking.roomId);
    formData.append('userId', booking.userId);
    console.log('inside addbooking in service 2');

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<any>(this.bookingUrl + '/create-booking', formData, { headers: headers });
  }

  getAllBookings(): Observable<BookingDto[]>{
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });

    return this.http.get<any[]>(this.bookingUrl + '/get-bookings', { headers: headers});
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{

/**
 *
 */
constructor(private router: Router) {
}

  onButtonClicked() {
      this.router.navigate(['rooms/get-room', "B9676650-B24F-4FF6-ABC5-08DB45721218"]);
  }
}

import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: false,
  styleUrl: './home.css',
  templateUrl: './home.html',
})
export class HomeComponent {
  selectRole(role: string) {
    fetch('/api/viewers/role', {
      method: 'POST',
      headers:
      {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        visitorType: role
      })
    });
  }
}

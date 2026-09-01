import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: false,
  styleUrl: './home.css',
  templateUrl: './home.html',
})
export class HomeComponent {
  showPopup = true;

  ngOnInit() {
    const roleSelected = sessionStorage.getItem('visitorRole');
    if (roleSelected) {
      this.showPopup = false;
    }
  }


  selectRole(role: string)
  {
    sessionStorage.setItem('visitorRole', role);
    const timezone=Intl.DateTimeFormat().resolvedOptions().timeZone;

    fetch('/api/viewers/role', {
      method: 'POST',
      headers:
      {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        visitorType: role,
        timeZone: timezone
      })
    });
    this.showPopup = false;
  }
}

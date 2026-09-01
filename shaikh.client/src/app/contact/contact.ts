import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.html',
  styleUrl: './contact.css',
  standalone: false
})
export class ContactComponent {
  public phoneButtonText: string = 'Contact: (470) 886-6957';
  public readonly phoneNumber: string = '4708866957';

  handlePhoneClick(): void {
    navigator.clipboard.writeText(this.phoneNumber).then(() => {
      const originalText = this.phoneButtonText;
      this.phoneButtonText = 'Number Copied!';

      setTimeout(() => {
        this.phoneButtonText = originalText;
      }, 2000);
    }).catch(err => {
      console.error('Failed to copy text: ', err);
    });

    window.location.href = `tel:${this.phoneNumber}`;
  }
}

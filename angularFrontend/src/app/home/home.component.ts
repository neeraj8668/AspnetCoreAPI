import { Component } from '@angular/core';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    showCookieConsent = true;
    ngOnInit(): void{ 

    }
    hideCookieConsent(): void {
      this.showCookieConsent = false;
    }
}
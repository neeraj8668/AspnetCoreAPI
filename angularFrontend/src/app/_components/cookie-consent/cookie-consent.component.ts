import { Component, Input, OnInit,Output, EventEmitter } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cookie-consent',
  templateUrl: './cookie-consent.component.html',
  styleUrls: ['./cookie-consent.component.css']
})
export class CokieConsentComponent implements OnInit {
  @Input() showCookieConsent: boolean=false;
  @Output() consentGiven = new EventEmitter<void>();

  constructor(private cookieService: CookieService) {}
 
  ngOnInit(): void {
    const cookieValue = this.cookieService.get('cookieConsent');
    this.showCookieConsent = cookieValue ? cookieValue === 'true' : this.showCookieConsent;
  }
  giveConsent(): void {
    // Set a cookie or store user consent preference
    this.cookieService.set('cookieConsent', 'false');
    this.hideCookieConsent();
    this.consentGiven.emit();
  }
  hideCookieConsent(): void {
    this.showCookieConsent = false;
  }
  
  denyConsent(): void {
    this.cookieService.set('cookieConsent', 'true');
    // Handle denial logic (e.g., restrict certain features)
    // Hide the cookie consent component
  }
}

import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  constructor(private authService: MsalService) { }

  login() {
    this.authService.loginRedirect();
  }

  logout() {
    this.authService.logoutRedirect();
  }

}

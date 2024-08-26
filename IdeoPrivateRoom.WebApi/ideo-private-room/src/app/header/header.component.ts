import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MsalService} from '@azure/msal-angular';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isAuthorized = false;

  constructor(
    private authService: MsalService,
  )   {  
    this.isAuthorized = this.authService.instance.getAllAccounts().length > 0;
  }

  logout() {
    this.authService.instance.logoutRedirect();
  }

  login() {
    this.authService.instance.loginRedirect();
  }

}

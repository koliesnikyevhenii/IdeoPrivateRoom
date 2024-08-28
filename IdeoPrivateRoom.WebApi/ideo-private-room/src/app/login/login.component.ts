import { Component , Inject} from '@angular/core';
import {   MsalService,
  MsalBroadcastService,
  MSAL_GUARD_CONFIG,
  MsalGuardConfiguration, } from '@azure/msal-angular';
import {
  RedirectRequest,
  PublicClientApplication 
} from '@azure/msal-browser';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent  {

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private authService: MsalService,
    private msalBroadcastService: MsalBroadcastService
  ) {

    this.authService.instance.initialize().then(() => {
      console.log('MsalService is initialized.');
    }).catch(error => {
      console.error('MsalService failed to initialize:', error);
    });
  }

  clearInteractionStatus() {
    console.log('MsalService clearInteractionStatus.');
    const msalService = this.authService.instance;
    msalService.getLogger().verbose("Clearing interaction status.");
    msalService.clearCache();
    
    localStorage.removeItem("msal.interaction.status");
    sessionStorage.removeItem("msal.interaction.status");

  }


  login() {
    this.clearInteractionStatus();
    if (this.msalGuardConfig.authRequest) {
      this.authService.loginRedirect({
        ...this.msalGuardConfig.authRequest,
      } as RedirectRequest);
    } else {
      this.authService.loginRedirect();
    }
  }
}

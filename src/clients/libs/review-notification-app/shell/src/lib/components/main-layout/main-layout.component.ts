import { ApiService } from '@drreview/shared/data-access';
import { Component } from '@angular/core';

@Component({
  selector: 'drreview-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent {

  public randomNumber = 0;


  public constructor(private apiService: ApiService) {
    this.apiService.get("migrations/random").subscribe({
      next: res => {
        console.log(res);
      },
      error: error => {
        console.error("error", error);
      },
      complete: () => {

      }
    });
  }
}

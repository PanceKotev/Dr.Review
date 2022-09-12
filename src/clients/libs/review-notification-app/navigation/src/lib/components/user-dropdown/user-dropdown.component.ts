import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ThemesService } from '@drreview/shared/services/themes';

@Component({
  selector: 'drreview-user-dropdown',
  templateUrl: './user-dropdown.component.html',
  styleUrls: ['./user-dropdown.component.scss']
})
export class UserDropdownComponent {
  @Input()
  public username = 'Example Username';

  @Output()
  public logoutClicked = new EventEmitter<boolean>();

  public constructor(public themeService: ThemesService){

  }
}

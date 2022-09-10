import { Component, Input, Output, EventEmitter } from '@angular/core';

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

}

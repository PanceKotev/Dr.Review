import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'drreview-side-navigation',
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss']
})
export class SideNavigationComponent {

  @Output()
  public sideNavClicked = new EventEmitter<void>();

  public toggleSidebar(): void {
    this.sideNavClicked.emit();
  }
}

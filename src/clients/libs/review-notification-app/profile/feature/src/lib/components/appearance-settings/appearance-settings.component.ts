import { Component } from '@angular/core';
import { ThemesService } from '@drreview/shared/services/themes';

@Component({
  selector: 'drreview-appearance-settings',
  templateUrl: './appearance-settings.component.html',
  styleUrls: ['./appearance-settings.component.scss']
})
export class AppearanceSettingsComponent {

  public constructor(public themeService: ThemesService){
  }
}

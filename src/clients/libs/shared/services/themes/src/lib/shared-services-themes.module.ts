import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemesService } from './services/themes.service';

@NgModule({
  imports: [CommonModule],
  providers: [ThemesService]
})
export class SharedServicesThemesModule {}

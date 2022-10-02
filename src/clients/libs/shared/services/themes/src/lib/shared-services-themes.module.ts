import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemesService } from './services/themes.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [CommonModule, HttpClientModule],
  providers: [ThemesService]
})
export class SharedServicesThemesModule {}

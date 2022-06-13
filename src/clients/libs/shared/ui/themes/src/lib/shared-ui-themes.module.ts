import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeToggleComponent } from './components/theme-toggle/theme-toggle.component';

@NgModule({
  imports: [CommonModule],
  declarations: [ThemeToggleComponent],
  exports: [ThemeToggleComponent]
})
export class SharedUiThemesModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from './base/api.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule],
  providers: [
    ApiService
  ]
})
export class SharedDataAccessModule {}

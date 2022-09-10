import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarComponent } from './components/avatar/avatar.component';
import { AvatarModule } from 'ngx-avatars';

@NgModule({
  imports: [
    CommonModule,
    AvatarModule],
  declarations: [AvatarComponent],
  exports: [AvatarComponent]
})
export class SharedUiAvatarModule {}

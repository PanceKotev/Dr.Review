import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'drreview-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AvatarComponent{
  @Input()
  public isRound = false;

  @Input()
  public text!: string;

  @Input()
  public size = 40;

}

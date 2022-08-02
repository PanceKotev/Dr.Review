import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { RatingChangeEvent } from 'angular-star-rating';

@Component({
  selector: 'drreview-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RatingComponent {

  @Input()
  public value = 0.0;

  @Input()
  public readOnly = false;

  @Input()
  public useAppColors = false;

  @Output()
  public ratingChanged = new EventEmitter<number>();

  public ratingChange(value: RatingChangeEvent): void {
    this.value = value.rating;
    this.ratingChanged.emit(this.value);
  }
}

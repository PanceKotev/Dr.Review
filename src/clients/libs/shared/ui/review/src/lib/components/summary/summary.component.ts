import { KeyValue } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'drreview-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SummaryComponent {

  @Input()
  public summaryScore = 0;

  public get totalReviews(): number {

    return Object.values(this.starByCount).reduce((partialSum, a) => partialSum + a, 0);
  }

  @Input()
  public starByCount : {[key: number] : number} = {
    1 : 0,
    2 : 0,
    3: 0,
    4: 0,
    5: 0};


    public sortNumbers = (a: KeyValue<string, number>, b: KeyValue<string, number>) : number => {
      return a.key > b.key ? -1 : 1;
    };
}

import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { ChangeDetectionStrategy, Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { ThemesService } from '@drreview/shared/services/themes';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';
import { map, Observable, shareReplay } from 'rxjs';

@Component({
  selector: 'drreview-slider-popover',
  templateUrl: './slider-popover.component.html',
  styleUrls: ['./slider-popover.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SliderPopoverComponent), multi: true}]
})
export class SliderPopoverComponent extends BaseControlValueAccessor<number>{
  public isHandset$: Observable<boolean>;
  public isDarkMode$: Observable<boolean>;

  public constructor(
    private breakpointObserver: BreakpointObserver,
    themeService: ThemesService){
    super();

    this.isDarkMode$ = themeService.isDarkTheme$;

    this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  }


  @Input()
  public valueClass = '';

  @Input()
  public valueSuffix = '';

  @Input()
  public min = 5;

  @Input()
  public max = 75;
}

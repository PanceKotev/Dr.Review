import { FilterBy, IOptionItemWithLink } from "@drreview/shared/data-access";

export const filterChips: IOptionItemWithLink<FilterBy>[] = [
  {
    label: 'Сите',
    link: '/subscriptions/filter/all/',
    value: FilterBy.ALL
  },
  {
    label: 'Мои',
    link: '/subscriptions/filter/mine/',
    value: FilterBy.MINE
  },
  {
    label: 'Близу мене',
    value: FilterBy.CLOSE_BY,
    link: '/subscriptions/filter/close_by/'
  },
  {
    label: 'Професии',
    value: FilterBy.SPECIALIZATION,
    link: '/subscriptions/filter/specialization/'

  },
  {
    label: 'Институции',
    value: FilterBy.INSTITUTION,
    link: '/subscriptions/filter/institution/'

  },
  {
    label: 'Локација',
    value: FilterBy.LOCATION,
    link: '/subscriptions/filter/location/'

  }
];

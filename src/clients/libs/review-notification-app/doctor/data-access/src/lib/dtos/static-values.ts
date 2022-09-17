import { FilterBy, IOptionItemWithLink } from "@drreview/shared/data-access";

export const filterOptions: IOptionItemWithLink<FilterBy>[] = [
  {
    label: 'Сите',
    value: FilterBy.ALL,
    link: '/doctors/filter/all/woo'
  },
  {
    label: 'Близу мене',
    value: FilterBy.CLOSE_BY,
    link: '/doctors/filter/close-by/woo'
  },
  {
    label: 'Професии',
    value: FilterBy.SPECIALIZATION,
    link: '/doctors/filter/specialization/woo'

  },
  {
    label: 'Институции',
    value: FilterBy.INSTITUTION,
    link: '/doctors/filter/institution/woo'

  },
  {
    label: 'Локација',
    value: FilterBy.LOCATION,
    link: '/doctors/filter/location/woo'

  }
];

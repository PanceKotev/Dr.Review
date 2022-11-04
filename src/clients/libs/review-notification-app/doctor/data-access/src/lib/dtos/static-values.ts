import { FilterBy, IOptionItemWithLink } from "@drreview/shared/data-access";

export const filterOptions: IOptionItemWithLink<FilterBy>[] = [
  {
    label: 'Сите',
    value: FilterBy.ALL,
    link: '/doctors/filter/all/'
  },
  {
    label: 'Професии',
    value: FilterBy.SPECIALIZATION,
    link: '/doctors/filter/specialization/'

  },
  {
    label: 'Институции',
    value: FilterBy.INSTITUTION,
    link: '/doctors/filter/institution/'

  },
  {
    label: 'Локација',
    value: FilterBy.LOCATION,
    link: '/doctors/filter/location/'
  }
];

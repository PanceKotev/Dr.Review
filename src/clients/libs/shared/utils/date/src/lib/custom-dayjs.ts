import * as dayjs from "dayjs";
import * as isToday  from 'dayjs/plugin/isToday';
import * as localizedFormat from 'dayjs/plugin/localizedFormat';
import 'dayjs/locale/mk';
import * as UTC from 'dayjs/plugin/utc';

dayjs.extend(isToday);
dayjs.extend(localizedFormat);
dayjs.extend(UTC);
dayjs.locale('mk');

export const drReviewDate = dayjs;

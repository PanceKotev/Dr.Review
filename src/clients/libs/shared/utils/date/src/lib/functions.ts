import { drReviewDate } from "./custom-dayjs";

export const dateToString = (range: Date | undefined | null): string | undefined  => {
  return range ? drReviewDate(range).format('YYYY-MM-DD')
  .toString() : undefined;
};


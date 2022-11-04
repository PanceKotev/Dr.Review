import { PropNonNullable } from "./non-nullable.types";

export const valueNotNull =
 <T, TKey extends keyof T>(key: TKey) => (vals: T | PropNonNullable<T, TKey>): vals is PropNonNullable<T, TKey> =>
  vals != null && vals[key] != null;

export const notNull= <T>(val: T | null): val is T => val !== null;

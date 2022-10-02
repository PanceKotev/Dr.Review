export type PropNonNullable<T, TKey extends keyof T> = T & { [P in TKey]-?: NonNullable<T[P]> };



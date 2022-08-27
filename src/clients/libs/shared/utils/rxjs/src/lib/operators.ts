/* eslint-disable no-control-regex */
import { StringDict } from "@azure/msal-common";
import { map, OperatorFunction, pipe } from "rxjs";

const regexIfNotEnglish = /[^\x00-\x7F]+/;

const mapFromEnglishToCyrillic : StringDict = {
 "a" : 'а',
 "b" : 'б',
 "v" : 'в',
 "g" : 'г',
 "d" : 'д',
 "]" : 'ѓ',
 "e" : 'е',
 "\\" : 'ж',
 "z" : 'з',
 "y" : 'ѕ',
 "i" : 'и',
 "j" : 'ј',
 "k" : 'к',
 "l" : 'л',
 "q" : 'љ',
 "m" : 'м',
 "n" : 'н',
 "w" : 'њ',
 "o" : 'о',
 "p" : 'п',
 "r" : 'р',
 "s" : 'с',
 "t" : 'т',
 "'" : 'ќ',
 "u" : 'у',
 "f" : 'ф',
 "h" : 'х',
 "c" : 'ц',
 ";" : 'ч',
 "x" : 'џ',
 "[" : 'ш'
};

export function inputToCyrillic()  : OperatorFunction<string | undefined | null, string>{
    return pipe(
      map(
      value => {
        if(value?.length  && !regexIfNotEnglish.test(value.toString())){
          const mappedValue = [];

          for(const character of value.toLowerCase()){
            const mappedChar = mapFromEnglishToCyrillic[character];

            if(mappedChar){
              mappedValue.push(mappedChar);
            } else {
              mappedValue.push(character);
            }
          }

         const finalString = mappedValue.join("");

         return finalString ?? value;
      }

      return value ?? " ";
      }));
  };

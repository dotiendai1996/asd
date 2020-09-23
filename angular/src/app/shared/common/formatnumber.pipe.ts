import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatnumber'
})
export class FormatNumber implements PipeTransform {

  transform(value: any, selectedCulture: any): any {
    if (value === 0) {
     return 0
    }
    if (!value) {
     return;
    }
    if(isNaN(Number(value))){
      return value;
    }
    var userLang = navigator.language;
     if (selectedCulture) {
      return new Intl.NumberFormat(selectedCulture).format(value);
     } else {
      return new Intl.NumberFormat(userLang).format(value);
     }
   }
}

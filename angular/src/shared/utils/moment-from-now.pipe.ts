﻿import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'momentFromNow' })
export class MomentFromNowPipe implements PipeTransform {
    transform(value: moment.MomentInput) {
        moment.locale('vi');
        if (!value) {
            return '';
        }

        return moment(value).fromNow();
    }
}
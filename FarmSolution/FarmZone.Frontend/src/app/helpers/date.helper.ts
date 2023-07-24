import { Injectable } from "@angular/core";

@Injectable()
export class DateHelper {
  convertFromUTC(date: Date): Date {
    return new Date(date.getFullYear(), date.getMonth(), date.getUTCDate(), date.getHours() + 3, date.getUTCMinutes(), 0, 0);
  }
}
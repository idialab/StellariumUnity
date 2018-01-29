using System;
using UnityEngine;

public class CustomDateTime {

    public enum Era {
        BC = 0,
        AD = 1
    }

    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;
    public Era era;

    public CustomDateTime(double jday) {
        CustomDateTime temp = FromJulianDay(jday);
        year = temp.year;
        month = temp.month;
        day = temp.day;
        hour = temp.hour;
        minute = temp.minute;
        second = temp.second;
        era = temp.era;
    }

    public CustomDateTime(int _year, int _month, int _day, int _hour, int _minute, int _second, Era _era) {
        this.year = _year;
        this.month = _month;
        this.day = _day;
        this.hour = _hour;
        this.minute = _minute;
        this.second = _second;
        this.era = _era;
    }

    public double ToJulianDay() {
        return ToJulianDay(this);
    }

    public static double ToJulianDay(CustomDateTime customDateTime) {
        int jy, ja, jm;         //scratch

        if(customDateTime.year == 0) {
            Debug.LogError("There is no year 0 in the Julian system!");
        }
        if(customDateTime.year == 1582 && customDateTime.month == 10 && customDateTime.day > 4 && customDateTime.day < 15) {
            Debug.LogError("The dates 5 through 14 October, 1582, do not exist in the Gregorian system!");
        }

        //	if( y < 0 )  ++y;
        if(customDateTime.era == Era.BC) customDateTime.year = -customDateTime.year + 1;
        if(customDateTime.month > 2) {
            jy = customDateTime.year;
            jm = customDateTime.month + 1;
        } else {
            jy = customDateTime.year - 1;
            jm = customDateTime.month + 13;
        }

        int intgr = (int)Math.Floor(Math.Floor(365.25 * jy) + Math.Floor(30.6001 * jm) + customDateTime.day + 1720995);

        //check for switch to Gregorian calendar
        int gregcal = 15 + 31 * (10 + 12 * 1582);
        if(customDateTime.day + 31 * (customDateTime.month + 12 * customDateTime.year) >= gregcal) {
            ja = (int)Math.Floor(0.01 * jy);
            intgr += 2 - ja + (int)Math.Floor(0.25 * ja);
        }

        //correct for half-day offset
        double dayfrac = customDateTime.hour / 24.0 - 0.5;
        if(dayfrac < 0.0) {
            dayfrac += 1.0f;
            --intgr;
        }

        //now set the fraction of a day
        double frac = dayfrac + (customDateTime.minute + customDateTime.second / 60.0) / 60.0 / 24.0;

        //round to nearest second
        double jd0 = (intgr + frac) * 100000f;
        double jd = Math.Floor(jd0);
        if(jd0 - jd > 0.5) ++jd;
        return jd / 100000;
    }

    public static CustomDateTime FromJulianDay(double jday) {
        int j1, j2, j3, j4, j5;         //scratch

        //
        // get the date from the Julian day number
        //
        int intgr = (int)Math.Floor(jday);
        double frac = jday - intgr;
        int gregjd = 2299161;
        if(intgr >= gregjd) {               //Gregorian calendar correction
            int tmp = (int)Math.Floor(((intgr - 1867216) - 0.25) / 36524.25);
            j1 = intgr + 1 + tmp - (int)Math.Floor(0.25 * tmp);
        } else
            j1 = intgr;

        //correction for half day offset
        double dayfrac = frac + 0.5;
        if(dayfrac >= 1.0) {
            dayfrac -= 1.0;
            ++j1;
        }

        j2 = j1 + 1524;
        j3 = (int)Math.Floor(6680.0 + ((j2 - 2439870) - 122.1) / 365.25);
        j4 = (int)Math.Floor(j3 * 365.25);
        j5 = (int)Math.Floor((j2 - j4) / 30.6001);

        int d = (int)Math.Floor(j2 - j4 - Math.Floor(j5 * 30.6001));
        int m = (int)Math.Floor(j5 - 1.0);
        if(m > 12) { m -= 12; }
        int y = (int)Math.Floor(j3 - 4715.0);
        if(m > 2) { --y; }
        if(y <= 0) { --y; }

        //
        // get time of day from day fraction
        //
        int hr = (int)Math.Floor(dayfrac * 24.0);
        int mn = (int)Math.Floor((dayfrac * 24.0 - hr) * 60.0);
        double f = ((dayfrac * 24.0 - hr) * 60.0 - mn) * 60.0;
        int sc = (int)Math.Floor(f);
        f -= sc;
        if(f > 0.5) { ++sc; }

        Era era = Era.BC;

        if(y < 0) {
            y = -y;
            era = Era.BC;
        } else {
            era = Era.AD;
        }
        return new CustomDateTime(y, m, d, hr, mn, sc, era);
    }

}

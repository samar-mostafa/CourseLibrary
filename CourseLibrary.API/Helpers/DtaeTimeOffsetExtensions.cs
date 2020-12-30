﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DtaeTimeOffsetExtensions
    {

        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - dateTimeOffset.Year;
            if(currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }


    }
}
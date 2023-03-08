using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class FormatNumbers 
{
    // Start is called before the first frame update
    private static readonly SortedDictionary<long, string> abbrevations = new SortedDictionary<long, string>
     {
         {1000," k"},
         {1000000, " m" },
         {1000000000, " b" },
         {1000000000000, " t" },
         {1000000000000000, " q" }
     };
    public static string AbbreviateNumber(long number)
    {
        long roundedNumber = number;
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            KeyValuePair<long, string> pair = abbrevations.ElementAt(i);
            if (Mathf.Abs(number) >= pair.Key)
            {
                float temp = (int)(((double)roundedNumber / pair.Key) * 10);
                float returningNumber = temp / 10;
                return returningNumber.ToString() + pair.Value;
            }
        }
        return number.ToString();
    }
    public static string AbbreviateNumberForTotalMoney(long number)
    {
        long roundedNumber = number;
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            KeyValuePair<long, string> pair = abbrevations.ElementAt(i);
            if (Mathf.Abs(roundedNumber) >= pair.Key)
            {
                double temp = (int)(((double)roundedNumber / pair.Key) * 1000);
                double returningNumber = temp / 1000;

                return returningNumber.ToString() + pair.Value;
            }
        }
        return number.ToString();
    }

    public static long RoundNumberLikeText(long number)
    {
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            KeyValuePair<long, string> pair = abbrevations.ElementAt(i);
            if (Mathf.Abs(number) >= pair.Key)
            {
                double temp = (long)(((double)number / pair.Key) * 1000);
                return (long)((temp / 1000) * pair.Key);
            }

        }
        return (long)number;
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{
    public static void Main()
    {
        var qwe = new Solution();

        qwe.LongestDupSubstring("banana");
        //nsole.WriteLine(qwe.TwoCitySchedCost(new int[] { new int[] { 255, 770 }));
        //Console.WriteLine(qwe.LongestCommonPrefix(new string[] { "flower", "flow", "flight" }) == "fl");
        //Console.WriteLine(qwe.LongestCommonPrefix(new string[] { "flower", "flow", "flight", "" }) == "");
        //Console.WriteLine(qwe.LongestCommonPrefix(new string[] { "flower", "flow" }) == "flow");
        //Console.WriteLine(qwe.LongestCommonPrefix(new string[] { "dog", "racecar", "car" }) == "");
    }

    public class Solution
    {
        public string LongestDupSubstring(string S)
        {
            var sentence = S.AsSpan();
            var length = S.Length;

            while (length > 0)
            {
                length /= 2;
                for (var i = 0; i <= S.Length - length; i += length)
                {
                    var aux = 0;
                    var current = sentence.Slice(i, length);

                    for (var j = 0; j + length <= S.Length; j++)
                        if (sentence.Slice(j, length).SequenceEqual(current) && ++aux > 1)
                            return current.ToString();
                }
            }

            return string.Empty;
        }
    }
}
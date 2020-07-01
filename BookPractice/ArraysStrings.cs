using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookPractice
{
    /// <summary>
    /// Chapter 1 Exercises Arrays and Strings 
    /// </summary>
    public class ArraysStrings
    {
        #region .:: 1.1 - Is Unique ::.

        // Is Unique: Implement an algorithm to determine if a string has all unique characters. 
        // What if you cannot use additional data structures? 

        #region .: My Anwser :.

        [Theory]
        [InlineData("aaa", false)]
        [InlineData("qwezxchkj iopm", true)]
        public void IsUnique(string value, bool assertAnswer)
        {
            // Complexity: O(N);
            // Space Complexity: O(N)

            var hashTable = new HashSet<char>();
            for (int i = 0; i < value.Length; i++)
                hashTable.Add(value[i]);

            Assert.Equal(assertAnswer, hashTable.Count == value.Length);
        }

        // What if you cannot use additional data structures?

        [Theory]
        [InlineData("aaa", false)]
        [InlineData("qwezxchkj iopm", true)]
        public void IsUnique_Without_DataStructures(string value, bool assertAnswer)
        {
            // Complexity: O(N^2);
            // Space Complexity: O(1)

            var isUnique = true;
            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < value.Length; j++)
                    if (i != j && value[i] == value[j])
                    {
                        isUnique = false;
                        break;
                    }

                if (!isUnique)
                    break;
            }

            Assert.Equal(assertAnswer, isUnique);
        }

        #endregion

        #region .: Book Anwser :.

        // Note:
        // We can also immediately return false if the string length exceeds the number of unique characters in the alphabet.After all, 
        // you can't form a string of 280 unique characters out of a 128-character alphabet. 

        [Theory]
        [InlineData("aaa")]
        [InlineData("qwezxchkj iopm")]
        public void IsUniqueChars(string str)
        {
            // Time Complexity: O(n)
            // Space Complexity: O(1)

            if (str.Length > 128)
                Assert.True(false);

            var char_set = new bool[128];
            for (int i = 0; i < str.Length; i++)
            {
                if (char_set[str[i]])
                    Assert.True(false);

                char_set[str[i]] = true;
            }

            Assert.True(true);
        }

        // Note:
        // We can reduce our space usage by a factor of eight by using a bit vector.We will assume, in the below code, 
        // that the string only uses the lowercase letters a through z.This will allow us to use just a single int. 

        [Theory]
        [InlineData("aaa")]
        [InlineData("qwezxchkj iopm")]
        public void IsUniqueChars_2(string str)
        {
            int checker = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int val = str[i] - 'a';
                if ((checker & (1 << val)) > 0)
                    Assert.True(false);

                checker |= (1 << val);
            }

            Assert.True(true);
        }

        #endregion

        #endregion

        #region .:: 1.2 - Check Permutation ::.

        // Check Permutation: Given two strings, write a method to decide if one is a permutation of the other.

        [Theory]
        [InlineData("pedro", "ordep")]
        [InlineData("xablau", "bluaxa")]
        [InlineData("xablau", "xablum")]
        public void CheckPermutation(string str1, string str2)
        {
            // TimeComplexity: O(2N + 128) ~= O(N)
            // SpaceComplexity: O(128) ~= O(1)

            // Note: I am considering the string params will be in ASCII
            if (str1.Length != str2.Length)
                Assert.False(true);

            var charSet = new byte[128];
            for (int i = 0; i < str1.Length; i++)
                charSet[str1[i]]++;

            for (int i = 0; i < str2.Length; i++)
                charSet[str2[i]]--;

            for (int i = 0; i < charSet.Length; i++)
                if (charSet[i] != 0)
                    Assert.True(false);

            Assert.True(true);
        }

        #endregion

        #region .:: 1.3 - URLify ::.

        // URLify: Write a method to replace all spaces in a string with '%20'. 
        // You may assume that the string has sufficient space at the end to hold the additional characters, 
        // and that you are given the "true" length of the string. 
        // (Note: If implementing in Java, please use a character array so that you can perform this operation in place.)

        // EXAMPLE: 
        // Input: "Mr John Smith ", 13 
        // Output: "Mr%20John%20Smith"

        [Theory]
        [InlineData(" John Smith    ", 11, "%20John%20Smith")]
        [InlineData("Mr John Smith    ", 13, "Mr%20John%20Smith")]
        public void URLify(string completeString, int realLength, string expected)
        {
            // Time Complexity: O(N)
            // Space Complexity: O(1)

            // Note: If implementing in Java, please use a character array so that you can perform this operation in place.
            // => Because strings are imutable on Java and in this case C# too
            var value = completeString.ToCharArray();

            void putUrlSpace(ref int index)
            {
                value[--index] = '0';
                value[--index] = '2';
                value[--index] = '%';
            }

            var insertAux = value.Length;
            for (int i = realLength - 1; i >= 0; i--)
            {
                var currentChar = value[i];
                if (currentChar == ' ')
                    putUrlSpace(ref insertAux);
                else value[--insertAux] = currentChar;
            }

            var outputString = new string(value, 0, value.Length);
            Assert.Equal(expected, outputString);
        }

        #endregion

        #region .:: 1.4 - Palindrome Permutation ::.

        // Palindrome Permutation: Given a string, write a function to check if it is a permutation of a palindrome.
        // A palindrome is a word or phrase that is the same forwards and backwards. A permutation is a rearrangement of letters.
        // The palindrome does not need to be limited to just dictionary words.

        // EXAMPLE 
        // Input: Tact Coa 
        // Output: True(permutations: "taco cat", "atco cta", etc.)

        [Theory]
        [InlineData("becceb")]
        [InlineData("becoiceb")]
        [InlineData("becoceb")]
        [InlineData("tact coa")]
        public void PalindromePermutation(string str)
        {
            // Time Complexity: O(N + 26) ~= O(N)
            // Space Complexity: O(1)

            int minCharValue = 'a';
            int maxCharValue = 'z';
            var charSet = new int[maxCharValue - minCharValue];

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] > maxCharValue || str[i] < minCharValue)
                    continue;

                charSet[str[i] - 'a']++;
            }

            var oddCount = 0;
            for (int i = 0; i < charSet.Length; i++)
                if ((charSet[i] & 1) == 1 && ++oddCount > 1)
                    Assert.True(false);

            Assert.True(true);
        }

        [Theory]
        [InlineData("becceb")]
        [InlineData("becoceb")]
        [InlineData("becoiceb")]
        [InlineData("tact coa")]
        public void PalindromePermutation_BitVector(string str)
        {
            // Time Complexity: O(N)
            // Space Complexity: O(1)

            int bitVector = 0;
            int minCharValue = 'a';
            int maxCharValue = 'z';

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] > maxCharValue || str[i] < minCharValue)
                    continue;

                var mask = 1 << str[i];
                if ((bitVector & mask) == 0)
                    bitVector |= mask;
                else bitVector &= ~mask;
            }

            Assert.True(bitVector == 0 || (bitVector & (bitVector - 1)) == 0);
        }

        #endregion

        #region .:: 1.5 - One Away ::.

        // One Away: There are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character.
        // Given two strings, write a function to check if they are one edit (or zero edits) away.

        // EXAMPLE: 
        // pale, ple -> true 
        // pales, pale -> true 
        // pale, bale -> true 
        // bale, bble -> true
        // pale, bake -> false 

        [Theory]
        [InlineData("pale", "ple")]
        [InlineData("pale", "bale")]
        [InlineData("bale", "bble")]
        [InlineData("pale", "bake")]
        [InlineData("bble", "byie")]
        [InlineData("pales", "pale")]
        public void OneAway(string str1, string str2)
        {
            // Space Complexity O(1)
            // Time Complexity O(M + N + 25) ~= O(N+M)

            int minCharValue = 'a';
            int maxCharValue = 'z';
            var charSet = new byte[maxCharValue - minCharValue];

            for (int i = 0; i < str1.Length; i++)
                if (str1[i] <= maxCharValue && str1[i] >= minCharValue)
                    charSet[str1[i] - 'a']++;

            for (int i = 0; i < str2.Length; i++)
            {
                var charIndex = str2[i] - 'a';
                if (str2[i] <= maxCharValue && str2[i] >= minCharValue && charSet[charIndex] > 0)
                    charSet[charIndex]--;
            }

            var diff = 0;
            for (int i = 0; i < charSet.Length; i++)
                diff += charSet[i];

            Assert.True(diff <= 1 && diff >= -1);
        }

        // Muchhhhhh better =/
        #region .:: Book Answer ::.

        [Theory]
        [InlineData("pale", "ple")]
        [InlineData("pale", "bale")]
        [InlineData("bale", "bble")]
        [InlineData("pale", "bake")]
        [InlineData("bble", "byie")]
        [InlineData("pales", "pale")]
        public void OneAway_Book(string first, string second)
        {
            /* Length checks. */
            if (Math.Abs(first.Length - second.Length) > 1)
                Assert.True(false);

            /* Get shorter and longer string.*/
            string s1 = first.Length < second.Length ? first : second;
            string s2 = first.Length < second.Length ? second : first;

            int indexl = 0;
            int index2 = 0;
            bool foundDifference = false;
            while (index2 < s2.Length && indexl < s1.Length)
            {
                if (s1[indexl] != s2[index2])
                {
                    /* Ensure that this is the first difference found.*/
                    if (foundDifference) Assert.True(false);
                    foundDifference = true;

                    // On replace, move shorter pointer
                    if (s1.Length == s2.Length)
                        indexl++;
                }
                else indexl++; // If matching, move shorter pointer 
                index2++; // Always move pointer for longer string 
            }

            Assert.True(true);
        }

        #endregion

        #endregion

        #region .:: 1.6 - String Compression ::.

        // String Compression: Implement a method to perform basic string compression using the counts of repeated characters.
        // For example, the string aabcccccaaa would become a2blc5a3.If the "compressed" string would not become smaller than the original string, 
        // your method should return the original string. You can assume the string has only uppercase and lowercase letters (a -z). 

        // MyNote: Read carefuly the text and always read many times the expected output (30 minutes waste)

        [Theory]
        [InlineData("aabcccccaaa", "a2b1c5a3")]
        public void StringCompression(string str, string expected)
        {
            var countAux = 1;
            var lastLetter = str[0];
            var newStringComressed = new StringBuilder();

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == lastLetter) countAux++;
                else 
                {
                    newStringComressed.Append(lastLetter);
                    newStringComressed.Append(countAux);
                    lastLetter = str[i];
                    countAux = 1;
                }
            }

            newStringComressed.Append(lastLetter);
            newStringComressed.Append(countAux);

            if (newStringComressed.Length > str.Length)
                 Assert.Equal(expected, str);
            else Assert.Equal(expected, newStringComressed.ToString());
        }

        #endregion

        #region .:: 1.7 - Rotate Matrix ::.

        // Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, 
        // write a method to rotate the image by 90 degrees. Can you do this in place? 

        private int[,] generateMatrix(int n)
        {
            var output = new int[n, n];
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    output[i, j] = random.Next(100);

            return output;
        }

        [Fact]
        public void Rotate90()
        {
            // Time Complexity: O(n*n)
            // Space Complexity: O(n*n)

            var matrix = generateMatrix(2);
            var length = matrix.GetLength(0);
            var aux = new int[length, length];

            for (int x = 0; x < length; x++)
            {
                var currentChangeY = (length - x) -1;
                for (int y = 0; y < length; y++)
                    aux[y, currentChangeY] = matrix[x, y];
            }
        }

        [Fact]
        public void Rotate90_Book()
        {
            // Time Complexity: O(n*n)
            // Space Complexity: O(1)

            var length = 2;
            var matrix = generateMatrix(length);

            for (int layer = 0; layer < length / 2; layer++)
            {
                int first = layer;
                int last = length - 1 - layer;
                for (int i = first; i < last; i++)
                {
                    int offset = i - first;
                    int top = matrix[first, i];

                    matrix[first, i] = matrix[last - offset, first];
                    matrix[last - offset,first] = matrix[last,last - offset];
                    matrix[last, last - offset] = matrix[i, last];
                    matrix[i, last] = top;
                }
            }
        }

        #endregion

        #region .:: 1.8 - Zero Matrix ::.

        // Zero Matrix: Write an algorithm such that if an element in an MxN matrix is 0, its entire row and column are set to 0. 

        [Fact]
        public void ZeroMatrixTest()
        {
            var matrix = new int[4, 4]
            {
                { 1, 2, 3, 0 },
                { 2, 0, 0, 5 },
                { 0, 3, 7, 9 },
                { 5, 3, 7, 9 }
            };

            ZeroMatrix(matrix);
        }

        public void ZeroMatrix(int[,] matrix)
        {
            // Space Complexity: O(M + N)
            // Time Complexity: O(MN)

            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            var matrixWholeZero = false;
            var zeroRows = new HashSet<int>(rowsCount);
            var zeroColumns = new HashSet<int>(columnsCount);

            for (int i = 0; i < rowsCount; i++)
            {
                if (matrixWholeZero)
                    break;

                for (int j = 0; j < columnsCount; j++)
                    if (matrix[i, j] == 0)
                    {
                        zeroRows.Add(i);
                        zeroColumns.Add(j);

                        if (zeroRows.Count == rowsCount || zeroColumns.Count == columnsCount)
                        {
                            matrixWholeZero = true;
                            break;
                        }
                    }
            }
                
            foreach (var row in zeroRows)
                for (int j = 0; j < columnsCount; j++)
                    matrix[row, j] = 0;

            foreach (var column in zeroColumns)
                for (int j = 0; j < rowsCount; j++)
                    matrix[j, column] = 0;
        }

        #endregion

        #region .:: 1.9 - String Rotation ::.

        // String Rotation: Assume you have a method isSubstring which checks if one word is a substring of another. 
        // Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one call to isSubstring.
        // e.g: "waterbottle" is a rotation of "erbottlewat".

        [Theory]
        [InlineData("waterbottle", "erbottlewat")]
        public void IsRotation(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                Assert.True(false);

            var cutIndex = 0;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] == s2[0])
                {
                    cutIndex = i;
                    var subString = s2.Substring(0, s2.Length - i);

                    if (!s1.Contains(subString))
                        Assert.True(false);

                    break;
                }

            var s1StartIndex = s1.Length - cutIndex;
            for (int i = s1StartIndex; i < s1.Length; i++)
                if (s2[i] != s1[i - s1StartIndex])
                    Assert.True(false);

            Assert.True(true);
        }

        // Muchhhhhhh better
        #region .:: Book Answer ::.

        bool isRotation(string sl, string s2)
        {
            int len = sl.Length;
            if (len == s2.Length && len > 0)
            {
                string slsl = sl + sl;
                return slsl.Contains(s2);
            }

            return false;
        }

        #endregion

        #endregion
    }
}

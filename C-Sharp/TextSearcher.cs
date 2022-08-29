using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Token
{
    public class TextSearcher
    {
        public TextSearcher(string filePath)
        {
            Data = System.IO.File.ReadAllText(filePath, System.Text.Encoding.UTF8);

            // Split into space delimited array of strings
            Words = Data.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToArray(); 

        }

        public string[] Search(string queryWord, int contextWords)
        {
            List<string> results = new List<string>();

            // Use of regex and LINQ queries to search for indices of the word we are looking for inside of our space delimited Words array 
            var searchPattern = new Regex(queryWord, RegexOptions.IgnoreCase);
            List<int> result = Words.Select((s, i) => new { i, s }).Where(t => searchPattern.IsMatch(t.s)).Select(t => t.i).ToList();

            String [] wordsArray;

            for (int p = 0; p < result.Count; p++)
            {
                //TODO:NearEnd

                //NearBeginning 
                if (((result[p] - contextWords) > 0) == false && (contextWords * 2 + 1) > ((Words.Length) - 1) == false)
                {
                    wordsArray = Enumerable.Range(0, contextWords + result[p] + 1).Select(i => Words[i]).ToArray();

                }else //Everything else
                {
                    wordsArray = Enumerable
                        .Range((result[p] - contextWords) > 0 ? (result[p] - contextWords) : 0, (contextWords * 2 + 1 > (Words.Length) - 1) ? (Words.Length) - 1 : contextWords * 2 + 1)
                        .Select(i => Words[i]).ToArray();
                }

                results.Add(string.Join(" ", wordsArray));
            }

            return results.ToArray();
        }

        public string Data { get; set; }
        public string[] Words { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using AhoCorasick;

namespace ConString
{
    public static class ConString
    {
        /// <summary>
        /// This filters 6 letter words from the list and return ones that are composed of 2 smaller words in the same list
        /// </summary>
        public static IEnumerable<string> FilterConcatenatedStrings(this IEnumerable<string> list)
        {
            // Do not processes if the list is empty
            if (list == null || !list.Any())
            {
                return new List<string>();
            }

            // find all the 6 letter words in the List
            IEnumerable<string> stringsOfSize6 = list.StringsOfSize6();

            var result = new List<string>();
            
            // process the 6 letter words (candidate) and add matches to the result List
            result.AddRange(
                from candidate in stringsOfSize6
                // Return a list of substrings of the candidate word
                // Note: This uses  Aho–Corasick, NOT written by me
                let matches = list.GenerateMatches(candidate)

                // this generates all the variations of 2 substrings and compares them to the candidate
                // NOTE: could have used Fisher–Yates shuffle here, but the lazy linq is not too bad, need to measure and compare for larger datasets
                where matches.GetVariationStrings().Any(x => x.Contains(candidate)) 
                select candidate);

            return result;
        }

        /// <summary>
        /// This uses Trie, an Aho–Corasick string matching algorithm. This returns every substring of candidate in input
        /// </summary>
        public static IEnumerable<string> GenerateMatches(this IEnumerable<string> input, string candidate)
        {
            var potentialMatchList = input.Where(x => x != candidate && x.Length < candidate.Length);

            var trie = new Trie();
            trie.Add(potentialMatchList);

            return trie.Find(candidate);
        }

        /// <summary>
        /// Returns all strings of length 6 in input
        /// </summary>
        public static IEnumerable<string> StringsOfSize6(this IEnumerable<string> input)
        {
            return input.Where(x => x.Length == 6);
        }

        /// <summary>
        /// Gets all the variations of the strings in input by concatenating 2 eg:
        /// for set {ab, cd, ef} the following is generated
        /// {abcd, abef, cdef, cdab, efab, efcd}
        /// </summary>
        public static IEnumerable<string> GetVariationStrings(this IEnumerable<string> input)
        {
            return input.SelectMany(
                    (value, index) => input.Skip(index + 1),
                    (first, second) => first + second)
                .Union(input.SelectMany(
                    (value, index) => input.Skip(index + 1),
                    (first, second) => second + first));
        }
    }
}
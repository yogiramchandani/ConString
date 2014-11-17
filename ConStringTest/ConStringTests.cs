using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConString.Test
{
    [TestClass]
    public class ConStringTests
    {
        [TestMethod]
        public void FilterConcatenatedStrings_WhenANullListIsSet_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = ((List<string>)null).FilterConcatenatedStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void FilterConcatenatedStrings_WhenAnEmptyListIsSet_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>().FilterConcatenatedStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void FilterConcatenatedStrings_WhenTheListDoesNotContain6LetterWords_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string> { "a", "ab", "abc", "abcd", "abcde" }.FilterConcatenatedStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void FilterConcatenatedStrings_WithAssignmentInputs()
        {
            IEnumerable<string> actual = new List<string>
                {
                    "al",
                    "albums",
                    "aver",
                    "bar",
                    "barely",
                    "be",
                    "befoul",
                    "bums",
                    "by",
                    "cat",
                    "con",
                    "convex",
                    "ely",
                    "foul",
                    "here",
                    "hereby",
                    "jig",
                    "jigsaw",
                    "or",
                    "saw",
                    "tail",
                    "tailor",
                    "vex",
                    "we",
                    "weaver"
                }.FilterConcatenatedStrings();

            CollectionAssert.AreEqual(actual.ToList(),
                new List<string> { "albums", "barely", "befoul", "convex", "hereby", "jigsaw", "tailor", "weaver" });
        }

        [TestMethod]
        public void FilterConcatenatedStrings_WhereSmallerStringsOverlap_ShouldIgnoreOverlap()
        {
            IEnumerable<string> actual = new List<string>
                {
                    "al",
                    "albu",
                    "ms",
                    "albums"
                }.FilterConcatenatedStrings();

            CollectionAssert.AreEqual(actual.ToList(),
                new List<string> { "albums" });
        }

        [TestMethod]
        public void FilterConcatenatedStrings_WhereSmallerStringsOverlap_AndIsReversed_ShouldIgnoreOverlap()
        {
            IEnumerable<string> actual = new List<string>
                {
                    "al",
                    "albu",
                    "ms",
                    "msalbu"
                }.FilterConcatenatedStrings();

            CollectionAssert.AreEqual(actual.ToList(),
                new List<string> { "msalbu" });
        }

        [TestMethod]
        public void GetVariationStrings_WhenListEmpty_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>().GetVariationStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void GetVariationStrings_With1Word_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>{"ab"}.GetVariationStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void GetVariationStrings_With2Words_ShouldReturn2()
        {
            IEnumerable<string> actual = new List<string>{"ab", "cd"}.GetVariationStrings();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>{"abcd", "cdab"});
        }

        [TestMethod]
        public void GetVariationStrings_With3Words_ShouldReturn2()
        {
            IEnumerable<string> actual = new List<string>{"ab", "bc", "cd"}.GetVariationStrings();
            CollectionAssert.AreEquivalent(actual.ToList(), new List<string>{"abbc", "abcd", "bcab", "bccd", "cdab", "cdbc"});
        }

        [TestMethod]
        public void GenerateMatches_WhenEmptyStringIsProvided_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>().GenerateMatches("");
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void GenerateMatches_WhenEmptyListProvided_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>().GenerateMatches("abcde");
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void GenerateMatches_WhenListHasNoMatches_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>{"fg", "hij", "cdefg"}.GenerateMatches("abcde");
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void GenerateMatches_WhenListHas3Matches_ShouldReturn3()
        {
            IEnumerable<string> actual = new List<string>{"al", "albu", "ms", "abcd", "fg", "hij", "cdefg"}.GenerateMatches("albums");
            CollectionAssert.AreEqual(actual.ToList(), new List<string>{"al", "albu", "ms"});
        }

        [TestMethod]
        public void StringsOfSize6_WhenGivenAnEmptyList_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>().StringsOfSize6();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void StringsOfSize6_WhenGivenAListWithLessThan6Letters_ShouldReturnEmpty()
        {
            IEnumerable<string> actual = new List<string>{"abc", "a", "abcde"}.StringsOfSize6();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>());
        }

        [TestMethod]
        public void StringsOfSize6_WhenGivenAListWith6LetterWord_ShouldReturn1Match()
        {
            IEnumerable<string> actual = new List<string>{"abc", "a", "abcde", "abcdef"}.StringsOfSize6();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>{"abcdef"});
        }

        [TestMethod]
        public void StringsOfSize6_WhenGivenAListWithMoreThan6Letters_ShouldReturn1Match()
        {
            IEnumerable<string> actual = new List<string>{"abc", "a", "abcde", "abcdef", "abcdefg"}.StringsOfSize6();
            CollectionAssert.AreEqual(actual.ToList(), new List<string>{"abcdef"});
        }
    }
}
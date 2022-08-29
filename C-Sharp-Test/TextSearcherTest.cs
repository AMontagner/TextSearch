using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Token;

namespace TextSearcherTest
{
    [TestClass]
    public class TextSearcherShortText
    {
        TextSearcher tSearch;

        [TestInitialize]
        public void InitializeShortFile()
        {
            // you may nned to change this to fit your environment
            string apath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(apath, @"../../../files/short_excerpt.txt");
            tSearch = new TextSearcher(path);
        }

        [TestMethod]
        [Timeout(1000)]
        public void BasicSearchNoContext()
        {
            string[] expected = new string[] { "sketch" };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("sketch", 0);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void MultipleHitsNoContext()
        {
            string[] expected = new string[]  {
                "naturalists",
                "naturalists,"
              };
            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("naturalists", 0);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void MultipleHits3Context()
        {
            string[] expected = new string[]  {
                "great majority of naturalists believed that species",
                "authors. Some few naturalists, on the other"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("naturalists", 3);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void MultipleHits6Context()
        {
            string[] expected = new string[]  {
                "Until recently the great majority of naturalists believed that species were immutable productions,",
                "maintained by many authors. Some few naturalists, on the other hand, have believed"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("naturalists", 6);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void CaseInsensitiveLower()
        {
            string[] expected = new string[]  {
                "on the Origin of Species. Until recently the great",
                "of naturalists believed that species were immutable productions, and",
                "hand, have believed that species undergo modification, and that"
            };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("species", 4);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void CaseInsensitiveUpper()
        {
            string[] expected = new string[]  {
                "on the Origin of Species. Until recently the great",
                "of naturalists believed that species were immutable productions, and",
                "hand, have believed that species undergo modification, and that"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("SPECIES", 4);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void CaseInsensitiveMixed()
        {
            string[] expected = new string[]  {
                "on the Origin of Species. Until recently the great",
                "of naturalists believed that species were immutable productions, and",
                "hand, have believed that species undergo modification, and that"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("SpEcIeS", 4);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void NearBeginning()
        {
            string[] expected = new string[]  {
                "I will here give a brief sketch"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("here", 4);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void NearEnd()
        {
            string[] expected = new string[]  {
                "and that the existing forms of life",
                "generation of pre existing forms."
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("existing", 3);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void Overlapping()
        {
            string[] expected = new string[]  {
                "of naturalists believed that species were immutable",
                "hand, have believed that species undergo modification,",
                "undergo modification, and that the existing forms"
              };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("that", 3);
                CollectionAssert.AreEqual(expected, matches);
            }
        }
    }

    [TestClass]
    public class TextSearcherLongText
    {
        TextSearcher tSearch;

        [TestInitialize]
        public void InitializeShortFile()
        {
            string apath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(apath, @"../../../files/long_excerpt.txt");
            tSearch = new TextSearcher(path);
        }

        [TestMethod]
        [Timeout(1000)]
        public void Apostrophe()
        {
            string[] expected = new string[]  {
                "not indeed to the animal's or plant's own good",
                "habitually speak of an animal's organisation as\r\nsomething plastic"
            };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("animal's", 4);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void Numeric()
        {
            string[] expected = new string[]  {
                "enlarged in 1844 into a",
                "sketch of 1844--honoured me"
            };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("1844", 2);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void Mixed()
        {
            string[] expected = new string[]  {
                "date first edition [xxxxx10x.xxx] please check"
            };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("xxxxx10x", 3);
                CollectionAssert.AreEqual(expected, matches);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void NoHits()
        {
            string[] expected = new string[] { };

            for (int i = 0; i < 1000; i++)
            {
                string[] matches = tSearch.Search("slejrlskejrlkajlsklejrlksjekl", 3);
                CollectionAssert.AreEqual(expected, matches);
            }
        }
    }
}
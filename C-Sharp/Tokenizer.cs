using System.Collections.Generic;
using System.Text.RegularExpressions;

// These classes implement a tokenizer. 
// The Tokenizer class returns a list of "tokens" in the file.
// "tokens" are any string of characters that matches the regex and
// and are represented by the Token class. The Token class stores
// the word that matched and the char start and end positions of the word
// in the input data.

namespace Token
{
    public class Token
    {
        public Token(string iword, int istart, int iend)
        {
            word = iword;
            start = istart;
            end = iend;
        }

        public string word;
        public int start;
        public int end;
    }

    public class Tokenizer
    {
        public Tokenizer(string input, string regex)
        {
            this.input = input;
            this.pattern = new Regex(regex);
        }

        public List<Token> Lex()
        {
            List<Token> tokens = new();
            foreach (Match match in pattern.Matches(input))
            {
                if (match != null)
                {
                    tokens.Add(new Token(match.ToString(), match.Index, match.Index + match.ToString().Length));
                }
            }
            return tokens;
        }

        public Regex pattern;
        public string input;
    }
}

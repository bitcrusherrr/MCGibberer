using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCGibberer
{
    class MapBuilder
    {
        private List<WordChainItem> _wordList;

        public MapBuilder(string input)
        {
            _wordList = new List<WordChainItem>();

            Regex regex = new Regex(@"^(\s+|\d+|\w+|[^\d\s\w])+$");

            if (regex.IsMatch(input))
            {
                Match match = regex.Match(input);

                WordChainItem previousWord = null;

                foreach (Capture splitWord in match.Groups[1].Captures)
                {
                    if (!string.IsNullOrWhiteSpace(splitWord.Value))
                    {
                        WordChainItem word = _wordList.Find(x => x.Word.ToLower() == splitWord.Value.ToLower());

                        if (word == null)
                        {
                            word = new WordChainItem(splitWord.Value);
                            _wordList.Add(word);
                        }

                        if (previousWord != null)
                            previousWord.AddFollower(word);

                        previousWord = word;
                    }
                }
            }
        }

        internal string MakeSentence(int SentenceLength, int wordDistance)
        {
            int startingLoc = new Random().Next(_wordList.Count);

            List<WordChainItem> sentence = new List<WordChainItem>();
            sentence.Add(_wordList[startingLoc]);

            for (int wordCount = 1; wordCount < SentenceLength; wordCount++)
                sentence.Add(sentence.Last().GetNextWord(wordDistance, sentence[0]));

            string result = "";

            Regex regex = new Regex(@"\W");

            foreach (var item in sentence)
            {
                if (!regex.IsMatch(item.Word))
                    result += " " + item.Word;
                else
                    result += item.Word;
            }

            return result;      
        }
    }
}

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
        private Random _randomizer = new Random();

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

        internal string MakeSentence(int SentenceLength, int wordDistance, bool fun = false)
        {
            int startingLoc = _randomizer.Next(_wordList.Count);

            List<WordChainItem> sentence = new List<WordChainItem>();
            sentence.Add(_wordList[startingLoc]);

            WordChainItem sentenceRoot = _wordList[startingLoc];

            //if in fun mode, set random root word.
            if(fun)
                sentenceRoot = _wordList[_randomizer.Next(_wordList.Count)];

            Regex regex = new Regex(@"\W");

            for (int wordCount = 1; wordCount < SentenceLength; wordCount++)
            {
                //Ignore punctuation
                if (!regex.IsMatch(sentence.Last().Word) && sentence.Last().Word.Length == 1)
                    wordCount--;

                sentence.Add(sentence.Last().GetNextWord(wordDistance, sentenceRoot));
            }

            string result = "";  

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

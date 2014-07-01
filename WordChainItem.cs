using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGibberer
{
    class WordChainItem
    {
        public string Word { get; set; }

        public List<WordChainItem> FollowingWords = new List<WordChainItem>();
        Random randomer = new Random();
        
        public WordChainItem(string word)
        {
            Word = word;
        }

        public void AddFollower(WordChainItem follower)
        {
            FollowingWords.Add(follower);
        }

        /// <summary>
        /// Get new chained word
        /// Take into account word depth. 
        /// The word should be wordDistance amount of words away from the new one
        /// </summary>
        /// <param name="wordDistance"></param>
        /// <returns></returns>
        internal WordChainItem GetNextWord(int wordDistance, WordChainItem firstWord)
        {
            //Get next word
            WordChainItem newWord = FollowingWords[randomer.Next(FollowingWords.Count - 1)];

            while (!TestDepth(firstWord, newWord, wordDistance))
                newWord = GetNextWord();

            return newWord;
        }

        private WordChainItem GetNextWord()
        {
            return FollowingWords[randomer.Next(FollowingWords.Count - 1)];
        }

        //Test how many links from the root word the new word is.
        private bool TestDepth(WordChainItem rootWord, WordChainItem newWord, int wordDistance)
        {
            bool found = false;

            if (wordDistance > -1)
            {
                //TODO: This will not work. Need to rewrite so that there is a collection of results. As threads can finiosh in order such as tru and then false, thus voiding the match
                //Parallel.ForEach(rootWord.FollowingWords, (word, state) =>
                //{
                //    if (word.Word.ToLower() == newWord.Word.ToLower())
                //        found = true;
                //    else if ((wordDistance - 1) > -1)
                //        found = TestDepth(word, newWord, (wordDistance - 1));

                //    if (found || wordDistance == 0)
                //        state.Stop();
                //});

                foreach (var word in rootWord.FollowingWords)
                {
                    if (word.Word.ToLower() == newWord.Word.ToLower())
                        found = true;
                    else if ((wordDistance - 1) > -1)
                        found = TestDepth(word, newWord, (wordDistance - 1));

                    if (found || wordDistance == 0)
                        break;
                }
            }

            return found;
        }
    }
}

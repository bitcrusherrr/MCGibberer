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

        private List<WordChainItem> followers = new List<WordChainItem>();
        Random randomer = new Random();
        
        public WordChainItem(string word)
        {
            Word = word;
        }

        public void AddFollower(WordChainItem follower)
        {
            followers.Add(follower);
        }

        /// <summary>
        /// Get new chained word
        /// </summary>
        internal WordChainItem GetNextChainItem()
        {
            return followers[randomer.Next(followers.Count - 1)];
        }
    }
}

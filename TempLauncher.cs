using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGibberer
{
    class TempLauncher
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader(@".\Test Text ENG.txt");

            MapBuilder map = new MapBuilder(streamReader.ReadToEnd());

            streamReader.Close();

            //Map now has list of all words, which also know what their possible folowers are
            Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);

            //Russian test
            //streamReader = new StreamReader(@".\Test Text RUS.txt");

            //map = new MapBuilder(streamReader.ReadToEnd());
            //Console.WriteLine("");
            //Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            //Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            //Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);
            //Console.WriteLine(map.MakeSentence(13) + Environment.NewLine);

            //streamReader.Close();

            Console.ReadKey();
        }
    }
}

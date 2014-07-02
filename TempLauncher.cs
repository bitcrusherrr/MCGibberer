using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Defaults
            int sentenceLength = 13;
            int sentcenceCount = 5;
            string sourcePath = @".\Test Text ENG.txt";

            if (args.Length != 0)
            {
                if (args.Length >= 1)
                    sentenceLength = int.Parse(args[0]);

                if (args.Length >= 2)
                    sentcenceCount = int.Parse(args[1]);

                if (args.Length == 3)
                    sourcePath = args[2];
            }

            StreamReader streamReader = new StreamReader(sourcePath);

            var watch = Stopwatch.StartNew();

            MapBuilder map = new MapBuilder(streamReader.ReadToEnd());

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            streamReader.Close();

            Console.WriteLine("Map created in " + elapsedMs / 1000 + "seconds" + Environment.NewLine);

            //Map now has list of all words, which also know what their possible folowers are
            for (int i = 0; i < sentcenceCount; i++)
                Console.WriteLine(map.MakeSentence(sentenceLength) + Environment.NewLine);

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

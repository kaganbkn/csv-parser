using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pi
{
    class Program
    {
        
        public const int max_song_id = 10000;
        public const int max_client_id = 1000;
        class data
        {
            public string play_id { get; set; }
            public int song_id { get; set; }
            public int client_id { get; set; }
            public string play_ts { get; set; }

            public static data FromLine(string line)
            {
                var input = line.Split('\t');
                return new data()
                {
                    play_id = input[0],
                    song_id = int.Parse(input[1]),
                    client_id = int.Parse(input[2]),
                    play_ts = input[3],
                };
            }
        }
        //deneme
        //deneme2
        //deneme3
        //deneme4
        //deneme5
        //deneme6
        //deneme7
        static void Main(string[] args)
        {
            args = new[] { "../../../exhibitA-input.csv" };
            var data = ReadData(args[0]);
            data = Distinct(data);
       //   find_max_client_id(data);
       //   find_max_song_id(data);
            Console.WriteLine("DISTINCT_PLAY_COUNT   "+ "CLIENT_COUNT");
            Expected(data);
            Console.ReadLine();
        }

        static IList<data> ReadData(string path)
        {
            var list = new List<data>();
            foreach (var line in File.ReadLines(path).Skip(1))
            {
                if (data.FromLine(line).play_ts.Contains("10/08/2016"))
                {
                    list.Add(data.FromLine(line));
                }
            }
            return list;
        }

        static IList<data> Distinct(IList<data> data)
        {
            bool[,] array = new bool[max_song_id, max_client_id];
            var newlist = new List<data>();
          
            foreach (var line in data)
            {
                 if (array[line.song_id,line.client_id] == true)
                {
                    continue;
                }
                else
                {
                    newlist.Add(line);
                    array[line.song_id, line.client_id] = true;
                }
            }
            return newlist;
        }

        static void Expected(IList<data> data)
        {
            int[] array = new int[max_client_id];
            int[] array_again = new int[max_client_id];


            for (int i = 0; i < max_client_id; i++)
            {
                array[i] = 0;
                array_again[i] = 0;

            }

            for (int i = 0; i < data.Count; i++)
            {
                array[data[i].client_id]++;
            }

            for (int i = 0; i < max_client_id; i++)
            {
                if (array[i] != 0)
                {
                    array_again[array[i]]++;
                }

            }

            for (int i = 0; i < max_client_id; i++)
            {
                if (array_again[i] != 0)
                {
                    Console.WriteLine("                 "+i + "             " + array_again[i]);
                }

            }
        }
       
        static void find_max_client_id(IList<data> data)
        {
            int temp = data[0].client_id;
            foreach (var line in data.Skip(1))
            {
                if (line.client_id > temp)
                {
                    temp = line.client_id;
                }
            }
            Console.WriteLine("find_max_client_id : " + temp);
        }

        static void find_max_song_id(IList<data> data)
        {
            int temp = data[0].song_id;
            foreach (var line in data.Skip(1))
            {
                if (line.song_id > temp)
                {
                    temp = line.song_id;
                }
            }
            Console.WriteLine("find_max_song_id : " + temp);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace Read_Novel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start stopwatch 
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var path = @"C:\Users\User\source\repos\Read_Novel\bin\2600-0.txt";

            // Read all text from file save in into a single string
            string text = File.ReadAllText(path);

            var size = (new FileInfo(path)).Length;
            var lines = File.ReadAllLines(path);

            // Create dictionary
            Dictionary<string, int> stats = new Dictionary<string, int>();

            // Creating a array list of characters
            char[] chars = { ' ', '.', ',', ';', ':', '?', '"', '"', '!', '(', ')', '/', '-', '\n' };

            // Split words
            string[] words = text.Split(chars);

            // Count words having more than 2 characters
            int minWordLength = 2;

            // Iterate over the word collection to count occurrences
            foreach (string word in words)
            {
                string w = word.Trim().ToLower();
                if (w.Length > minWordLength)
                {
                    if (!stats.ContainsKey(w))
                    {
                        // add new word to collection
                        stats.Add(w, 1);
                    }
                    else
                    {
                        // update word occurrence count
                        stats[w] += 1;
                    }
                }
            }

            var stats1 = stats.Select(x => new
            {
                Word = x.Key,
                Count = x.Value
            }).ToList();


            var top50 = stats1.OrderByDescending(x => x.Count).Take(50).ToList();
            var top50length6 = stats1.Where(x => x.Word.Length >= 6).OrderByDescending(x => x.Count).Take(50).ToList();

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            top50.ForEach(x =>
            {
                Console.WriteLine($"Word '{x.Word}' occurs {x.Count} times");
            });

            top50length6.ForEach(x =>
           {
               Console.WriteLine($"Word '{x.Word}' occurs {x.Count} times");

           });

            // Stop timing.
            stopwatch.Stop();


        }
    }
}


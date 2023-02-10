using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab2V21.DBStructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;

namespace Lab2V21
{
    class Finder
    {
        /// <summary>
        /// Controls searching elements in array by field by linear search
        /// </summary>
        /// <returns>
        /// Array of finded elements
        /// </returns>
        /// <param name="input">
        /// Input list of Info with sorted data
        /// </param>
        /// <param name="field">
        /// Field on which we search equal elements
        /// </param>
        public static List<Info> LinearSearch(List<Info> input, string? field)
        {
            List<Info> result = new List<Info>();

            DateTime start = DateTime.Now;

            for(int i = 0; i < input.Count; i++)
                if (input[i].AviacompanyName == field) result.Add(input[i]);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Linear search, {input.Count} elements, {(end - start).TotalMilliseconds} ms");

            return result;
        }

        /// <summary>
        /// Controls searching elements in array by field by binary search
        /// </summary>
        /// <returns>
        /// Array of finded elements
        /// </returns>
        /// <param name="input">
        /// Input list of Info with sorted data
        /// </param>
        /// <param name="field">
        /// Field on which we search equal elements
        /// </param>
        public static List<Info> BinarySearch(List<Info> input, string? field)
        {
            List<Info> result = new List<Info>();

            DateTime startWithSort = DateTime.Now;

            List<Info> sorted = MergeSortStart(input);

            DateTime startWithoutSort = DateTime.Now;

            BinarySearching(input, 0, input.Count - 1, field, result);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Binary search with sorting, {input.Count} elements, {(end - startWithSort).TotalMilliseconds} ms");
            Console.WriteLine($"Binary search without sorting, {input.Count} elements, {(end - startWithoutSort).TotalMilliseconds} ms");

            return result;
        }

        /// <summary>
        /// Search element by field in sorted array
        /// </summary>
        /// <returns>
        /// Returns sorted data
        /// </returns>
        /// <param name="input">
        /// Input list of Info with sorted data
        /// </param>
        /// <param name="start">
        /// Index of start of search
        /// </param>
        /// <param name="start">
        /// Index of end of search
        /// </param>
        /// <param name="field">
        /// Field on which we search equal elements
        /// </param>
        /// <param name="result">
        /// Output array with searched elements
        /// </param>
        static void BinarySearching(List<Info> input, int start, int end, string? field, List<Info> result)
        {
            if (start <= end) return;
            if(start == end)
            {
                if (string.Compare(input[start].AviacompanyName, field) == 0)
                    SearchInLine(input, start, field, out result);
                return;
            }

            if (string.Compare(input[(start + end) / 2].AviacompanyName, field) != 0)
            {
                if(string.Compare(input[(start + end) / 2].AviacompanyName, field) > 0)
                    BinarySearching(input, start, (start + end) / 2 - 1, field, result);
                else
                    BinarySearching(input, (start+end)/2 + 1, end, field, result);

                return;
            }

            SearchInLine(input, (start + end) / 2, field, out result);
        }

        /// <summary>
        /// Search equals elements in a row in sorted array
        /// </summary>
        /// <returns>
        /// Returns sorted data
        /// </returns>
        /// <param name="input">
        /// Input list of Info with sorted data
        /// </param>
        /// <param name="start">
        /// Index of start of search
        /// </param>
        /// <param name="field">
        /// Field on which we search equal elements
        /// </param>
        /// <param name="result">
        /// Output array with searched elements
        /// </param>
        static void SearchInLine(List<Info> input, int start, string? field, out List<Info> result)
        {
            int i = start;
            result = new List<Info>();
            while (i >= 0 && string.Compare(input[i].AviacompanyName, field) == 0)
                i--;
            i++;
            while (i < input.Count && string.Compare(input[i].AviacompanyName, field) == 0)
                result.Add(input[i]);
        }

        /// <summary>
        /// Controls sorting of list of Info by merge sort and outputs sorting time
        /// </summary>
        /// <returns>
        /// Returns sorted data
        /// </returns>
        /// <param name="input">
        /// Input list of Info with unsorted data
        /// </param>
        static List<Info> MergeSortStart(List<Info> input)
        {
            Info[] result = new Info[input.Count];
            input.CopyTo(result);

            List<Info> res = result.ToList();

            MergeSort(res, 0, input.Count - 1);

            return res;

        }

        /// <summary>
        /// Sorts list of Info by merge sort
        /// </summary>
        /// <param name="input">
        /// Input list of Info with unsorted data
        /// </param>
        /// /// <param name="start">
        /// Start of sortable range
        /// </param>
        /// /// <param name="end">
        /// End of sortable range
        /// </param>
        static void MergeSort(List<Info> input, int start, int end)
        {
            if (start == end)
                return;

            MergeSort(input, start, (start + end) / 2);
            MergeSort(input, (start + end) / 2 + 1, end);

            Merge(input, start, end, (start + end) / 2 + 1);
        }

        /// <summary>
        /// Merge two sorted lists of Info into one
        /// </summary>
        /// <param name="input">
        /// Input list of Info with data
        /// </param>
        /// /// <param name="start">
        /// Start of the first merging range
        /// </param>
        /// /// <param name="end">
        /// End of the second merging range
        /// </param>
        /// /// /// <param name="mid">
        /// End of the first merging range and the previous element to the start of the second merging range
        /// </param>
        static void Merge(List<Info> input, int start, int end, int mid)
        {
            int left = start, right = mid;

            List<Info> temp = new List<Info>();

            while (left < mid && right <= end)
            {
                if (string.Compare(input[left].AviacompanyName, input[right].AviacompanyName) <= 0)
                {
                    temp.Add(input[left++]);
                }
                else
                {
                    temp.Add(input[right++]);
                }
            }

            while (left < mid)
            {
                temp.Add(input[left++]);
            }

            while (right <= end)
            {
                temp.Add(input[right++]);
            }

            for (int i = 0; i < end - start + 1; i++)
                input[i + start] = temp[i];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            using (var db = new DataContext100())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext1000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext10000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext20000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext40000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext60000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext80000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
            using (var db = new DataContext100000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string?, Info> infos_look = (Lookup<string?, Info>)infos.ToLookup(x => x.AviacompanyName, x => x);
                string? searchField = infos[random.Next(infos.Count)].AviacompanyName;
                Finder.LinearSearch(infos, searchField);
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
        }
    }
}
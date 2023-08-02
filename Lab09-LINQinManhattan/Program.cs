using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection.Emit;

using System.Text.Json;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Lab09_LINQinManhattan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string json = File.ReadAllText("C:\\Users\\raygr\\Documents\\Dev\\labs\\Lab9Demo\\data.json");
            Console.WriteLine("Read file into string");

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);
            Console.WriteLine("Deserialized the json data");

            Location[] locations = featureCollection.features;
            Console.WriteLine(locations);

            // Output all of the neighborhoods
            OutputAllNeighborhoods(locations);
            // Filter out all the neighborhoods that do not have any names
            FilterOutNamelessNeighborhoods(locations);
            // Remove the duplicates
            RemoveDuplicateNeighborhoods(locations);
            // Rewrite the queries from above and consolidate all into one single query
            ConsolidatedQuery(locations);
            // Rewrite at least one of these questions only using the opposing method
            RemoveDuplicateNeighborhoodsQuerySyntax(locations);
        }

        public static void Part1(Location[] items)
        {
            Dictionary<string, int> locationAppearances = new Dictionary<string, int>();
            for (int i = 0; i < items.Length; i++)
            {
                Location currentLocation = items[i];
                string neighborhood = currentLocation.properties.neighborhood;
                bool neighborhoodAlreadyInDictionary = locationAppearances.ContainsKey(neighborhood);
                if (neighborhoodAlreadyInDictionary == false)
                {
                    locationAppearances.Add(neighborhood, 1);
                }
                else
                {
                    int currentValue = locationAppearances.GetValueOrDefault(neighborhood);
                    int newValue = currentValue + 1;
                    locationAppearances[neighborhood] = newValue;

                }
            }

            foreach (var location in locationAppearances)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }

        }

        public static void FilterOutNamelessNeighborhoods(Location[] items)
        {
            var namedNeighborhoods = items.Where(item => !string.IsNullOrEmpty(item.properties.neighborhood));

            foreach (var neighborhood in namedNeighborhoods)
            {
                Console.WriteLine(neighborhood.properties.neighborhood);
            }
        }
        public static void OutputAllNeighborhoods(Location[] items)
        {
            var allNeighborhoods = items.Select(item => item.properties.neighborhood);

            foreach (var neighborhood in allNeighborhoods)
            {
                Console.WriteLine(neighborhood);
            }
        }

        public static void RemoveDuplicateNeighborhoods(Location[] items)
        {
            var uniqueNeighborhoods = items.Select(item => item.properties.neighborhood).Distinct();

            foreach (var neighborhood in uniqueNeighborhoods)
            {
                Console.WriteLine(neighborhood);
            }
        }

        public static void ConsolidatedQuery(Location[] items)
        {
            var results = items
                .Select(item => item.properties.neighborhood)
                .Where(neighborhood => !string.IsNullOrEmpty(neighborhood))
                .Distinct();

            foreach (var neighborhood in results)
            {
                Console.WriteLine(neighborhood);
            }
        }

        public static void RemoveDuplicateNeighborhoodsQuerySyntax(Location[] items)
        {
            var uniqueNeighborhoods = (from item in items
                                       select item.properties.neighborhood).Distinct();

            foreach (var neighborhood in uniqueNeighborhoods)
            {
                Console.WriteLine(neighborhood);
            }
        }
    }
}
namespace Assignment_13
{
    using global::ASSLINQ;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    namespace ASSLINQ
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                // =========================================================
                // LINQ - Element Operators
                // =========================================================

                Console.WriteLine("===== Element Operators =====");

                // 1. Get the first Product out of Stock
                var firstOutOfStock = ListGenerators.ProductList
                    .FirstOrDefault(p => p.UnitsInStock == 0);

                Console.WriteLine("1. First product out of stock:");
                Console.WriteLine(firstOutOfStock);


                // 2. Return the first product whose Price > 1000
                var productOver1000 = ListGenerators.ProductList
                    .FirstOrDefault(p => p.UnitPrice > 1000);

                Console.WriteLine("\n2. First product with price > 1000:");
                Console.WriteLine(productOver1000 == null
                    ? "No product found"
                    : productOver1000.ToString());


                // 3. Retrieve the second number greater than 5
                int[] arr1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var secondGreaterThan5 = arr1
                    .Where(n => n > 5)
                    .Skip(1)
                    .FirstOrDefault();

                Console.WriteLine("\n3. Second number greater than 5:");
                Console.WriteLine(secondGreaterThan5);


                // =========================================================
                // LINQ - Aggregate Operators
                // =========================================================

                Console.WriteLine("\n===== Aggregate Operators =====");

                // 1. Count odd numbers
                int[] arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                int oddCount = arr2.Count(n => n % 2 != 0);

                Console.WriteLine("1. Number of odd numbers:");
                Console.WriteLine(oddCount);


                // 2. Customers and number of orders
                var customerOrders = ListGenerators.CustomerList
                    .Select(c => new
                    {
                        Customer = c.CustomerName,
                        OrdersCount = c.Orders.Count()
                    });

                Console.WriteLine("\n2. Customers and number of orders:");

                foreach (var item in customerOrders)
                {
                    Console.WriteLine($"{item.Customer} -> {item.OrdersCount}");
                }


                // 3. Categories and number of products
                var categoryProducts = ListGenerators.ProductList
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        ProductCount = g.Count()
                    });

                Console.WriteLine("\n3. Categories and number of products:");

                foreach (var item in categoryProducts)
                {
                    Console.WriteLine($"{item.Category} -> {item.ProductCount}");
                }


                // 4. Total of numbers
                int[] arr3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                int total = arr3.Sum();

                Console.WriteLine("\n4. Total:");
                Console.WriteLine(total);


                // Read dictionary
                string[] words = File.ReadAllLines("dictionary_english.txt");


                // 5. Total number of characters
                int totalCharacters = words.Sum(w => w.Length);

                Console.WriteLine("\n5. Total number of characters:");
                Console.WriteLine(totalCharacters);


                // 6. Length of shortest word
                int shortestWord = words.Min(w => w.Length);

                Console.WriteLine("\n6. Length of shortest word:");
                Console.WriteLine(shortestWord);


                // 7. Length of longest word
                int longestWord = words.Max(w => w.Length);

                Console.WriteLine("\n7. Length of longest word:");
                Console.WriteLine(longestWord);


                // 8. Average length
                double averageLength = words.Average(w => w.Length);

                Console.WriteLine("\n8. Average word length:");
                Console.WriteLine(averageLength);


                // 9. Total units in stock for each category
                var totalUnitsInStock = ListGenerators.ProductList
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        TotalUnits = g.Sum(p => p.UnitsInStock)
                    });

                Console.WriteLine("\n9. Total units in stock:");

                foreach (var item in totalUnitsInStock)
                {
                    Console.WriteLine($"{item.Category} -> {item.TotalUnits}");
                }


                // 10. Cheapest price among each category
                var cheapestPrice = ListGenerators.ProductList
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        CheapestPrice = g.Min(p => p.UnitPrice)
                    });

                Console.WriteLine("\n10. Cheapest price:");

                foreach (var item in cheapestPrice)
                {
                    Console.WriteLine($"{item.Category} -> {item.CheapestPrice}");
                }


                // 11. Products with lowest price in each category
                // Use LET
                var lowestProducts =
                    from p in ListGenerators.ProductList
                    group p by p.Category into g
                    let minPrice = g.Min(p => p.UnitPrice)
                    from p in g
                    where p.UnitPrice == minPrice
                    select new
                    {
                        Category = g.Key,
                        Product = p.ProductName,
                        Price = p.UnitPrice
                    };

                Console.WriteLine("\n11. Products with lowest price:");

                foreach (var item in lowestProducts)
                {
                    Console.WriteLine(
                        $"{item.Category} -> {item.Product} -> {item.Price}");
                }


                // 12. Highest price among each category
                var highestPrice = ListGenerators.ProductList
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        HighestPrice = g.Max(p => p.UnitPrice)
                    });

                Console.WriteLine("\n12. Highest price:");

                foreach (var item in highestPrice)
                {
                    Console.WriteLine($"{item.Category} -> {item.HighestPrice}");
                }


                // 13. Products with highest price in each category
                var highestProducts =
                    from p in ListGenerators.ProductList
                    group p by p.Category into g
                    let maxPrice = g.Max(p => p.UnitPrice)
                    from p in g
                    where p.UnitPrice == maxPrice
                    select new
                    {
                        Category = g.Key,
                        Product = p.ProductName,
                        Price = p.UnitPrice
                    };

                Console.WriteLine("\n13. Products with highest price:");

                foreach (var item in highestProducts)
                {
                    Console.WriteLine(
                        $"{item.Category} -> {item.Product} -> {item.Price}");
                }


                // 14. Average price of each category
                var averageCategoryPrice = ListGenerators.ProductList
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        AveragePrice = g.Average(p => p.UnitPrice)
                    });

                Console.WriteLine("\n14. Average price:");

                foreach (var item in averageCategoryPrice)
                {
                    Console.WriteLine(
                        $"{item.Category} -> {item.AveragePrice:F2}");
                }


                // =========================================================
                // LINQ - Set Operators
                // =========================================================

                Console.WriteLine("\n===== Set Operators =====");

                // 1. Unique Category names
                var uniqueCategories = ListGenerators.ProductList
                    .Select(p => p.Category)
                    .Distinct();

                Console.WriteLine("\n1. Unique Categories:");

                foreach (var category in uniqueCategories)
                {
                    Console.WriteLine(category);
                }


                // 2. Unique first letters from products and customers
                var productFirstLetters = ListGenerators.ProductList
                    .Select(p => p.ProductName[0]);

                var customerFirstLetters = ListGenerators.CustomerList
                    .Select(c => c.CustomerName[0]);

                var uniqueFirstLetters = productFirstLetters
                    .Union(customerFirstLetters);

                Console.WriteLine("\n2. Unique first letters:");

                foreach (var letter in uniqueFirstLetters)
                {
                    Console.WriteLine(letter);
                }


                // 3. Common first letters
                var commonFirstLetters = productFirstLetters
                    .Intersect(customerFirstLetters);

                Console.WriteLine("\n3. Common first letters:");

                foreach (var letter in commonFirstLetters)
                {
                    Console.WriteLine(letter);
                }


                // 4. Product first letters not in customer names
                var productOnlyLetters = productFirstLetters
                    .Except(customerFirstLetters);

                Console.WriteLine("\n4. Product-only first letters:");

                foreach (var letter in productOnlyLetters)
                {
                    Console.WriteLine(letter);
                }


                // 5. Last three characters of all names
                var lastThreeCharacters = ListGenerators.ProductList
                    .Select(p => p.ProductName.Substring(
                        Math.Max(0, p.ProductName.Length - 3)))
                    .Concat(
                        ListGenerators.CustomerList
                            .Select(c => c.CustomerName.Substring(
                                Math.Max(0, c.CustomerName.Length - 3)))
                    );

                Console.WriteLine("\n5. Last three characters:");

                foreach (var item in lastThreeCharacters)
                {
                    Console.WriteLine(item);
                }


                // =========================================================
                // LINQ - Partitioning Operators
                // =========================================================

                Console.WriteLine("\n===== Partitioning Operators =====");

                // 1. First 3 orders from customers in Washington
                var firstThreeWashingtonOrders = ListGenerators.CustomerList
                    .Where(c => c.City == "Washington")
                    .SelectMany(c => c.Orders)
                    .Take(3);

                Console.WriteLine("\n1. First 3 orders from Washington:");

                foreach (var order in firstThreeWashingtonOrders)
                {
                    Console.WriteLine(order);
                }


                // 2. All but first 2 orders from Washington
                var remainingWashingtonOrders = ListGenerators.CustomerList
                    .Where(c => c.City == "Washington")
                    .SelectMany(c => c.Orders)
                    .Skip(2);

                Console.WriteLine("\n2. All but first 2 orders:");

                foreach (var order in remainingWashingtonOrders)
                {
                    Console.WriteLine(order);
                }


                // 3. Take elements until number < its position
                int[] numbers1 =
                {
                5, 4, 1, 3, 9, 8, 6, 7, 2, 0
            };

                var takeUntil = numbers1
                    .TakeWhile((number, index) => number >= index);

                Console.WriteLine("\n3. Until number is less than its position:");

                foreach (var number in takeUntil)
                {
                    Console.WriteLine(number);
                }


                // 4. Elements starting from first divisible by 3
                int[] numbers2 =
                {
                5, 4, 1, 3, 9, 8, 6, 7, 2, 0
            };

                var fromDivisibleBy3 = numbers2
                    .SkipWhile(n => n % 3 != 0);

                Console.WriteLine("\n4. Starting from first divisible by 3:");

                foreach (var number in fromDivisibleBy3)
                {
                    Console.WriteLine(number);
                }


                // 5. Elements starting from first less than position
                int[] numbers3 =
                {
                5, 4, 1, 3, 9, 8, 6, 7, 2, 0
            };

                var fromLessThanPosition = numbers3
                    .SkipWhile((number, index) => number >= index);

                Console.WriteLine("\n5. Starting from first number less than position:");

                foreach (var number in fromLessThanPosition)
                {
                    Console.WriteLine(number);
                }


                // =========================================================
                // LINQ - Quantifiers
                // =========================================================

                Console.WriteLine("\n===== Quantifiers =====");

                // 1. Any word contains "ei"
                bool containsEi = words.Any(w =>
                    w.Contains("ei", StringComparison.OrdinalIgnoreCase));

                Console.WriteLine("\n1. Any word contains 'ei':");
                Console.WriteLine(containsEi);


                // 2. Categories that have at least one product out of stock
                var categoriesWithOutOfStock =
                    ListGenerators.ProductList
                        .GroupBy(p => p.Category)
                        .Where(g => g.Any(p => p.UnitsInStock == 0));

                Console.WriteLine("\n2. Categories with at least one out-of-stock product:");

                foreach (var group in categoriesWithOutOfStock)
                {
                    Console.WriteLine($"\nCategory: {group.Key}");

                    foreach (var product in group)
                    {
                        Console.WriteLine(product.ProductName);
                    }
                }


                // 3. Categories where all products are in stock
                var categoriesAllInStock =
                    ListGenerators.ProductList
                        .GroupBy(p => p.Category)
                        .Where(g => g.All(p => p.UnitsInStock > 0));

                Console.WriteLine("\n3. Categories where all products are in stock:");

                foreach (var group in categoriesAllInStock)
                {
                    Console.WriteLine($"\nCategory: {group.Key}");

                    foreach (var product in group)
                    {
                        Console.WriteLine(product.ProductName);
                    }
                }


                // =========================================================
                // LINQ - Grouping Operators
                // =========================================================

                Console.WriteLine("\n===== Grouping Operators =====");


                // 1. Group numbers by remainder when divided by 5
                List<int> numbers =
                    new List<int>
                    {
                    0, 1, 2, 3, 4,
                    5, 6, 7, 8, 9,
                    10, 11, 12, 13, 14, 15
                    };

                var groupedNumbers = numbers
                    .GroupBy(n => n % 5);

                Console.WriteLine("\n1. Numbers grouped by remainder:");

                foreach (var group in groupedNumbers)
                {
                    Console.WriteLine($"Remainder {group.Key}:");

                    foreach (var number in group)
                    {
                        Console.WriteLine(number);
                    }
                }


                // 2. Group dictionary words by first letter
                var groupedWords = words
                    .GroupBy(w => w[0]);

                Console.WriteLine("\n2. Words grouped by first letter:");

                foreach (var group in groupedWords)
                {
                    Console.WriteLine($"\nLetter: {group.Key}");

                    foreach (var word in group)
                    {
                        Console.WriteLine(word);
                    }
                }


                // 3. Group words with the same characters
                string[] arr =
                {
                "from",
                "salt",
                "earn",
                "last",
                "near",
                "form"
            };

                var groupedAnagrams = arr
                    .GroupBy(
                        word => word,
                        new AnagramComparer());

                Console.WriteLine("\n3. Words grouped by same characters:");

                foreach (var group in groupedAnagrams)
                {
                    Console.WriteLine(
                        string.Join(", ", group));
                }


                Console.WriteLine("\n===== Finished =====");
                Console.ReadKey();
            }
        }


        // Custom comparer for words containing the same characters
        class AnagramComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                if (x == null || y == null)
                    return x == y;

                return string.Concat(x.OrderBy(c => c))
                    .Equals(string.Concat(y.OrderBy(c => c)),
                        StringComparison.OrdinalIgnoreCase);
            }


            public int GetHashCode(string obj)
            {
                if (obj == null)
                    return 0;

                return string.Concat(
                    obj.ToLower().OrderBy(c => c)
                ).GetHashCode();
            }
        }
    }
}

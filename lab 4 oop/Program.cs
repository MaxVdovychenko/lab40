using System;

namespace StructConlsole
{
    public struct Currency
    {
        public string Name { get; }
        public double ExchangeRate { get; } // UAH per 1 currency unit

        public Currency(string name, double exchangeRate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Currency name is required.", nameof(name));
            if (exchangeRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Exchange rate must be > 0.");

            Name = name;
            ExchangeRate = exchangeRate;
        }
    }

    public struct Product
    {
        public string Name { get; }
        public Currency Cost { get; }
        public int Quantity { get; }
        public string Manufacturer { get; }
        public double Weight { get; }

        public Product(string name, Currency cost, int quantity, string manufacturer, double weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));
            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("Manufacturer is required.", nameof(manufacturer));
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be >= 0.");
            if (weight < 0)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be >= 0.");

            Name = name;
            Cost = cost;
            Quantity = quantity;
            Manufacturer = manufacturer;
            Weight = weight;
        }

        public double GetUnitPriceUAH() => Cost.ExchangeRate;

        public double GetTotalPriceUAH() => Cost.ExchangeRate * Quantity;

        public double GetTotalWeight() => Weight * Quantity;
    }

    class Program
    {
        static void Main()
        {
            Console.Title = "Lab Work #5";

            Product[] products = null;
            bool exit = false;

            while (!exit)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                HandleMenuChoice(choice, ref products, ref exit);
            }
        }

        static void ShowMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("========== MAIN MENU ==========");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Enter product list");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("2. Display all products");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("3. Show cheapest and most expensive product");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("4. Sort products by unit price");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("5. Sort products by quantity");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("6. Exit");
            Console.ResetColor();

            Console.Write("Select an option: ");
        }

        static void HandleMenuChoice(string choice, ref Product[] products, ref bool exit)
        {
            switch (choice)
            {
                case "1":
                    products = ReadProductsArray();
                    Pause();
                    break;

                case "2":
                    if (EnsureProductsLoaded(products)) PrintProducts(products);
                    Pause();
                    break;

                case "3":
                    if (EnsureProductsLoaded(products))
                    {
                        GetProductsInfo(products, out Product cheapest, out Product mostExpensive);
                        Console.WriteLine("--- Cheapest Product ---");
                        PrintProduct(cheapest);
                        Console.WriteLine("--- Most Expensive Product ---");
                        PrintProduct(mostExpensive);
                    }
                    Pause();
                    break;

                case "4":
                    if (EnsureProductsLoaded(products))
                    {
                        SortProductsByPrice(products);
                        Console.WriteLine("Products sorted by unit price.");
                    }
                    Pause();
                    break;

                case "5":
                    if (EnsureProductsLoaded(products))
                    {
                        SortProductsByQuantity(products);
                        Console.WriteLine("Products sorted by quantity.");
                    }
                    Pause();
                    break;

                case "6":
                    exit = true;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection. Try again.");
                    Console.ResetColor();
                    Pause();
                    break;
            }
        }

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        static bool EnsureProductsLoaded(Product[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No products loaded. Please enter products first.");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        // ---------- Input helpers (Issue #2 fix) ----------

        static string ReadNonEmpty(string prompt)
        {
            string s;
            do
            {
                Console.Write(prompt);
                s = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(s));
            return s;
        }

        static int ReadInt(string prompt, int minInclusive)
        {
            int value;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out value) || value < minInclusive);
            return value;
        }

        static double ReadDouble(string prompt, double minExclusive)
        {
            double value;
            do
            {
                Console.Write(prompt);
            } while (!double.TryParse(Console.ReadLine(), out value) || value <= minExclusive);
            return value;
        }

        static double ReadDoubleMinInclusive(string prompt, double minInclusive)
        {
            double value;
            do
            {
                Console.Write(prompt);
            } while (!double.TryParse(Console.ReadLine(), out value) || value < minInclusive);
            return value;
        }

        public static Product[] ReadProductsArray()
        {
            int n = ReadInt("Enter number of products: ", 1);

            Product[] arr = new Product[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"--- Product #{i + 1} ---");

                string name = ReadNonEmpty("Name: ");
                string currencyName = ReadNonEmpty("Currency name: ");

                double rate = ReadDouble("Exchange rate (UAH per unit): ", 0);

                int qty = ReadInt("Quantity: ", 0);

                string manufacturer = ReadNonEmpty("Manufacturer: ");

                double weight = ReadDoubleMinInclusive("Weight (kg): ", 0);

                Currency currency = new Currency(currencyName, rate);
                arr[i] = new Product(name, currency, qty, manufacturer, weight);
            }
            return arr;
        }

        public static void PrintProduct(Product p)
        {
            Console.WriteLine($"Name: {p.Name}");
            Console.WriteLine($"Unit Price (UAH): {p.GetUnitPriceUAH():F2}");
            Console.WriteLine($"Total Price (UAH): {p.GetTotalPriceUAH():F2}");
            Console.WriteLine($"Quantity: {p.Quantity}");
            Console.WriteLine($"Manufacturer: {p.Manufacturer}");
            Console.WriteLine($"Unit Weight (kg): {p.Weight:F2}");
            Console.WriteLine($"Total Weight (kg): {p.GetTotalWeight():F2}");
            Console.WriteLine(new string('-', 40));
        }

        public static void PrintProducts(Product[] arr)
        {
            Console.Clear();
            Console.WriteLine("=== Products List ===");
            foreach (var p in arr)
            {
                PrintProduct(p);
            }
        }

        public static void GetProductsInfo(Product[] arr, out Product min, out Product max)
        {
            min = arr[0];
            max = arr[0];
            foreach (var p in arr)
            {
                if (p.GetUnitPriceUAH() < min.GetUnitPriceUAH()) min = p;
                if (p.GetUnitPriceUAH() > max.GetUnitPriceUAH()) max = p;
            }
        }

        public static int CompareByPrice(Product a, Product b)
        {
            if (a.GetUnitPriceUAH() > b.GetUnitPriceUAH()) return 1;
            if (a.GetUnitPriceUAH() < b.GetUnitPriceUAH()) return -1;
            return 0;
        }

        public static int CompareByQuantity(Product a, Product b)
        {
            if (a.Quantity > b.Quantity) return 1;
            if (a.Quantity < b.Quantity) return -1;
            return 0;
        }

        public static void SortProductsByPrice(Product[] arr) => Array.Sort(arr, CompareByPrice);

        public static void SortProductsByQuantity(Product[] arr) => Array.Sort(arr, CompareByQuantity);
    }
}

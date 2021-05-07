using System;
using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_week1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CompanyAsset> companyAssets = new List<CompanyAsset>();

            while (true)
            {
                Console.WriteLine("Press 1 for insert laptop information, press 2 for insert phone information or q for quite ");
                string input = Console.ReadLine();
                if (input.ToLower() == "q")
                {
                    break; ;
                }
                if (input == "1")
                {
                    Console.Write("Model name: ");
                    string modelName = Console.ReadLine();
                    DateTime dt;
                    while (true)
                    {
                        Console.Write("Purschase date in MM/DD/YYYY format: ");
                        // DateTime purchaseDate = DateTime.Parse(Console.ReadLine());
                        string purchaseDate = Console.ReadLine();
                        var isValidDate = DateTime.TryParse(purchaseDate, out dt);
                        if (isValidDate)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{purchaseDate} is not a valid date string");
                        }
                    }
                    int priceValue;
                    while (true)
                    {
                        Console.Write("Price: ");
                        string price = Console.ReadLine();
                        if (int.TryParse(price, out priceValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Flektigt formatt på price!");
                        }

                    }
                    CompanyAsset companyAsset = new LaptopComputer(priceValue, dt, modelName);
                    companyAssets.Add(companyAsset);
                }

                if (input == "2")
                {
                    Console.Write("Model name: ");
                    string modelName = Console.ReadLine();
                    DateTime dt;
                    while (true)
                    {
                        Console.Write("Purschase date in MM/DD/YYYY format: ");
                        // DateTime purchaseDate = DateTime.Parse(Console.ReadLine());
                        string purchaseDate = Console.ReadLine();
                        var isValidDate = DateTime.TryParse(purchaseDate, out dt);
                        if (isValidDate)
                        {

                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{purchaseDate} is not a valid date string");
                        }
                    }
                    int priceValue;
                    while (true)
                    {
                        Console.Write("Price: ");
                        string price = Console.ReadLine();
                        if (int.TryParse(price, out priceValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Flektigt formatt på price!");
                        }

                    }
                    CompanyAsset companyAsset = new MobilPhones(priceValue, dt, modelName);
                    companyAssets.Add(companyAsset);
                }
            }
            List<CompanyAsset> sortedCompanyAsset = companyAssets.
               OrderBy(companyAsset => companyAsset.GetType().Name).
               ThenBy(companyAsset => companyAsset.PurchaseDate).
               ToList();
            
            foreach (CompanyAsset company in companyAssets)
            {
                DateTime dtNow = DateTime.Now;
                int incresedYear = company.PurchaseDate.Year + 3;
                DateTime expdt = company.PurchaseDate.AddYears(incresedYear - company.PurchaseDate.Year);
                TimeSpan diff = expdt.Subtract(dtNow);
                if (diff.TotalDays < 90)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(company.ModelName.PadRight(10) + "" + company.Price.ToString().PadRight(10) + company.PurchaseDate);
                    Console.ResetColor();
                }
                else if (diff.TotalDays < 180)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(company.ModelName.PadRight(10) + "" + company.Price.ToString().PadRight(10) + company.PurchaseDate);
                    Console.ResetColor();
                }
                
                else
                {
                    Console.WriteLine(company.ModelName.PadRight(10) + "" + company.Price.ToString().PadRight(10) + company.PurchaseDate);
                }
               
            }


            Console.ReadLine();
        }
       
    }
}

public class CompanyAsset
{
    public int Price { set; get; }
    public DateTime PurchaseDate { set; get; }
    public string ModelName { set; get; }


}
public class LaptopComputer : CompanyAsset
{
    public LaptopComputer(int price, DateTime purchaseDate, string modelName)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        ModelName = modelName;
    }
}

public class MobilPhones : CompanyAsset
{
    public MobilPhones(int price, DateTime purchaseDate, string modelName)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        ModelName = modelName;
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mini_Project_Week1
{
    class Program
    {
        static void Main(string[] args)
        {
            API_Obj Test;
            double curren = 0;
            double priceValue = 0;
            string splitCurr;
            List<CompanyAsset> companyAssets = new List<CompanyAsset>();
            String URLString = "https://v6.exchangerate-api.com/v6/aeccd74cdf37986987dc6b47/latest/USD";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URLString);
                Test = JsonConvert.DeserializeObject<API_Obj>(json);
            }

            var nameOfProperty = "conversion_rates";
            var propertyInfo = Test.GetType().GetProperty(nameOfProperty);
            object value = propertyInfo.GetValue(Test, null);

            //Write to file 
            using (StreamWriter sw = File.CreateText("CurrencyCuntry.txt"))
            {
                sw.WriteLine("sweden SEK");
                sw.WriteLine("china CNY");
                sw.WriteLine("poland PLN");
                sw.WriteLine("denemark DKK");
            }

            while (true)
            {
                Console.WriteLine("Press 1 to insert Computers information, press 2 to insert Phone information or q to quite ");
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
                        Console.Write("Purschase date in MM-DD-YYYY format: ");
                        // DateTime purchaseDate = DateTime.Parse(Console.ReadLine());
                        string purchaseDate = Console.ReadLine();
                        var isValidDate = DateTime.TryParse(purchaseDate, out dt);
                        if (isValidDate)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{dt} is not a valid date string");
                        }
                    }

                    while (true)
                    {
                        Console.Write("Price in dollar: ");
                        Console.Write("$");
                        string price = Console.ReadLine();
                        if (double.TryParse(price, out priceValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Flektigt formatt på price!");
                        }

                    }
                    Console.Write("Office Location: ");
                    string office = Console.ReadLine();
                    CompanyAsset ca = new LaptopComputer(priceValue, dt, modelName, office);
                    companyAssets.Add(ca);
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
                            Console.WriteLine($"{dt} is not a valid date string");
                        }
                    }
                    // string cutencyName = "";
                    while (true)
                    {
                        Console.Write("Price in dollar: ");
                        Console.Write("$");
                        string price = Console.ReadLine();
                        if (double.TryParse(price, out priceValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Flektigt formatt på price!");
                        }

                    }
                    Console.Write("Office Location: ");
                    string office = Console.ReadLine();

                    CompanyAsset companyAsset = new MobilPhones(priceValue, dt, modelName, office);
                    companyAssets.Add(companyAsset);
                }

            }

            CompanyAsset mobilPhones = new MobilPhones(400, new DateTime(2018, 8, 30), "Iphone", "China");
            companyAssets.Add(mobilPhones);
            CompanyAsset mobilPhones1 = new MobilPhones(100, new DateTime(2018, 2, 28), "Samsung", "Poland");
            companyAssets.Add(mobilPhones1);
            CompanyAsset mobilPhones2 = new MobilPhones(200, new DateTime(2021, 5, 4), "Nokia", "Denemark");
            companyAssets.Add(mobilPhones2);
            CompanyAsset mobilPhones3 = new LaptopComputer(100, new DateTime(2018, 8, 30), "ASUS", "China");
            companyAssets.Add(mobilPhones3);
            CompanyAsset mobilPhones4 = new LaptopComputer(100, new DateTime(2018, 2, 28), "MacBook", "Poland");
            companyAssets.Add(mobilPhones4);
            CompanyAsset mobilPhones5 = new LaptopComputer(100, new DateTime(2021, 5, 4), "Lenovo", "Denemark");
            companyAssets.Add(mobilPhones5);

            //   LEVEL2
            // List<CompanyAsset> sortedCompanyAsset = companyAssets.
            //   OrderBy(companyAsset => companyAsset.GetType().Name).
            //  ThenBy(companyAsset => companyAsset.PurchaseDate).
            //  ToList();

            //LEVEL3
            List<CompanyAsset> sortedCompanyAsset = companyAssets.
               OrderBy(companyAsset => companyAsset.Office).
               ThenBy(companyAsset => companyAsset.PurchaseDate).
               ToList();
            Console.WriteLine("Office".PadRight(15) + "Model Name".PadRight(15) + "Price".PadRight(20) + "Purchase Date".PadRight(10));
            Console.WriteLine("======================================================================");

            foreach (CompanyAsset company in sortedCompanyAsset)
            {
                var s = File.ReadLines("CurrencyCuntry.txt")
                 .SkipWhile(line => !line.Contains(company.Office.ToLower()));
                string[] split = s.First().Split(" ");
                splitCurr = split[1];

                foreach (var cur in value.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
                {
                    if (cur.Name == splitCurr)
                    {
                        curren = (double)cur.GetValue(value, null);
                    }
                }
                DateTime dt = DateTime.Now;
                int incresedYear = company.PurchaseDate.Year + 3;
                DateTime newdt = company.PurchaseDate.AddYears(incresedYear - company.PurchaseDate.Year);
                TimeSpan diff = newdt.Subtract(dt);


                //LEVEL2
                // if (diff.TotalDays < 90)
                //{
                //    Console.Write(company.GetType().Name.PadRight(20));
                //   Console.BackgroundColor = ConsoleColor.Red;
                //    Console.WriteLine(company.ModelName.PadRight(10) + (company.Price * curren).ToString("0.##") + "  " +
                //      company.Currency.PadRight(10) + company.PurchaseDate);
                //   Console.ResetColor();
                // }
                // else if (diff.TotalDays < 180)
                //{
                //   Console.Write(company.GetType().Name.PadRight(20));
                //  Console.BackgroundColor = ConsoleColor.Yellow;
                //   Console.WriteLine(company.ModelName.PadRight(10) + (company.Price * curren).ToString("0.##")
                //      + "  " + company.Currency.PadRight(10) + company.PurchaseDate);
                //  Console.ResetColor();
                //}
                //else
                //{
                //   Console.ResetColor();
                //   Console.WriteLine(company.GetType().Name.PadRight(20) + company.ModelName.PadRight(10) + (company.Price * curren).ToString("0.##") + "  " + company.Currency.PadRight(10) + company.PurchaseDate);
                // }

                //LEVEL3
                if (diff.TotalDays < 90)
                {
                    Console.Write(company.Office.PadRight(15));
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(company.ModelName.PadRight(15) + (company.Price * curren).ToString("0.##").PadRight(10) +
                        splitCurr.PadRight(10).PadRight(10) + company.PurchaseDate);
                    Console.ResetColor();
                }
                else if (diff.TotalDays < 180)
                {
                    Console.Write(company.Office.PadRight(15));
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(company.ModelName.PadRight(15) + (company.Price * curren).ToString("0.##").PadRight(10)
                        + splitCurr.PadRight(10) + company.PurchaseDate);
                    Console.ResetColor();
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine(company.Office.PadRight(15) + company.ModelName.PadRight(15)
                        + (company.Price * curren).ToString("0.##").PadRight(10)
                        + splitCurr.PadRight(10) + company.PurchaseDate);
                }
            }
            Console.ReadLine();
        }
    }
}


public class CompanyAsset
{
    public double Price { set; get; }
    // public string Currency { set; get; }
    public DateTime PurchaseDate { set; get; }
    public string ModelName { set; get; }
    public string Office { set; get; }


}
public class LaptopComputer : CompanyAsset
{
    public LaptopComputer(double price, DateTime purchaseDate, string modelName, string office)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        ModelName = modelName;
        Office = office;
    }
}

public class MobilPhones : CompanyAsset
{
    public MobilPhones(double price, DateTime purchaseDate, string modelName, string office)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        ModelName = modelName;
        Office = office;
    }
}


public class API_Obj
{
    public string result { get; set; }
    public string documentation { get; set; }
    public string terms_of_use { get; set; }
    public string time_zone { get; set; }
    public string time_last_update { get; set; }
    public string time_next_update { get; set; }
    public ConversionRate conversion_rates { get; set; }
}

public class ConversionRate
{
    public double AED { get; set; }
    public double ARS { get; set; }
    public double AUD { get; set; }
    public double BGN { get; set; }
    public double BRL { get; set; }
    public double BSD { get; set; }
    public double CAD { get; set; }
    public double CHF { get; set; }
    public double CLP { get; set; }
    public double CNY { get; set; }
    public double COP { get; set; }
    public double CZK { get; set; }
    public double DKK { get; set; }
    public double DOP { get; set; }
    public double EGP { get; set; }
    public double EUR { get; set; }
    public double FJD { get; set; }
    public double GBP { get; set; }
    public double GTQ { get; set; }
    public double HKD { get; set; }
    public double HRK { get; set; }
    public double HUF { get; set; }
    public double IDR { get; set; }
    public double ILS { get; set; }
    public double INR { get; set; }
    public double ISK { get; set; }
    public double JPY { get; set; }
    public double KRW { get; set; }
    public double KZT { get; set; }
    public double MXN { get; set; }
    public double MYR { get; set; }
    public double NOK { get; set; }
    public double NZD { get; set; }
    public double PAB { get; set; }
    public double PEN { get; set; }
    public double PHP { get; set; }
    public double PKR { get; set; }
    public double PLN { get; set; }
    public double PYG { get; set; }
    public double RON { get; set; }
    public double RUB { get; set; }
    public double SAR { get; set; }
    public double SEK { get; set; }
    public double SGD { get; set; }
    public double THB { get; set; }
    public double TRY { get; set; }
    public double TWD { get; set; }
    public double UAH { get; set; }
    public double USD { get; set; }
    public double UYU { get; set; }
    public double ZAR { get; set; }
}

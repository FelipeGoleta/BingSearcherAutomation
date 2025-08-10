using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using CrypticWizard.RandomWordGenerator;

const int _wordCount = 90;

var options = new EdgeOptions();
var driverPath = @"C:\Users\Oyiebhoy\Downloads\msedgedriver.exe";
var service = EdgeDriverService.CreateDefaultService(driverPath);

using (var driver = new EdgeDriver(service, options))
{
    var wordGenerator = new WordGenerator();
    var words = wordGenerator.GetWords(_wordCount);
    foreach (var stringQuery in words)  
    {
        Console.WriteLine($"Searching for: {stringQuery}");
        driver.Navigate().GoToUrl("https://www.bing.com");
        await Task.Delay(1000);

        var searchBox = driver.FindElement(By.Name("q"));
        searchBox.Clear();
        searchBox.SendKeys(stringQuery);
        searchBox.SendKeys(Keys.Enter);
        await Task.Delay(1000);
    }
}


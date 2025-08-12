using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using CrypticWizard.RandomWordGenerator;

namespace BingSearcher;

public static class Program
{
    public static async Task Main(string[] args)
    {
        const int _wordCount = 90;

        var options = new EdgeOptions();
        var driverPath = @"C:\Users\Oyiebhoy\Downloads\msedgedriver.exe";
        var service = EdgeDriverService.CreateDefaultService(driverPath);
        var random = new Random();

        using (var driver = new EdgeDriver(service, options))
        {
            var wordGenerator = new WordGenerator();
            var words = wordGenerator.GetWords(_wordCount);
            foreach (var stringQuery in words)
            {
                Console.WriteLine($"Searching for: {stringQuery}");
                driver.Navigate().GoToUrl("https://www.bing.com");
                await Task.CompletedTask.RandomDelay();

                var searchBox = driver.FindElement(By.Name("q"));
                searchBox.Clear();
                searchBox.SendKeys(stringQuery);
                searchBox.SendKeys(Keys.Enter);
                await Task.CompletedTask.RandomDelay();
            }

            driver.Navigate().GoToUrl("https://rewards.bing.com/?form=ML2UYJ");
            await Task.Delay(1000);

            var anchorElements = driver.FindElements(By.TagName("a"));

            foreach (var anchorElement in anchorElements)
            {
                try
                {
                    var href = anchorElement.GetAttribute("href");
                    if (href is null || !href.Contains("https://www.bing.com/search?q="))
                    {
                        continue;
                    }

                    Console.WriteLine($"Found link: {href}");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", anchorElement);
                    await Task.CompletedTask.RandomDelay();

                    anchorElement.Click();
                    await Task.CompletedTask.RandomDelay();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            driver.Close();
        }
    }
}



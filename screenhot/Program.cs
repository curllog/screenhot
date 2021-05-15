using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace screenhot
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please specify website url !");
                return;
            }

            FirefoxOptions firefoxOptions = new FirefoxOptions();
           

            firefoxOptions.AddArgument("--headless");


            using (IWebDriver driver = new FirefoxDriver(firefoxOptions))
            {
                string siteUrl = args[0];
                Console.WriteLine(siteUrl);
                var ct = new CancellationTokenSource();

                try
                {


                    List<Task> tasks = new List<Task>();
                    string directoryName;
                    Screenshot ss;
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    driver.Navigate().GoToUrl(siteUrl);
                    if (Uri.TryCreate(driver.Url, UriKind.Absolute, out Uri uri))
                    {

                        directoryName = uri.Host;

                    }
                    else
                    {
                        directoryName = "Unnamed" + Guid.NewGuid().ToString();
                    }



                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    Int64 siteHeight = (Int64)js.ExecuteScript("return Math.max( document.body.scrollHeight,document.body.offsetHeight,document.documentElement.clientHeight,document.documentElement.scrollHeight,document.documentElement.offsetHeight )");

                    Console.WriteLine("\n\n\n\n\n");
                    Console.Write("Taking shots be patience! ");


                    var token = ct.Token;
                    var loadTask = Task.Factory.StartNew(() =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            Console.Write('#');
                            Thread.Sleep(1000);
                        }

                    }, ct.Token);
                    StreamReader file = new StreamReader("sizes.txt");
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line == "" || line==null)
                        {
                            continue;
                        }
                        driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = Convert.ToInt32(line) };
                        ss = ((ITakesScreenshot)driver).GetScreenshot();
                        tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = line, screen = ss })));

                    }



                    ct.Cancel();

                    Task.WaitAll(tasks.ToArray());
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("\n\n\nScreenshots are saved at {0}{1}screenshots{2}{3}", Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar, Path.DirectorySeparatorChar, directoryName);
                    Console.ResetColor();




                }


                catch (Exception)
                {
                    ct.Cancel();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\nSome Error acquired during process \nmake sure you have fast internet connection and also make sure you have entered a valid url and try again");
                    Console.ResetColor();

                }
            }

        }
    }
}

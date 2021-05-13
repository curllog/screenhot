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

                    var ct = new CancellationTokenSource();

                    var token = ct.Token;
                    var loadTask = Task.Factory.StartNew(() =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            Console.Write('#');
                            Thread.Sleep(1000);
                        }

                    }, ct.Token);

                    /*
                    //1910
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1910 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1920", screen = ss })));


                    //1536
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1536 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1536", screen = ss })));


                    //1440
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1440 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1440", screen = ss })));


                    //1336
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1336 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1336", screen = ss })));


                    //1280
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1280 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1280", screen = ss })));


                    //1080
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1080 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1080", screen = ss })));


                    //1024
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 1024 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();

                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "1024", screen = ss })));


                    //962
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 962 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "962", screen = ss })));

                    //800
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 800 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "800", screen = ss })));


                    //768
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 768 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "768", screen = ss })));


                    //601
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 601 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "601", screen = ss })));


                    //540
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 540 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "540", screen = ss })));



                    //414
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 414 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "414", screen = ss })));


                    //375
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 375 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "375", screen = ss })));


                    //360
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 360 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "360", screen = ss })));


                    //360
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 320 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "320", screen = ss })));



                    //280
                    driver.Manage().Window.Size = new System.Drawing.Size { Height = (int)siteHeight, Width = 280 };
                    ss = ((ITakesScreenshot)driver).GetScreenshot();
                    tasks.Add(Task.Factory.StartNew(() => Saver.Save(new SaveModel { directory = directoryName, title = "280", screen = ss })));


                    */
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\nSome Error acquired during process \nmake sure you have fast internet connection and also make sure you have entered a valid url and try again");
                    Console.ResetColor();

                }
            }

        }
    }
}

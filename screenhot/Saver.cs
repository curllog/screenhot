using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screenhot
{
    class Saver
    {
        public static bool Save(SaveModel s)
        {

                if (!Directory.Exists("screenshots"+ Path.AltDirectorySeparatorChar+s.directory))
                {
                    Directory.CreateDirectory("screenshots" + Path.AltDirectorySeparatorChar + s.directory);
                   
                }
                s.screen.SaveAsFile("screenshots" + Path.DirectorySeparatorChar + s.directory+ Path.DirectorySeparatorChar +s.title+".png");
                return true;
            
        }
    }
}

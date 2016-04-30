using System;
using System.IO;
using System.Reflection;
using Gecko;

namespace Gekkota
{
    public class GeckoFxInitializer
    {   
        public static void SetUpXulRunner()
        {
	        if (Xpcom.IsInitialized)
		        return;
			Xpcom.Initialize("Firefox");
        }

        /// <summary>
        /// Gives the directory of either the project folder (if running from visual studio), or
        /// the installation folder.  Helpful for finding templates and things; by using this,
        /// you don't have to copy those files into the build directory during development.
        /// It assumes your build directory has "output" as part of its path.
        /// </summary>
        /// <returns></returns>
        public static string DirectoryOfApplicationOrSolution
        {
            get
            {
                string path = DirectoryOfTheApplicationExecutable;
                char sep = Path.DirectorySeparatorChar;
                int i = path.ToLower().LastIndexOf(sep + "output" + sep);

                if (i > -1)
                {
                    path = path.Substring(0, i + 1);
                }
                return path;
            }
        }

        public static string DirectoryOfTheApplicationExecutable
        {
            get
            {
                string path;
                bool unitTesting = Assembly.GetEntryAssembly() == null;
                if (unitTesting)
                {
                    path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
                    path = Uri.UnescapeDataString(path);
                }
                else
                {
                    path = Assembly.GetEntryAssembly().Location;
                }
                return Directory.GetParent(path).FullName;
            }
        }
    }  
}
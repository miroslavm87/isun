using System.Text;

namespace ISun.CommandLine
{
    public class CitiesArgumentExtractor
    {
        private const string _citiesArgument = "--cities";
        public List<string> Cities { get; set; }
        public CitiesArgumentExtractor()
        {
            var commandLineArguments = Environment.GetCommandLineArgs();
            Cities = ExtractCitiesListFromArguments(commandLineArguments).ToList();
        }

        /// <summary>
        /// Extract from arguments cities list
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IEnumerable<string> ExtractCitiesListFromArguments(string[] args)
        {
            if (args.Length == 0) return new string[] { };

            int citiesStartIndex = -1;
            StringBuilder stringBuilder = new StringBuilder();
            for (int currentIndex = 0; currentIndex < args.Length; currentIndex++)
            {
                if (citiesStartIndex > -1 && args[currentIndex].StartsWith("-")) break;
                if (args[currentIndex] == _citiesArgument)
                {
                    citiesStartIndex = currentIndex;
                    continue;
                }

                if (citiesStartIndex > -1)
                {
                    stringBuilder.Append(args[currentIndex]);
                }
            }
            if (stringBuilder.Length == 0) return new string[] { };

            return stringBuilder.ToString().Split(',').Select(city => city.Trim());
        }
    }
}

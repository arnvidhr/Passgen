namespace Passgen
{
    internal class Program
    {
        #region Const
        const char C_TITLE_FILLER = '*';
        readonly static string STR_P = new string(C_TITLE_FILLER, 2);
        const string STR_APP_NAME = "Passgen";

        #endregion

        #region Properties
        static string CopyrightYear
        {
            get
            {
                var retVal = "2024";

                if (DateTime.Now.Year < 2024)
                    retVal += "-" + DateTime.Now.Year;

                return retVal;
            }
        }

        #endregion

        #region Entry
        static void Main(string[] args)
        {
            AppStart();

            var gen = new RandomPasswordGenerator(args);
            if(gen.Validate())
            {
                Console.WriteLine();
                Console.WriteLine($"Generated passwords below:");
                Console.WriteLine();

                var list = gen.Generate();

                foreach (var item in list)
                {
                    Console.WriteLine($"{item}");
                }

                Console.WriteLine();
            }
            else
            {
                AppUsage();
            }

            AppEnd();
        }

        #endregion

        #region Helpers

        static void AppStart()
        {
            Console.WriteLine(new string(C_TITLE_FILLER, 80));
            Console.WriteLine($"{STR_P} Passgen (C) {CopyrightYear} Arvydas Grigonis");
            Console.WriteLine(new string(C_TITLE_FILLER, 80));
        }
        
        static void AppEnd()
        {
            Console.WriteLine("Read Any Key To Continue..");
            Console.ReadKey();
        }

        static void AppUsage()
        {
            Console.WriteLine();
            Console.WriteLine($"{STR_P} Usage: {STR_APP_NAME} [number of passwords] [length] [used symbols]");
            Console.WriteLine($"{STR_P}");
            Console.WriteLine($"{STR_P} Where used sybols can be:");
            Console.WriteLine($"{STR_P} \td - digits");
            Console.WriteLine($"{STR_P} \tu - upper case letters");
            Console.WriteLine($"{STR_P} \tl - lower case letters");
            Console.WriteLine($"{STR_P} \ts - symbols");
            Console.WriteLine($"{STR_P}");
        }

        #endregion
    }
}
using System;
using System.Threading.Tasks;

namespace GetToken
{
    class Program
    {
        static ConsoleColor defaultColor;

        static async Task Main(string[] args)
        {
            defaultColor = Console.ForegroundColor;

            Console.Write("username: ");
            var username = Console.ReadLine();

            Console.Write("password: ");
            var password = ConsoleUtils.ReadPassword();
            Console.WriteLine();

            try
            {
                var token = "";

                using (new LoadingIndicator().Show())
                {
                  token = await TokenService.GetTokenAsync(username, password);
                }

                PrintToken(token);
                CopyToken(token);

                while (true)
                {
                    Console.ForegroundColor = defaultColor;
                    Console.WriteLine();
                    Console.WriteLine(" > Enter to copy to clipboard");
                    Console.WriteLine(" > Ctrl + R to refresh");

                    var pressedKey = Console.ReadKey();

                    var ctrlR = (pressedKey.Modifiers & ConsoleModifiers.Control) != 0
                        && pressedKey.Key == ConsoleKey.R;

                    if (ctrlR)
                    {
                        Console.Write("\b \b");

                        using (new LoadingIndicator().Show())
                        {
                            token = await TokenService.GetTokenAsync(username, password);
                        }

                        PrintToken(token);
                        CopyToken(token);
                    }

                    if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        CopyToken(token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            password.Dispose();

            Console.ReadLine();
        }

        static void PrintToken(string token)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(token);
        }

        static void CopyToken(string token)
        {
            Clipboard.Copy(token);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Copied to clipboard");
        }
    }
}

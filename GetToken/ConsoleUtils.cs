using System;
using System.Security;

namespace GetToken
{
    public static class ConsoleUtils
    {
        public static SecureString ReadPassword()
        {
            var password = new SecureString();

            while (true)
            {
                var pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.AppendChar(pressedKey.KeyChar);
                    Console.Write("*");
                }
            }

            password.MakeReadOnly();

            return password;
        }
    }
}

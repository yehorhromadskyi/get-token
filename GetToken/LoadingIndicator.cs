using System;
using System.Threading.Tasks;

namespace GetToken
{
    public class LoadingIndicator : IDisposable
    {
        bool running = true;

        public LoadingIndicator()
        {
        }

        public IDisposable Show()
        {
            Task.Run(async () =>
            {
                int i = 0;

                while (running)
                {
                    Console.Write(".");

                    await Task.Delay(150);

                    if (++i == 5)
                    {
                        ClearCurrentLine();
                        i = 0;
                    }
                }
            });

            return this;
        }

        public void Dispose()
        {
            running = false;

            ClearCurrentLine();
        }

        void ClearCurrentLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}

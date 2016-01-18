using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static EventWaitHandle ewhOperacje = new AutoResetEvent(false); //utworzenie obiektów klasy AutoResetEvent umożliwia wątkom komunikowanie się poprzez wysyłanie sygnałów
        private static EventWaitHandle ewhZapis = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Thread pobranie = new Thread(() =>
                {
                    for(int i=0;i<5;i++)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("Pobieranie Danych"); // pętla for która wypisuje nam 5 razy Pobieranie Danych
                    }
                    ewhOperacje.Set(); //Metoda która najpierw jest blokowana a potem wywoływana 
                });
            Thread operacje = new Thread(() =>
                {
                    ewhOperacje.WaitOne(); //Wątek oczekujący na sygnał
                    for(int i=0; i<4;i++)
                    {
                        Thread.Sleep(250);
                        Console.WriteLine("Przetwarzanie Danych"); //Pętla for która wypisuje nam 4 razy przetwarzanie danych
                    }
                    ewhZapis.Set(); //Metoda która najpierw jest blokowana a potem wywoływana 
                });
            Thread zapis = new Thread(() =>
                {
                    ewhZapis.WaitOne(); //Wątek oczekujący na sygnał
                    for(int i=0;i<8;i++)
                    {
                        Thread.Sleep(150);
                        Console.WriteLine("Zapis Danych"); //Pętla for która wypisuje nam 8 razy Zapis danych
                    }
                });
            pobranie.Start(); // Wywołanie wątku pobieranie
            operacje.Start(); // Wywołanie wątku operacje
            zapis.Start();  //Wywołanie wątku zapis
        }
    }
}

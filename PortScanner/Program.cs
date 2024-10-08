using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace PortScanner
{
    class Program
    {
        private static string Ip = "";
        private static int prin = 0;
        private static int fin = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Escaner de puertos");
            Console.WriteLine("Ingrese una direccion IP:");
            Direccion();
            Console.WriteLine("Ingrese un rango de puertos");
            Console.WriteLine("Puerto de Inicio:");
            if (validar(Console.ReadLine(), 1)==false)
            {
                while (true)
                {
                    Console.WriteLine("Los puertos solo pueden ser numericos");
                    if(validar(Console.ReadLine(), 1) == true)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Puerto Final:");
            if (validar(Console.ReadLine(), 2) == false)
            {
                while (true)
                {
                    Console.WriteLine("Los puertos solo pueden ser numericos");
                    if (validar(Console.ReadLine(), 2) == true)
                    {
                        break;
                    }
                }
            }
            Scaneo(prin, fin);
            Console.WriteLine("------------------------------------------------------", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("Presione cualquier letra para cerrar el programa", Console.ForegroundColor=ConsoleColor.White);
            Console.ReadKey();
        }

        private static Boolean validar(string r, int op)
        {
            if (op == 1)
            {
                if (!int.TryParse(r, out int puerto))
                {
                    return false;
                }
                else
                {
                    prin = puerto;
                    return true;
                }
            }
            else
            {
                if (!int.TryParse(r, out int puertoss))
                {
                    return false;
                }
                else
                {
                    fin = puertoss;
                    return true;
                }
            }
        }
        private static void Direccion()
        {
            while (true)
            {
                Console.WriteLine("Direccion IP: ");
                Ip = Console.ReadLine();
                if (Ip != "" && Ip !=null)
                {
                    if(IPAddress.TryParse(Ip, out _)==true)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("La direccion Ip no es valida, revise si cumple con el formato o si tiene letras");
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese la direccion IP");
                }
            }
        }
        private static void Scaneo(int prin, int fin)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(Ip);
            Console.WriteLine("------------------------------------------------------");
            using (TcpClient Scan= new TcpClient())
            {
                for(int i = prin; i <= fin; i++)
                {
                    try
                    {
                        Scan.Connect(Ip, i);
                        Thread.Sleep(100);
                        Console.WriteLine($"[{i}] | Abierto", Console.ForegroundColor = ConsoleColor.Green);
                    }
                    catch
                    {
                        Thread.Sleep(100);
                        Console.WriteLine($"[{i}] | Cerrado", Console.ForegroundColor = ConsoleColor.Red);
                    }
                }
            }
        }
    }
}

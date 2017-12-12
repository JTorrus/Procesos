using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace escriure_lletra
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lletra
            string lletra = args[0];

            //Núm vegades
            int num = Convert.ToInt32(args[1]);

            //Velocitat
            int vel = Convert.ToInt32(args[2]);

            for (int i=0; i<num;i++)
            {
                Console.Write(lletra);
                Thread.Sleep(vel);
            }
        }
    }
}

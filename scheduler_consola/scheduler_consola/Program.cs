using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace scheduler_consola
{
    class Program
    {
        //Caldrà definir una variable de classe que serà la cua de processos
        static Queue<Process> cuaProcessos = new Queue<Process>();

        static void Main()
        {
            string opcio = "";

            //Cal crear i iniciar un fil que farà la tasca de planificador de processos
            Thread planificador = new Thread(generar_proces);
            planificador.Start();

            while (opcio != "q")
            {
                Console.WriteLine("Que vols fer crear nou proces (c), sortir(q)?");
                opcio = Console.ReadLine();

                if (opcio == "c")
                {
                    generar_proces();
                }
            }
        }

        private static void generar_proces()
        {
            string lletra;
            int vegades;
            int retard;

            Console.WriteLine("Escriu lletra:");
            lletra = Console.ReadLine();
            Console.WriteLine("Escriu nombre vegades:");
            vegades = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Escriu retard (en ms.):");
            retard = Convert.ToInt32(Console.ReadLine());
            
            //Aqui cal fer el següent:
            //1) Crear un nou process d'EscriureLletra amb les dades que ens ha passat l'usuari com a arguments 
            //2) Guardar el procés a la cua

            Process process = new Process();
            process.StartInfo.FileName = @"C:\Users\Alumne\Documents\Visual Studio 2013\Projects\Pr4 - alumne\esciure_lletra\escriure_lletra\escriure_lletra\bin\Debug\escriure_lletra.exe";
            process.StartInfo.Arguments = lletra + Convert.ToString(vegades) + Convert.ToString(retard);

            cuaProcessos.Enqueue(process);
        }
    }
}

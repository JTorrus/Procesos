﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
namespace scheduler_consola
{
    class Program
    {
        //Caldrà definir una variable de classe que serà la cua de processos
        private static readonly Queue<Process> CuaProcessos = new Queue<Process>();
        private static readonly SemaphoreSlim Semafor = new SemaphoreSlim(2);
        private static readonly SemaphoreSlim SemaforControlarCua = new SemaphoreSlim(0);

        static void Main()
        {
            string opcio = "";

            //Cal crear i iniciar un fil que farà la tasca de planificador de processos
            Thread planificador = new Thread(planificador_proces);
            planificador.Start();

            while (opcio != "q")
            {
                Console.WriteLine("Que vols fer crear nou proces (c), sortir(q)?");
                opcio = Console.ReadLine();

                if (opcio == "c")
                {
                    generar_proces();
                }

                if (opcio == "q")
                {
                    Environment.Exit(1);
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
            // RECORDATORIO: Cambiar ruta
            process.StartInfo.FileName = @"C:\Users\Usuari1\Desktop\Procesos\esciure_lletra\escriure_lletra\escriure_lletra\bin\Debug\escriure_lletra.exe";
            process.StartInfo.Arguments = lletra + " " + Convert.ToString(vegades) + " " + Convert.ToString(retard);
            process.Exited += new EventHandler(process_finalitzat);
            process.EnableRaisingEvents = true;

            CuaProcessos.Enqueue(process);
            SemaforControlarCua.Release();
        }
        
        private static void planificador_proces()
        {
            Process auxProc;

            while (true)
            {
                SemaforControlarCua.Wait();
                if (CuaProcessos.Count > 0)
                { 
                    auxProc = CuaProcessos.Dequeue();
                    Semafor.Wait();
                    auxProc.Start();
                }
            }
        }

        private static void process_finalitzat(object sender, EventArgs eventArgs)
        {
            Semafor.Release();
        }
    }
}

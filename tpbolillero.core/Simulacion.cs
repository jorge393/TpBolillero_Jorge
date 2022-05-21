using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace tpbolillero.core
{
    public class Simulacion
    {
        public long SimularSinHilos(Bolillero bolillero,List<byte> jugadas, long num2)
        {
            return bolillero.JugarN(jugadas,num2);
        }
        public long SimularConHilos(Bolillero bolillero,long numtareas,List<byte> jugadas, long hilosenuso)
        {
            
            Task<long>[] tareas = new Task<long>[hilosenuso];
            long suma = 0; 
            for (int i = 1; i < numtareas; i++)
            {
                Bolillero clon = (Bolillero)bolillero.Clone();
                tareas[i] = Task<long>.Run(() => SimularSinHilos(clon, jugadas, numtareas));
                Task<long>.WaitAll(tareas);
                tareas.Sum(suma + tareas.Result);
            }
            return 1;
            
        }

    }
}
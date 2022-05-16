using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tpbolillero.core
{
    public class Simulacion
    {
        public long SimularSinHilos(Bolillero bolillero,List<byte> jugadas, long num2)
        {
            return bolillero.JugarN(jugadas,num2);
        }
        public long SimularConHilos(Bolillero bolillero,long numtareas,List<byte> jugadas, long num2)
        {
            
            Task<long>[] tareas = new Task<long>[numtareas];
            for (int i = 1; i < num2; i++)
            {
                
            }

            return 1;
        }

    }
}
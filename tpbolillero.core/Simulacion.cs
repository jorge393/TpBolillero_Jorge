using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace tpbolillero.core
{
    public class Simulacion
    {
        public long SimularSinHilos(Bolillero bolillero, List<byte> jugadas, long num2)
        {
            return bolillero.JugarN(jugadas, num2);
        }
        public long SimularConHilos(Bolillero bolillero, long numtareas, List<byte> jugadas, long hilosenuso)
        {

            Task<long>[] tareas = new Task<long>[hilosenuso];
            var divdehilos = numtareas / hilosenuso;
            for (int i = 1; i < hilosenuso; i++)
            {
                for (int b = 0; b < divdehilos; b++)
                {
                    Bolillero clon = (Bolillero)bolillero.Clone();
                    tareas[i] = Task<long>.Run(() => SimularSinHilos(clon, jugadas, numtareas));

                }
            }

            Task<long>.WaitAll(tareas);

            return tareas.Sum(x => x.Result);

        }

        public async Task<long> simularConHilosAsync(Bolillero bolillero, List<byte> jugada, long cantidadSimulaciones, long cantidadHilos)
        {
            long SimulacionesPorHilo = cantidadSimulaciones / cantidadHilos;

            Task<long>[] tareas = new Task<long>[cantidadHilos];

            for (long i = 0; i < cantidadHilos; i++)
            {
                Bolillero bolilleroClon = (Bolillero)bolillero.Clone();
                tareas[i] = Task<long>.Run(() => SimularSinHilos(bolilleroClon, jugada, SimulacionesPorHilo));
            }
            await Task<long>.WhenAll(tareas);
            return tareas.Sum(x => x.Result);

        }
        public async Task<long> SimularBollileroAsyncParallel(Bolillero bolillero, long numtareas, List<byte> jugadas, long hilosenuso)
        {
            var tareas = new long[hilosenuso];

            await Task.Run(() =>
                Parallel.For(0,
                            hilosenuso,
                            i =>
                                {
                                    var clon = (Bolillero)bolillero.Clone();
                                    tareas[i] = SimularSinHilos(clon, jugadas, numtareas / hilosenuso);
                                }
                    )
            );

            return tareas.Sum();
        }

    }
}
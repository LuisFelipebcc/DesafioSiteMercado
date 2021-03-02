using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMercado.Desafio.Utils.Services
{
    public class Periodo
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Periodo(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public static bool DoesNotOverlap(List<Periodo> peridodos)
        {
            DateTime endPrior = DateTime.MinValue;
            foreach (Periodo peridodo in peridodos.OrderBy(x => x.Start))
            {
                if (peridodo.Start > peridodo.End)
                    return false;
                if (peridodo.Start < endPrior)
                    return false;
                endPrior = peridodo.End;
            }

            return true;
        }
    }


}

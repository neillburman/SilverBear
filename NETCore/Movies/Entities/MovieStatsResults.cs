using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Entities
{
    public class MovieStatsResults
    {
        public int MovieId { get; set; }
        public int MovieWatches { get; set; }
        public int Total { get; set; }
        public int Average { get; internal set; }
    }
}

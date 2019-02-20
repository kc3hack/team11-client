using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ikubaku.Scripts.Rankings
{
    class RankingsMeta
    {
        public int current_page { get; set; }
        public string from { get; set; }
        public int last_page { get; set; }
        public int path { get; set; }
        public int per_page { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }
}

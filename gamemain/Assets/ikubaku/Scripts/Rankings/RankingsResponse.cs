using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ikubaku.Scripts.Rankings
{
    class RankingsResponse
    {
        public ScoreEntry[] data { get; set; }
        public RankingsLinks links { get; set; }
        public RankingsMeta meta { get; set; }
    }
}

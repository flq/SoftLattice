using System.Collections.Generic;
using SoftLattice.Common;
using System.Linq;
using MemBus.Support;

namespace SoftLattice.Core.Common
{
    public class StartupRunner
    {
        private List<IGrouping<StartupPhase, IStartup>> groupedStartups;

        public StartupRunner(IEnumerable<IStartup> startups)
        {
            groupedStartups = startups.GroupBy(s => s.StartupPhaseInWhichToRun).ToList();
        }

        public void Run(params StartupPhase[] phases)
        {
            foreach (var phase in phases)
            {
                var group = groupedStartups.FirstOrDefault(grp => grp.Key == phase); // OK since enumeration happens straight away
                if (group != null)
                  group.Each(s=>s.Start());
            }
        }
    }
}
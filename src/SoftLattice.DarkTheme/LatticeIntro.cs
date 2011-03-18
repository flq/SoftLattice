using System;
using SoftLattice.Common;

namespace SoftLattice.DarkTheme
{
    public class LatticeIntro : ILatticeGroup
    {
        public void Access(ILatticeWiring wiring)
        {
            wiring.AddResource("Theme.xaml");
        }
    }
}
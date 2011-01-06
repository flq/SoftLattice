namespace SoftLattice.Common
{
    /// <summary>
    /// Implement this interface with a public class to get access to the SoftwareLattice
    /// </summary>
    public interface ILatticeGroup
    {
        void Access(ILatticeWiring wiring);
    }
}
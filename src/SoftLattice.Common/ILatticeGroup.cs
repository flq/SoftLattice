namespace SoftLattice.Common
{
    /// <summary>
    /// Implement this interface with a public class to get access to the SoftwareLattice.
    /// When SoftLattice starts up, it will look for Assemblies that contain the name LatticeGroup
    /// and  then for public classes implementing this interface 
    /// </summary>
    public interface ILatticeGroup
    {
        void Access(ILatticeWiring wiring);
    }
}
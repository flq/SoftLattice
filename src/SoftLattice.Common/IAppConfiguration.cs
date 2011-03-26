namespace SoftLattice.Common
{
    public interface IAppConfiguration
    {
        T GetSection<T>();
    }
}
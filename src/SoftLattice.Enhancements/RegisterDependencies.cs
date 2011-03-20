using StructureMap.Configuration.DSL;

namespace SoftLattice.Enhancements
{
    public class RegisterDependencies : Registry
    {
        public RegisterDependencies()
        {
            For<object>().Use<InteractionsContainer>().Named("InteractionsContainer");
        }
    }
}
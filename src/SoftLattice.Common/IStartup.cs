namespace SoftLattice.Common
{
    /// <summary>
    /// Implement this interface if you want to get something done just before the root view is loaded and shown.
    /// </summary>
    public interface IStartup
    {
        void Start();
        StartupPhase StartupPhaseInWhichToRun { get; }
    }

    /// <summary>
    /// State in which pahse of the startup you want to run
    /// </summary>
    public enum StartupPhase
    {
        /// <summary>
        /// For startups that have no precondition and just set something up regardless of any other setups
        /// </summary>
        IndependentInfrastructure,
        /// <summary>
        /// Startups that require all plugins to have been registered
        /// </summary>
        AllPluginsKnown,
        /// <summary>
        /// Startups that require the View system to be up and running
        /// </summary>
        UIShown
    }
}
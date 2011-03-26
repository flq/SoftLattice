namespace SoftLattice.PluginC
{
    public class Item2ViewModel
    {
        private readonly ConfigValues _values;

        public Item2ViewModel(ConfigValues values)
        {
            _values = values;
        }

        public string Message { get { return _values.SoulSource; } }
    }
}
namespace VT.Observer
{
    public static class ObserverSystem
    {
        internal static VarRegistry Vars { get; private set; } = null;
        internal static ObserverRegistry Observers { get; private set; } = null;

        public static void Initialize()
        {
            Vars = new VarRegistry();
            Observers = new ObserverRegistry(Vars);
        }
    }
}
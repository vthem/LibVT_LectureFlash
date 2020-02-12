namespace VT.Observer
{
    public static class ObserverSystem
    {
        internal static VarRegistry Vars { get; private set; } = new VarRegistry();
        internal static ObserverRegistry Observers { get; private set; } = new ObserverRegistry(Vars);
    }
}
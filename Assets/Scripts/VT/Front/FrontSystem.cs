namespace VT.Front
{
    public static class FrontSystem
    {
        internal static Logger Logger = new Logger();

        internal static Registry Registry { get; private set; } = new Registry();

        public static void Initialize()
        {
            Registry = new Registry();
        }
    }
}

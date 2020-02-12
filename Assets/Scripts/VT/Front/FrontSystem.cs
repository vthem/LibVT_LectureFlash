namespace VT.Front
{
    public static class FrontSystem
    {
        internal static Logger Logger = new Logger();

        internal static Registry Registry { get; private set; } = new Registry();

        internal static Collection.Registry<string, FrontObject> Objects => Registry.Objects;
        internal static Collection.Registry<string, FrontObjectComponent> Components => Registry.Components;

        public static void Initialize()
        {
            Registry = new Registry();
        }
    }
}

using System;
using _Logger = VT.Core.Logger;

namespace VT.Observer
{
    public class Logger : _Logger
    {
        public override string Name => nameof(VT.Observer);
    }


    public static class ObserverSystem
    {
        internal static VarRegistry Vars { get; set; } = new VarRegistry();
        internal static ObserverRegistry Observers { get; set; } = new ObserverRegistry(Vars);
        internal static Logger Logger = new Logger();
    }
}
namespace VT.Observer
{
    internal interface IObserver
    {
        string Name { get; }
        string VarName { get; }
        void VarUpdatedHandler(IObservable observable);
    }
}
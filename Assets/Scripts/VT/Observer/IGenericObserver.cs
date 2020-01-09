namespace VT.Observer
{
    internal interface IGenericObserver<T> : IObserver
    {
        void VarUpdatedHandler(IGenericObservable<T> observable);
    }
}
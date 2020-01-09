namespace VT.Observer
{
    public interface IGenericObservable<T> : IObservable
    {
        new T Value { get; }
    }
}
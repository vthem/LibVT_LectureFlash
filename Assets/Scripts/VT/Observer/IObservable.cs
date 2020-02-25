namespace VT.Observer
{
    public interface IObservable
    {
        object Value { get; }
        string Name { get; }
        string TypeString { get; }
    }
}
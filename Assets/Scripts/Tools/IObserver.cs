public interface IObserver
{
    public void AddSubscriber(ISubscriber subscriber);
    public void RemoveSubscriber(ISubscriber subscriber);
    public void Notify();
}


public interface ISubscriber
{
    public void React();
}

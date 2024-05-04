using System;
using System.Threading.Tasks;

public class AsyncTimer
{
    public event Action OnTimeOut;

    private float targetTime;

    public AsyncTimer(float targetTime)
    {
        this.targetTime = targetTime;
    }

    public async void Start()
    {
        await Task.Delay((int)(targetTime * 1000));

        OnTimeOut?.Invoke();
    }
}
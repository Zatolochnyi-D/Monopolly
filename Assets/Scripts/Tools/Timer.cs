using System;
using UnityEngine;

public class Timer
{
    public event Action TimeOut;

    private float targetTime;
    private float currentTime;

    public Timer(float targetTime)
    {
        this.targetTime = targetTime;
        currentTime = 0f;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= targetTime)
        {
            TimeOut?.Invoke();
            currentTime = 0f;
        }
    }
}


public class CooldownTimer : Timer
{
    private bool enabled;

    public bool Enabled { get => enabled; set => enabled = value; }

    public CooldownTimer(float targetTime) : base(targetTime)
    {
        enabled = false;
        TimeOut += () => enabled = false;
    }

    public new void Update()
    {
        if (enabled)
        {
            base.Update();
        }
    }
}


public class AsyncTimer : Timer
{
    public AsyncTimer(float targetTime) : base(targetTime)
    { }

    public void Start()
    {
        
    }
}
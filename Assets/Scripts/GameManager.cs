using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Daytime
{
    Dawn,
    Dusk,
    Noon,
    Midnight
}

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private float dayLength;
    public float DayLength => dayLength;

    [SerializeField] private float startTime;

    private float currentTime;  // a single source of truth for the game time
    public float CurrentTime => currentTime;

    private float speedup = 1;
    
    
    // Delegates for event handling
    
    // are called every frame and take the current time as a parameter
    private Action<float> perFrame;     
    // TODO: priority-queue esque
    private Action atDawn;
    private Action atDusk;
    private Action atNoon;
    private Action atMidnight;

    private void EnsureSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Awake()
    {
        EnsureSingleton();
    }

    private void Start()
    {
        currentTime = startTime;
    }

    private void Update()
    {
        currentTime += Time.deltaTime * speedup;
        perFrame?.Invoke(currentTime);
    }

    // using methods instead of public-ing the event
    // to prevent other classes from being able to null-out the 
    // events, or similar problems
    public void SubscribeToPerFrame(Action<float> action)
    {
        perFrame += action;
    }

    public void UnsubscribeFromPerFrame(Action<float> action)
    {
        perFrame -= action;
    }
    
}

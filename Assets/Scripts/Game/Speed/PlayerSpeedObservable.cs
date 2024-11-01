using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedObservable : MonoBehaviour, ISpeedObservable
{
    [Header("Player")]
    [SerializeField]
    private GameObject _playerObject;

    [Header("Observers")]
    [SerializeField]
    private GameObject _speedometer;

    private float _previousSpeed;
    private float _currentSpeed;
    private PlayerControl _playerControl;
    private List<ISpeedObserver> _observers;

    private void Awake()
    {
        InitPlayerControl();
        InitObservers();
        ForceNotifyObservers();
    }

    private void InitPlayerControl()
    {
        try
        {
            _playerControl = _playerObject.GetComponent<PlayerControl>();
        }
        catch
        {
            Debug.LogError("Cannot get PlayerControl component from PlayerObject");
        }
    }

    private void InitObservers()
    {
        _observers = new();

        var speedometer = _speedometer.GetComponent<SpeedometerObserver>();

        AddObserver(speedometer);
    }

    private void FixedUpdate()
    {
        _currentSpeed = _playerControl.CurrentSpeed;
        NotifyObservers();
    }

    public void AddObserver(ISpeedObserver o) => _observers.Add(o);
    public void RemoveObserver(ISpeedObserver o) => _observers.Remove(o);


    public void NotifyObservers()
    {
        if (_previousSpeed != _currentSpeed)
            ForceNotifyObservers();
    }

    private void ForceNotifyObservers()
    {
        _previousSpeed = _currentSpeed;
        var result = ConvertSpeed(_currentSpeed);

        foreach (var o in _observers)
            o.UpdateObserver(result);
    }

    private (int, string) ConvertSpeed(float speedInMeters)
    {
        speedInMeters *= 3600;
        if (speedInMeters <= (int)SpeedThreshold.m)
        {
            return ((int)speedInMeters, SpeedThreshold.m.ToString());
        }
        else if (speedInMeters <= (int)SpeedThreshold.km)
        {
            return ((int)(speedInMeters * 0.001), SpeedThreshold.km.ToString());
        }
        else
        {
            return ((int)(speedInMeters * 6.6846e-12), SpeedThreshold.au.ToString());
        }
    }

    private enum SpeedThreshold
    {
        m = 999,
        km = 99999,
        au,
    }
}

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

    private void Start()
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

    private (int, string) ConvertSpeed(float metersPerSecond)
    {
        int metersPerHour = (int)(metersPerSecond *= 3600);
        if (metersPerHour <= (int)SpeedThreshold.m)
        {
            return (metersPerHour, SpeedThreshold.m.ToString());
        }
        else if (metersPerHour <= (int)SpeedThreshold.km)
        {
            int kmPerHour = (int)(metersPerHour * 0.001);
            return (kmPerHour, SpeedThreshold.km.ToString());
        }
        else
        {
            int auPerHour = (int)(metersPerHour * 6.6846e-12);
            return (auPerHour, SpeedThreshold.au.ToString());
        }
    }

    private enum SpeedThreshold
    {
        m = 999,
        km = 99999,
        au,
    }
}

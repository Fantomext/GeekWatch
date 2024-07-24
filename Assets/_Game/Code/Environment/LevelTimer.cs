using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Audio;

public class LevelTimer : StaticInstance<LevelTimer>
{
    [SerializeField] float _levelTime = 60f;
    public float currentTime;

    public event Action OnTimeOut;

    private bool last10;

    void Start()
    {
        currentTime = _levelTime;
        last10 = false;
    }

    private void Update()
    {
        currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0, _levelTime);
        if(currentTime == 0)
        {
            TimesOut();
        }
        if(currentTime <= 10 && !last10)
        {
            last10 = true;
            AudioBox.Instance.Play("Last10");
        }
    }



    public float GetAmount() => Mathf.Clamp(currentTime / _levelTime, 0, 1f);

    public void TimesOut()
    {
        //OnTimeOut?.Invoke();
        FindAnyObjectByType<LocationReturn>().LoadLocation();
        print("Level End, time is out");
        Destroy(this);
    }
}

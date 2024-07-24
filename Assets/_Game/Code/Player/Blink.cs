using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] private float _timer;


    public void StartBlink(float timer)
    {
        _timer = timer;
        StartCoroutine(BlinkScreen());
    }

    IEnumerator BlinkScreen()
    {
        for (float t = 0; t < _timer; t += Time.deltaTime)
        {
            float value = t / _timer;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, value);
            yield return null;
        }
        for (float t = _timer; t > 0f; t -= Time.deltaTime)
        {
            float value = t / _timer;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, value);
            yield return null;
        }
    }
}

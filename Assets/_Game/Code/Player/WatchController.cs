using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchController : MonoBehaviour
{
    LevelTimer timer;

    [SerializeField]
    Image counterImage;

    void Start()
    {
        timer = LevelTimer.Instance;
    }

   
    void Update()
    {
        counterImage.fillAmount = timer.GetAmount();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animatorLeft;
    [SerializeField] private Animator _animatorRight;

    private readonly string snap = "Snap";
    private readonly string take = "Taking";
    private readonly string drop = "Droping";


    private void Start()
    {
        if (PlayerPrefs.HasKey("watch"))
        {
            if (PlayerPrefs.GetInt("watch") == 1)
                _animatorLeft.SetBool("Watch", true);
        }
    }

    public void SnapStart()
    {
        _animatorLeft.SetTrigger(snap);
    }

    public void TakeStart()
    {
        _animatorRight.SetTrigger(take);
    }

    public void DropStart()
    {
        _animatorRight.SetTrigger(drop);
    }

}

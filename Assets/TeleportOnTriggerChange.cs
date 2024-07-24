using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportOnTriggerChange : MonoBehaviour
{
    [SerializeField] Animator _teleportAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _teleportAnimator.SetTrigger("out");
            Destroy(this.gameObject);
        }
    }
}

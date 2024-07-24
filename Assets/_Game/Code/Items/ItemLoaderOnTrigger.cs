using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoaderOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            ItemLoader.Instance.SpawnItem();
            Destroy(this.gameObject);
        }
    }
}

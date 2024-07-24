using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [System.Serializable]
    public class VisibleUI : MonoBehaviour, IVisibleUI
    {
        public virtual void Deactive() => gameObject.SetActive(false);
        public virtual void Active() => gameObject.SetActive(true);

        public void SetActive(bool isActive)
        {
            if (isActive) Active();
            else Deactive();
        }
    }
}

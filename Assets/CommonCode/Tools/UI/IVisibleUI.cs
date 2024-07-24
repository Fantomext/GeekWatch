using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public interface IVisibleUI
    {
        public abstract void Deactive();
        public abstract void Active();

        public abstract void SetActive(bool isActive);
    }
}

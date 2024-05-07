using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraUtility
{
    public class RendererVisible : MonoBehaviour
    {
        public event Action<RendererVisible> OnRendererBecameVisible;
        public event Action<RendererVisible> OnRenderercameInvisible;

        private void OnBecameVisible() => OnRendererBecameVisible?.Invoke(this);
        private void OnBecameInvisible() => OnRenderercameInvisible?.Invoke(this);
    }
}
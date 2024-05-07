using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraUtility
{
    public class DebugOut : MonoBehaviour
    {
        public string text;
        public void DebugOutText()
        {
            Debug.Log(text);    
        }
    }
}
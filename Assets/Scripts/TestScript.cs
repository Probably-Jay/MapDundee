using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using CustomTools;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Vector2 a = new(-2.971841f, 56.459174f);
        // var g = GeoCord.FromLocationService(a);
        // Debug.Log(g.AsVector);
        // var w = g.ToWorldSpace();
        // Debug.Log(w);
        // var m = GeoCord.FromWorldSpace(w);
        // Debug.Log(m.AsVector);
    }

    // Update is called once per frame
    void Update()
    {
        DebugText.Instance["Time"] = Time.realtimeSinceStartup.ToString(CultureInfo.CurrentCulture);
    }
}

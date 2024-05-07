using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{

    [SerializeField] Vector2 origin;
    [SerializeField] Vector2 pos;

    [SerializeField, TextArea(1,1)] string st;

    // Start is called before the first frame update
    void Start()
    {
        GPSEncoder.SetLocalOrigin(origin);

        var ucs = GPSEncoder.GPSToUCS(pos);
        Debug.Log($"ucs: {ucs}");

        var poss = GPSEncoder.USCToGPS(ucs);
        Debug.Log($"gps: {poss}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

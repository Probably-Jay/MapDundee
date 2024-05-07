using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{

    [SerializeField] Vector2 origin;
    [SerializeField] double xOrigin;
    [SerializeField] double yOrigin;
    [SerializeField] Vector2 pos;

    [SerializeField, TextArea(1,1)] string st;

    // Start is called before the first frame update
    void Start()
    {
        GPSEncoder.SetLocalOrigin((xOrigin, yOrigin));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

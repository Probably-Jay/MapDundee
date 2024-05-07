using System;
using System.Collections;
using System.Collections.Generic;
using CustomTools;
using Unity.Mathematics;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private double xOffset;
    [SerializeField] private double yOffset;

    [SerializeField] private bool b = true;

    [SerializeField, TextArea(1,3)] private string foo;
    
    void Update()
    {
        //var geoCordFromUs = new GeoCord(offset.x, offset.y);

        // transform.position =  geoCordFromUs.ToWorldSpace();

        var pos = GPSEncoder.GPSToUCS(xOffset, yOffset);

       //var pos = GeoCord.GeoCordToWorldSpace(geoCordFromUs, b);
       transform.position =  new Vector3((float)pos.x, (float)pos.y, 0);
    }
}

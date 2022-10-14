using System;
using System.Collections;
using System.Collections.Generic;
using CustomTools;
using Unity.Mathematics;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    [SerializeField] private Vector2 offset;

    [SerializeField] private bool b = true;
    
    
    void Update()
    {
        var geoCordFromUs = new GeoCord(offset);

       // transform.position =  geoCordFromUs.ToWorldSpace();
       transform.position =  GeoCord.GeoCordToWorldSpace(geoCordFromUs);
    }
}

using ExtraUtility;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


namespace ExtraUtility
{
    public static class GameObjectExtensions
    {
        public static string GetFullyQualifiedSceneName(this GameObject gameObject)
        {
            var name = new StringBuilder(gameObject.name);
            var parent = gameObject.transform.parent;
            while (parent != null)
            {
                name.Append($".{parent.gameObject.name}");
                parent = parent.parent;
            }
            name.Append($".{SceneManager.GetActiveScene().name}");
            return name.ToString();
        }
    } 
}
//namespace SaveSysyem
//{

//    public interface ISaveSystemSerialiseable
//    {
//        public string Serialise(Serialiser serialiser);
//        public void Deserialise(Serialiser serialiser, string serialisation);
//    }

//    public readonly struct SaveDataElement
//    {
//        readonly string ID;
//        readonly string Payload;

//        public SaveDataElement(string iD, string payload) : this()
//        {
//            ID = iD;
//            Payload = payload;
//        }
//    }

//}
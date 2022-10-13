using System;
using System.Collections;
using System.Collections.Generic;
using SingletonT;
using UnityEngine;

public class LocationService : Singleton<LocationService>
{
   public new static LocationService Instance => Singleton<LocationService>.Instance;

   public readonly GeoCord Offset = GeoCord.FromLocationService(new Vector2(-2.965212f, 56.462334f));
   
   
   /*
    * y Latitude: 1 deg = 110_574 m 
    * x Longitude: 1 deg = 111_320*cos(latitude) m 
    */
   
   public GeoCord? CurrentLocation
   {
      get
      {
         if (Input.location.status != LocationServiceStatus.Running)
         {
            return null;
         }

         var x = Input.location.lastData.latitude;
         var y = Input.location.lastData.longitude;
         var location = new Vector2(x, y);

         return GeoCord.FromLocationService(location - Offset.AsVector);
      }
   }
}




public readonly struct GeoCord
{

   public static GeoCord FromLocationService(Vector2 v) => new(v);

   public GeoCord(Vector2 v)
   {
      Longitude = v.x;
      Latitude = v.y;
   }

   public static implicit operator GeoCord(Vector2 v) => new(v);

   public static implicit operator Vector2(GeoCord g) => g.AsVector;

   public float Longitude { get; }

   public float Latitude { get; }

   public Vector2 AsVector => new(Longitude, Latitude);

   public Vector2 ToWorldSpace(bool b = true)
   {
      var current = this.AsVector;

      var scaledCurrent = ToWorldScale(current, b);

      var originToScale = ToWorldScale(LocationService.Instance.Offset.AsVector, b);

      Vector2 localCord = scaledCurrent - originToScale;

      return localCord;
      
      // var originOffset = LocationService.Instance.Offset;
      //
      // GeoCord localCord = this.AsVector - originOffset.AsVector;
      //
      // var localPos = ToWorldScale(localCord, b);
      
    //  return localPos;
   }

   private static Vector2 ToWorldScaleNew(GeoCord localCord)
   {
      var x = localCord.AsVector.x * 110574f;
      
      var y = localCord.AsVector.y * 6371000f * Mathf.Sin(localCord.AsVector.y);
      
      return new Vector2(x, y);
      
      // var longitude = localCord.Longnitude * 111000 * Mathf.Cos(Mathf.Deg2Rad * localCord.Latitude) ;
      // var latitude = localCord.Latitude * 111000 ;
      // return new Vector2(longitude, latitude);
   }

   private static Vector2 ToWorldScale(GeoCord localCord, bool b)
   {
      double x;
      double y;
      if (b)
      {
         x = localCord.Longitude * 111_320;
         y = Mathf.Tan((float)((double)Mathf.Deg2Rad * (double)localCord.Latitude)) * 6370_000;
      }
      else
      {
         x = localCord.Longitude * 111000;
         y = localCord.Latitude* 111000;
      }
      return new Vector2((float)x, (float)y);
}

   public static GeoCord FromWorldSpace(Vector2 v)
   {
      var geoScale = FromWorldScale(v);

      var originOffset = LocationService.Instance.Offset;

      var nonlocal = geoScale + originOffset;

      return nonlocal;
   }

   private static Vector2 FromWorldScale(Vector2 v)
   {
      return v / GeoCordConversionConst;
   }


   private const float GeoCordConversionConst = 111000f;
}

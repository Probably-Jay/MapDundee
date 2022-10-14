using System;
using System.Collections;
using System.Collections.Generic;
using SingletonT;
using UnityEngine;

public class LocationService : Singleton<LocationService>
{
   public new static LocationService Instance => Singleton<LocationService>.Instance;

   public readonly GeoCord Offset = GeoCord.FromLocationService(new Vector2(-2.965212f, 56.462334f));

   public Vector2 pixelOffset;
   public float mapScale;
   
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

   public GeoCord(Vector2 v): this(longitude: v.x, latitude: v.y)
   {
   }

   public GeoCord(float longitude, float latitude) 
   {  Longitude = longitude;
      Latitude = latitude;
   }


   public static implicit operator GeoCord(Vector2 v) => new(v);

   public static implicit operator Vector2(GeoCord g) => g.AsVector;

   public float Longitude { get; }

   public float Latitude { get; }

   public Vector2 AsVector => new(Longitude, Latitude);
   
   //p0
   private static (Vector2 scene, GeoCord geoCord) topLeft = new (new Vector2(0f, 1250f), new GeoCord(-2.9973157131792667f, 56.468069672214014f));
   
   // p1
   private static (Vector2 scene, GeoCord geoCord) bottomRight = new (new Vector2(2500f, 0f), new GeoCord(-2.957536501757597f, 56.456901948333f));
   
   private static float earthRadiusInKm= 6371;      //Earth Radius in Km

//## Now I can calculate the global X and Y for each reference point ##\\

// This function converts lat and lng coordinates to GLOBAL X and Y positions
static Vector2 GeoCordToGlobalXY(GeoCord geoCord)
   {
      //Calculates x based on cos of average of the latitudes
      var x = earthRadiusInKm * geoCord.Longitude* Mathf.Cos((topLeft.geoCord.Latitude + bottomRight.geoCord.Latitude) / 2f);
      //Calculates y based on latitude
      var y = earthRadiusInKm * geoCord.Latitude;
      return new Vector2(x, y);
   }

public static Vector2 GeoCordToWorldSpace(GeoCord geoCord)
   {
      /*
* This gives me the X and Y in relation to map for the 2 reference points.
* Now we have the global AND screen areas and then we can relate both for the projection point.
*/

         
      // Calculate global X and Y for top-left reference point
      Vector2 globalTopLeft = GeoCordToGlobalXY(topLeft.geoCord);
      // Calculate global X and Y for bottom-right reference point
      Vector2 globalBottomRight = GeoCordToGlobalXY(bottomRight.geoCord);
      
      //Calculate global X and Y for projection point
      var globalXY = GeoCordToGlobalXY(geoCord);

      Vector2 lerpVals;
      
      //Calculate the percentage of Global X position in relation to total global width
      lerpVals.x = ((globalXY.x - globalTopLeft.x)/(globalBottomRight.x - globalTopLeft.x));
      
      //Calculate the percentage of Global Y position in relation to total global height
      lerpVals.y = ((globalXY.y - globalTopLeft.y)/(globalBottomRight.y - globalTopLeft.y));

      //Returns the screen position based on reference points
      
      var x = Mathf.Lerp(topLeft.scene.x, bottomRight.scene.x, lerpVals.x);
      var y = Mathf.Lerp(topLeft.scene.y, bottomRight.scene.y, lerpVals.y);

      return new Vector2(x, y);
   }





//# The usage is like this #\\

// var pos = latlngToScreenXY(-22.815319, -47.071718);
// $point = $("#point-to-project");
// $point.css("left", pos.x+"em");
// $point.css("top", pos.y+"em");

   // public Vector2 ToWorldSpace()
   // {
   //    var scale = LocationService.Instance.mapScale;
   //    var scaledCurrent = ToWorldScale(scale * this.AsVector);
   //    return scaledCurrent;
   //    // var scale = LocationService.Instance.mapScale;
   //    // var scaledCurrent = ToWorldScale(scale * this.AsVector);
   //    // var scaledOffset = (scale * LocationService.Instance.Offset.AsVector);
   //    //
   //    // var posCurrent = new GeoCord(scaledCurrent - scaledOffset);
   //    // return posCurrent;
   // }

//    private static Vector2 ToWorldScale(GeoCord geoCord)
//    {
//       var pixelVals = LocationService.Instance.pixelOffset;
//
//       var x = pixelVals.x * (180f + geoCord.Latitude) / 360f;
//       var y = pixelVals.y * (90f - geoCord.Longitude) / 180f;
//
//       return new Vector2(x, y);
//    }
//
//    public Vector2 ToWorldSpaceOlder(bool b = true)
//    {
//       var current = this.AsVector;
//
//       var scaledCurrent = ToWorldScaleOlder(current, b);
//
//       var originToScale = ToWorldScaleOlder(LocationService.Instance.Offset.AsVector, b);
//
//       Vector2 localCord = scaledCurrent - originToScale;
//
//       return localCord;
//       
//       // var originOffset = LocationService.Instance.Offset;
//       //
//       // GeoCord localCord = this.AsVector - originOffset.AsVector;
//       //
//       // var localPos = ToWorldScale(localCord, b);
//       
//     //  return localPos;
//    }
//
//    private static Vector2 ToWorldScaleNew(GeoCord localCord)
//    {
//       var x = localCord.AsVector.x * 110574f;
//       
//       var y = localCord.AsVector.y * 6371000f * Mathf.Sin(localCord.AsVector.y);
//       
//       return new Vector2(x, y);
//       
//       // var longitude = localCord.Longnitude * 111000 * Mathf.Cos(Mathf.Deg2Rad * localCord.Latitude) ;
//       // var latitude = localCord.Latitude * 111000 ;
//       // return new Vector2(longitude, latitude);
//    }
//
//    private static Vector2 ToWorldScaleOlder(GeoCord localCord, bool b)
//    {
//       double x;
//       double y;
//       if (b)
//       {
//          x = localCord.Longitude * 111_320;
//          y = Mathf.Tan((float)((double)Mathf.Deg2Rad * (double)localCord.Latitude)) * 6370_000;
//       }
//       else
//       {
//          x = localCord.Longitude * 111000;
//          y = localCord.Latitude* 111000;
//       }
//       return new Vector2((float)x, (float)y);
// }
//
//    public static GeoCord FromWorldSpace(Vector2 v)
//    {
//       var geoScale = FromWorldScale(v);
//
//       var originOffset = LocationService.Instance.Offset;
//
//       var nonlocal = geoScale + originOffset;
//
//       return nonlocal;
//    }
//
//    private static Vector2 FromWorldScale(Vector2 v)
//    {
//       return v / GeoCordConversionConst;
//    }
//
//
//    private const float GeoCordConversionConst = 111000f;
}

using System;
using System.Collections;
using System.Collections.Generic;
using CustomTools;
using SingletonT;
using UnityEngine;

[Serializable]
public struct DoubleVec
{
   public DoubleVec(double x, double y)
   {
      this.x = x;
      this.y = y;
   }
   public double x;
   public double y;
}

[Serializable]
public struct SceneWorldTie
{
   public DoubleVec scenePosition;
   public GeoCord GeoCord;
}


public class LocationService : Singleton<LocationService>
{
   public new static LocationService Instance => Singleton<LocationService>.Instance;

 //  public readonly GeoCord Offset = GeoCord.FromLocationService(new Vector2(-2.965212f, 56.462334f));

  // public Vector2 pixelOffset;
  //   public float mapScale;

  public SceneWorldTie topLeft;// = new() {scenePosition = {x = -4055, y =1439 }, GeoCord = new GeoCord(longitude: -3.0728734194401204, latitude: 56.48873568677126)};
   public SceneWorldTie bottomRight;// = new() {scenePosition = {x = 2989, y =-2039 }, GeoCord = new GeoCord(longitude: -2.918578798326768, latitude: 56.44756222755063)};

   /*
    * y Latitude: 1 deg = 110_574 m 
    * x Longitude: 1 deg = 111_320*cos(latitude) m 
    */
   
   // public GeoCord? CurrentLocation
   // {
   //    get
   //    {
   //       if (Input.location.status != LocationServiceStatus.Running)
   //       {
   //          return null;
   //       }
   //
   //       var x = Input.location.lastData.latitude;
   //       var y = Input.location.lastData.longitude;
   //       var location = new Vector2(x, y);
   //
   //       return GeoCord.FromLocationService(location - Offset.AsVector);
   //    }
   // }
}



[Serializable]
public struct GeoCord
{

  // public static GeoCord FromLocationService(Vector2 v) => new(v);

   public GeoCord(DoubleVec v): this(longitude: v.x, latitude: v.y)
   {
   }

   public GeoCord(double longitude, double latitude) 
   {  Longitude = longitude;
      Latitude = latitude;
   }


  // public static implicit operator GeoCord(Vector2 v) => new(v);

 //  public static implicit operator Vector2(GeoCord g) => g.AsVector;

   [field: SerializeField] public double Longitude { get; private set; }

   [field: SerializeField] public double Latitude { get; private set; }

 //  public Vector2 AsVector => new(Longitude, Latitude);
   
 //  private static (Vector2 scene, GeoCord geoCord) topLeft = new (new Vector2(0f, 1250f), new GeoCord(-2.9973157131792667f, 56.468069672214014f));
   
 //  private static (Vector2 scene, GeoCord geoCord) bottomRight = new (new Vector2(2500f, 0f), new GeoCord(-2.957536501757597f, 56.456901948333f));
   
   //private static double earthRadiusInKm= 6371;
   private static double earthRadiusInKm= 2.0*6371;

   DoubleVec GeoCordToGlobalXY()
   {
      var topLeftLatitude = LocationService.Instance.topLeft.GeoCord.Latitude;
      var bottomRightLatitude = LocationService.Instance.bottomRight.GeoCord.Latitude;
      var averageLatitude = (topLeftLatitude + bottomRightLatitude) / 2.0;

      var latitudeScaleFactor = Math.Cos((3.1415926535_8979323846_2643383279/180.0) * averageLatitude); // = ~0.5
      
      var x = earthRadiusInKm * Longitude * latitudeScaleFactor;
      var y = earthRadiusInKm * Latitude;
      
      return new DoubleVec(x, y);
   }


   public static Vector2 GeoCordToWorldSpace(GeoCord geoCord, bool b)
   {
      return b switch
      {
         true => GeoCordToWorldSpaceAAA(geoCord),
         false => GeoCordToWorldSpaceBBB(geoCord)
      };
   }

   public static Vector2 GeoCordToWorldSpaceAAA(GeoCord geoCord)
   {
      
      var globalXY = geoCord.GeoCordToGlobalXY();
      var globalTopLeft = LocationService.Instance.topLeft.GeoCord.GeoCordToGlobalXY();
      var globalBottomRight = LocationService.Instance.bottomRight.GeoCord.GeoCordToGlobalXY();

      DoubleVec lerpVals;

      lerpVals.x = InverseLerpUnclamped(globalTopLeft.x, globalBottomRight.x, globalXY.x);
      lerpVals.y = InverseLerpUnclamped(globalTopLeft.y, globalBottomRight.y, globalXY.y);

      var x = LerpUnclamped(LocationService.Instance.topLeft.scenePosition.x, LocationService.Instance.bottomRight.scenePosition.x, lerpVals.x);
      var y = LerpUnclamped(LocationService.Instance.topLeft.scenePosition.y, LocationService.Instance.bottomRight.scenePosition.y, lerpVals.y);

      return new Vector2((float)x, (float)y);
   }

   public static Vector2 GeoCordToWorldSpaceBBB(GeoCord geoCord)
   {
      
      var topLeft = LocationService.Instance.topLeft.GeoCord;
      var bottomRight = LocationService.Instance.bottomRight.GeoCord;
      
      DoubleVec lerpVals;

      lerpVals.x = InverseLerpUnclamped(topLeft.Longitude, bottomRight.Longitude, geoCord.Longitude);
      lerpVals.y = InverseLerpUnclamped(topLeft.Latitude, bottomRight.Latitude, geoCord.Latitude);

      var x = LerpUnclamped(LocationService.Instance.topLeft.scenePosition.x, LocationService.Instance.bottomRight.scenePosition.x, lerpVals.x);
      var y = LerpUnclamped(LocationService.Instance.topLeft.scenePosition.y, LocationService.Instance.bottomRight.scenePosition.y, lerpVals.y);

      return new Vector2((float)x, (float)y);
   }

   public static double LerpUnclamped(double a, double b, double t) => a + (b - a) * t;
public static double InverseLerpUnclamped(double a, double b, double value) => Math.Abs(a - b) > double.Epsilon ? (value - a) / (b - a) : 0.0;




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

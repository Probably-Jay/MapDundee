//Copyright 2013 MichaelTaylor3D
//www.michaeltaylor3d.com

using System;
using UnityEngine;

public sealed class GPSEncoder {

	/////////////////////////////////////////////////
	//////-------------Public API--------------//////
	/////////////////////////////////////////////////
	
	///// <summary>
	///// Convert UCS (X,Y,Z) coordinates to GPS (Lat, Lon) coordinates
	///// </summary>
	///// <returns>
	///// Returns Vector2 containing Latitude and Longitude
	///// </returns>
	///// <param name='position'>
	///// (X,Y,Z) Position Parameter
	///// </param>
	//public static Vector2 USCToGPS(double x, double y)
	//{
	//	return GetInstance().ConvertUCStoGPS(position);
	//}
	
	///// <summary>
	///// Convert GPS (Lat, Lon) coordinates to UCS (X,Y,Z) coordinates
	///// </summary>
	///// <returns>
	///// Returns a Vector3 containing (X, Y, Z)
	///// </returns>
	///// <param name='gps'>
	///// (Lat, Lon) as Vector2
	///// </param>
	//public static Vector3 GPSToUCS(double x, double y)
	//{
	//	return GetInstance().ConvertGPStoUCS(gps);
	//}
	
	/// <summary>
	/// Convert GPS (Lat, Lon) coordinates to UCS (X,Y,Z) coordinates
	/// </summary>
	/// <returns>
	/// Returns a Vector3 containing (X, Y, Z)
	/// </returns>
	public static (double x, double y) GPSToUCS(double latitude, double longitude)
	{
		return GetInstance().ConvertGPStoUCS((latitude,longitude));
	}
	
	/// <summary>
	/// Change the relative GPS offset (Lat, Lon), Default (0,0), 
	/// used to bring a local area to (0,0,0) in UCS coordinate system
	/// </summary>
	/// <param name='localOrigin'>
	/// Referance point.
	/// </param>
	public static void SetLocalOrigin((double x, double y) localOrigin)
	{
		GetInstance()._localOrigin = localOrigin;
	}
		
	/////////////////////////////////////////////////
	//////---------Instance Members------------//////
	/////////////////////////////////////////////////
	
	#region Singleton
	private static GPSEncoder _singleton;
	
	private GPSEncoder()
	{
		
	}
	
	private static GPSEncoder GetInstance()
	{
		if(_singleton == null)
		{
			_singleton = new GPSEncoder();
		}
		return _singleton;
	}
	#endregion
	
	#region Instance Variables
	private (double x, double y) _localOrigin = (0.0,0.0);
	private double _LatOrigin { get{ return _localOrigin.x; }}	
	private double _LonOrigin { get{ return _localOrigin.y; }}

	private double metersPerLat;
	private double metersPerLon;
	#endregion
	
	#region Instance Functions
	private void FindMetersPerLat(double lat) // Compute lengths of degrees
	{
	    // Set up "Constants"
	    double m1 = 111132.92f;    // latitude calculation term 1
	    double m2 = -559.82f;        // latitude calculation term 2
	    double m3 = 1.175f;      // latitude calculation term 3
	    double m4 = -0.0023f;        // latitude calculation term 4
	    double p1 = 111412.84f;    // longitude calculation term 1
	    double p2 = -93.5f;      // longitude calculation term 2
	    double p3 = 0.118f;      // longitude calculation term 3
	    
	    lat = lat * (Math.PI / 180.0);
	
	    // Calculate the length of a degree of latitude and longitude in meters
	    metersPerLat = m1 + (m2 * Math.Cos(2 * (double)lat)) + (m3 * Math.Cos(4 * (double)lat)) + (m4 * Math.Cos(6 * (double)lat));
	    metersPerLon = (p1 * Math.Cos((double)lat)) + (p2 * Math.Cos(3 * (double)lat)) + (p3 * Math.Cos(5 * (double)lat));	   
	}

	private (double x, double y) ConvertGPStoUCS((double x, double y) gps)  
	{
		FindMetersPerLat(_LatOrigin);
		double xPosition  = metersPerLat * (gps.x - _LatOrigin); //Calc current lat
		double yPosition  = metersPerLon * (gps.y - _LonOrigin); //Calc current lat
		return ((double)xPosition, (double)yPosition);
	}
	
	//private Vector2 ConvertUCStoGPS((double x, double y) position)
	//{
	//	FindMetersPerLat(_LatOrigin);
	//	Vector2 geoLocation = new Vector2(0,0);
	//	geoLocation.x = (_LatOrigin + (position.z)/metersPerLat); //Calc current lat
	//	geoLocation.y = (_LonOrigin + (position.x)/metersPerLon); //Calc current lon
	//	return geoLocation;
	//}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using CustomTools;
using UnityEngine;

public class LocationServiceInitializer : MonoBehaviour
{
    private bool updateLocation = true;
    
    private IEnumerator LocationCoroutine()
    {
        
        
        // Uncomment if you want to test with Unity Remote
/*#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
#endif*/
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!AskForPermissions()) 
            yield break;
#endif

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        
        var maxWait = 15;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            DebugText.Instance["Connected Status"] = $"Connecting ({maxWait})";
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        {
            // Editor has a bug which doesn't set the service status to Initializing. So different wait for Editor.
#if UNITY_EDITOR
            var editorMaxWait = 15;
            while (Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
            {
                DebugText.Instance["Connected Status"] = $"Connecting: Editor ({editorMaxWait})";
                yield return new WaitForSecondsRealtime(1);
                editorMaxWait--;
            }
#endif
        }

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            DebugText.Instance["Connected Status"] = "Timed out";
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status != LocationServiceStatus.Running)
        {
            DebugText.Instance["Connected Status"] = Input.location.status.ToString();

            Debug.LogFormat($"Unable to determine device location. Failed with status {Input.location.status}");
            yield break;
        }


        DebugText.Instance["Connected Status"] = Input.location.status.ToString();
        Debug.LogFormat($"Location service live. status {Input.location.status}");

        while (updateLocation)
        {
            // Access granted and location value could be retrieved
            var status = Input.location.status;
            var enabledByUser = Input.location.isEnabledByUser;
            
            var latitude = Input.location.lastData.latitude;
            var longitude = Input.location.lastData.longitude;
            var altitude = Input.location.lastData.altitude;
            var horizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            var timestamp = Input.location.lastData.timestamp;

            DebugText.Instance[nameof(status)] = status.ToString();
            DebugText.Instance[nameof(enabledByUser)] = enabledByUser.ToString();
            DebugText.Instance[nameof(latitude)] = latitude.ToString();
            DebugText.Instance[nameof(longitude)] = longitude.ToString();
            DebugText.Instance[nameof(altitude)] = altitude.ToString();
            DebugText.Instance[nameof(horizontalAccuracy)] = horizontalAccuracy.ToString();
            DebugText.Instance[nameof(timestamp)] = timestamp.ToString();

            var locationInfo = $"Location: {latitude} {longitude} {altitude} {horizontalAccuracy} {timestamp}";

            Debug.LogFormat(locationInfo);
            yield return null;
        }
    }

    private static bool AskForPermissions()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }

        // First, check if user has location service enabled
        if (Input.location.isEnabledByUser) 
            return true;

        DebugText.Instance["Connected Status"] = "Permissions denied";
        Debug.LogFormat("Android and Location not enabled");
        return false;
    }

    void Start()
    {
        return;
        
        StartCoroutine(LocationCoroutine());
    }

   
}

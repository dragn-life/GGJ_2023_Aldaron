using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class Analytics
{
  public static void TrackEvent(string eventName, IDictionary<string, object> eventParams = null)
  {
    Debug.Log("Track Event: " + eventName);
    var validParams = eventParams ?? new Dictionary<string, object>();
    AnalyticsService.Instance.CustomData(eventName, validParams);
  }
}
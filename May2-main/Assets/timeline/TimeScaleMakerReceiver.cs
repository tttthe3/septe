using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeScaleMakerReceiver : MonoBehaviour, INotificationReceiver
{
    // Start is called before the first frame update
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var maker = notification as TimeScaleMaker;
        if (maker == null)
        {
            return;
        }
        this.ChangeTimescale(maker.TimeScale);

    }
    private void ChangeTimescale(float timeScale)
    {
        Time.timeScale = timeScale;

    }
}

  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeScaleMaker : Marker,INotification
{
    // Start is called before the first frame update
    [SerializeField]
    private float timeScale = 0;
    public float TimeScale => timeScale;
    public PropertyName id => new PropertyName("Timescale");


}

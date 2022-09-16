using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeckfps : MonoBehaviour
{
    int frameCount;
    float prevTime;

    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }

    void Update()
    {
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            Debug.LogFormat("{0}fps", frameCount / time);

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}

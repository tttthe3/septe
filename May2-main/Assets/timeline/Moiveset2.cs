using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class Moiveset2 : PlayableBehaviour
{
    private PlayableDirector director;
    public GameObject templateGameObject { get; set; }
    private bool trigger=false;
  

    [SerializeField]
    public bool boolValue = false;
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        //director.playableGraph.GetRootPlayable(0).SetSpeed(0.2d);
    }

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        if(Input.GetKeyDown(KeyCode.A))
            director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1.2d);
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {


        if (Input.GetKeyDown(KeyCode.A))
            director.Resume();



    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
       
       

    }

   
    
}

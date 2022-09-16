using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class MovetimeMaker_Flag : MonoBehaviour
{
    // Start is called before the first frame update
    public enum state { play, pause, stop }
    public state State = state.play;
    public PlayableDirector playableDirector;
    public Flag_content flags;
    public void puase()
    {
        Debug.Log(playableDirector.state);
        playableDirector.Pause();
        State = state.pause;
    }
    public void restart()
    {

        if (Playerinput.Instance.Intract.Down)
            State = state.pause;
    }

    void Update()
    {
        Debug.Log(playableDirector.state);
        Debug.Log(flags.flag);
        if (playableDirector.state == PlayState.Paused && flags.flag)
        {
            State = state.play;
            Debug.Log(playableDirector.state);
            playableDirector.Resume();
            return;

        }
    }
}

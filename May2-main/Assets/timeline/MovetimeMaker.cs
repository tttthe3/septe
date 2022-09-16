using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;
public class MovetimeMaker : MonoBehaviour
{
    public enum state { play,pause,stop}
    public state State = state.play;
    public PlayableDirector playableDirector;
    public Flag_content flags;
    public Transform moves;
    private Transform firstpos;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        firstpos = this.transform;
    }
    public void puase()
    {
        //playableDirector.Pause();
        //playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        playableDirector.Pause();
        State = state.pause;
    }
    public void restart()
    {

        if (Playerinput.Instance.Intract.Down)
            State=state.pause;
    }


    public void skip()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetTime(6f);
    }

    public void onlyintract()
    {
        Playerinput.Instance.Intract.GainControl();
    }

    public void Allintract()
    {
        Playerinput.Instance.GainControl();
    }

    public void updown() {

        moves.DOMoveY(firstpos.position.y + 5, 1f, false).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

    }

    public void TurnRight()
    {
        Charactercontrolelr.CCInstance.TurnLeft();
    }

    void Update()
    {
        Debug.Log(playableDirector.state);

        if (playableDirector.state == PlayState.Paused && Playerinput.Instance.Intract.Down&& State == state.pause)
        {
            State = state.play;
            Debug.Log(playableDirector.state);
            playableDirector.Resume();

           
        }
        if (playableDirector.state == PlayState.Playing&&Playerinput.Instance.Intract.Down && State == state.pause)
        {
            if (playableDirector.playableGraph.GetRootPlayable(0).GetSpeed() != 0)
                return;
            State = state.play;
            Debug.Log(playableDirector.state);
             playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);

            return;

        }
    }

}

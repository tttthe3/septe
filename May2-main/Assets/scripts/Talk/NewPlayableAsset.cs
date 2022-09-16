using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class NewPlayableAsset : PlayableAsset
{
    public ExposedReference<GameObject> charaObj;
    // public string text;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var behaviour = new TextPlayableBehaviour();
        behaviour.charaObject = charaObj.Resolve(graph.GetResolver());
        // behaviour.text = text;
        return ScriptPlayable<TextPlayableBehaviour>.Create(graph, behaviour);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class movieset : PlayableAsset
{
    [SerializeField]
    private ExposedReference<GameObject> templateGameObject;

    public Moiveset2 template = new Moiveset2();
    public PlayableDirector Director;
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<Moiveset2>.Create(graph, template);
        var behav = new Moiveset2();
        // Get PlayableBehaviour
        var behaviour = playable.GetBehaviour();

        // Resolve Reference
        behaviour.templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable; 
    }
}

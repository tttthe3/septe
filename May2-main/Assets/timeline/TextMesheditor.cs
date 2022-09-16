using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class TextMesheditor : MonoBehaviour
{
    public TextMeshProUGUI texts;
    // Start is called before the first frame update
    public void fadeeach()
    {
        //texts.DOFade(0, 0);
        texts = this.GetComponent<TextMeshProUGUI>();
        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(texts);
        //for (int i = 0; i < tmproAnimator.textInfo.characterCount; i++)
        //{
        //   tmproAnimator.DOFadeChar(i, 1, 2f).SetDelay(i * 0.2f);
        // }

        texts.maxVisibleCharacters = 0;
        texts.DOMaxVisibleCharacters(texts.text.Length, 3.0f).Play();

    }

    public void fadeouteach()
    {

        texts.DOPause();
        texts.DOFade(0, 0.2f).SetUpdate(true); ;

    }
}

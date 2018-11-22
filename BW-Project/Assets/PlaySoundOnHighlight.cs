using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnHighlight : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler, IPointerClickHandler
{
    private bool nonStop;

    public void OnPointerClick(PointerEventData eventData)
    {
        nonStop = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "Prayut")
        {
            SoundStore.instance.StopSound();
            SoundStore.instance.PlayTH();
        }
        else if (gameObject.name == "Trump")
        {
            SoundStore.instance.StopSound();
            SoundStore.instance.PlayUSA();
        }
        else if (gameObject.name == "Kim")
        {
            SoundStore.instance.StopSound();
            SoundStore.instance.PlayNK();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!nonStop)
        {
            SoundStore.instance.StopSound();
            nonStop = false;
        }
    }
}

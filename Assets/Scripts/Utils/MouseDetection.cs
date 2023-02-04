using MyBox;
using freakingpig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using freakingpig.holders;

namespace freakingpig.utils
{
    public class MouseDetection : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {

        public void OnPointerEnter(PointerEventData e)
        {
            SPlayer.PlaySFX(SoundHolder.Instance.mouseHover, 2f);
        }

        public void OnPointerDown(PointerEventData e)
        {
            SPlayer.PlaySFX(SoundHolder.Instance.mouseClick, 5f);
        }

    }
}

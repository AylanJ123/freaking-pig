using MyBox;
using System.Collections;
using System.Collections.Generic;
using t3ampo.mixit.holders;
using UnityEngine;
using UnityEngine.EventSystems;

namespace t3ampo.mixit.utils
{
    public class MouseDetection : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {

        /// <summary> <inheritdoc cref="GameManager.sHolder"/> </summary>
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private SoundsHolder sHolder;

        public void OnPointerEnter(PointerEventData e)
        {
            SPlayer.PlaySFX(sHolder.mouseHover, .1f);
        }

        public void OnPointerDown(PointerEventData e)
        {
            SPlayer.PlaySFX(sHolder.mouseClick, .225f);
        }

    }
}

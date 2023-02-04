using DG.Tweening;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t3ampo.mixit
{
    public class Transitions : MonoBehaviour
    {

        /// <summary> <inheritdoc cref="GameManager.Instance"/> </summary>
        public static Transitions Instance { get; private set; }

        /// <summary> The big black cover for transitions </summary>
        [SerializeField, AutoProperty(AutoPropertyMode.Children)]
        private SpriteRenderer rend;

        /// <summary>
        /// Creates the Singleton and prevents the scene from destroying the Game Object
        /// </summary>
        void Awake()
        {
            Instance = this;
            rend = GetComponentInChildren<SpriteRenderer>();
            gameObject.DontDestroyOnLoad();
        }

        /// <summary>
        /// Makes the game go black and then fades back to full color. Highly customizable.
        /// </summary>
        /// <param name="movetime"> Time it takes to fade, only accounts for one fade </param>
        /// <param name="stayTime"> The time it stays completely black. </param>
        /// <param name="callback"> A callback called just after fade in and right before staying pitch black </param>
        public static void Transition(float movetime, float stayTime = 0, Action callback = null, float alphaFade = 1)
        {
            Sequence seq = DOTween.Sequence();
            if (Instance.rend.color.a == 0) seq.Append(Instance.rend.DOFade(alphaFade, movetime));
            if (callback != null) seq.AppendCallback(() => callback.Invoke());
            if (stayTime > 0) seq.AppendInterval(stayTime);
            seq.Append(Instance.rend.DOFade(0, movetime));
            seq.Play();
        }

    }
}

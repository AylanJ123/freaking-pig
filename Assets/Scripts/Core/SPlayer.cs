using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace t3ampo.mixit
{
    public class SPlayer : MonoBehaviour
    {

        /// <summary> <inheritdoc cref="GameManager.Instance"/> </summary>
        public static SPlayer Instance { get; private set; }

        /// <summary> Music audio source </summary>
        private AudioSource musicScr;

        /// <summary> SFX audio source </summary>
        private AudioSource sfxScr;

        /// <summary>
        /// Creates the Singleton, looks for the Audio Sources and prevents Scene
        /// from destroying the Game Object
        /// </summary>
        void Awake()
        {
            Instance = this;
            musicScr = GetComponent<AudioSource>();
            sfxScr = transform.GetChild(0).GetComponent<AudioSource>();
            gameObject.DontDestroyOnLoad();
        }

        /// <summary>
        /// Allows to smoothly switch the main track. Highly customizable.
        /// </summary>
        /// <param name="clip"> The clip to put instead </param>
        /// <param name="targetVolume"> The volume at which the clip will be set </param>
        /// <param name="fadeTime"> Time it takes to fade, only accounts for one fade </param>
        /// <param name="stayTime"> Time it stays completely mute </param>
        public static void SwitchTrack(AudioClip clip, float targetVolume, float fadeTime = 1, float stayTime = 0)
        {
            if (Instance.musicScr.clip == clip) return;
            Sequence seq = DOTween.Sequence();
            if (Instance.musicScr.volume > 0) seq.Append(Instance.musicScr.DOFade(0, fadeTime)).SetEase(Ease.InSine);
            seq.AppendCallback(() =>
            {
                Instance.musicScr.Stop();
                Instance.musicScr.clip = clip;
                Instance.musicScr.Play();
            });
            if (stayTime > 0) seq.AppendInterval(stayTime);
            seq.Append(Instance.musicScr.DOFade(targetVolume, fadeTime)).SetEase(Ease.InCirc);
            seq.Play();
        }

        public static void PlaySFX(AudioClip clip, float volume)
        {
            Instance.sfxScr.PlayOneShot(clip, volume);
        }

        /// <summary>
        /// Changes current main music volume
        /// </summary>
        /// <param name="amount"> Flat amount or coeficient if percentage is true </param>
        /// <param name="time"> How long it takes to change </param>
        /// <param name="percentage"> If true, amount behaves as the coeficient </param>
        public static void ChangeCurrentVolume(float amount, float time = 1, bool percentage = false)
        {
            Instance.musicScr.DOFade(percentage ? Instance.musicScr.volume * amount : amount, time);
        }

    }
}

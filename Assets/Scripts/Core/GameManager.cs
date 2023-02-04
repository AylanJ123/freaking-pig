using freakingpig.holders;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace freakingpig
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private SpriteHolder spHolder;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            SpriteHolder.Instance = spHolder;
            DontDestroyOnLoad(gameObject);
            Transitions.Transition(.25f);
            SPlayer.PlaySFX(SoundHolder.Instance.introSound, .55f);
            StartCoroutine(WaitFadeOut());
        }

        /// <summary> Waits a minimum time and then waits until loaded </summary>
        IEnumerator WaitFadeOut()
        {
            yield return new WaitForSeconds(2f);
            Transitions.Transition(.5f, 0, () =>
            {
                SceneManager.LoadScene("Game1", LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .4f, .25f);
            }
            );
        }

    }

    public static class Extensions
    {
        private static readonly System.Random rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}

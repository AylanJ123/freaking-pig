using freakingpig.holders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace freakingpig
{
    public class LooseMenu : MonoBehaviour
    {
        public void PlayAgain()
        {
            Transitions.Transition(.5f, 0, () =>
            {
                SceneManager.LoadScene("Game1", LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
            }
            );
        }
        public void ReturnMenu()
        {
            Transitions.Transition(.5f, 0, () =>
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
            }
            );
        }
    }
}

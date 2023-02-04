using freakingpig.holders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace freakingpig
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject settings;
        public GameObject credits;
        public void Play()
        {
            Transitions.Transition(.5f, 0, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
            }
            );
        }

        public void OpenSettings(){
            mainMenu.SetActive(false);
            credits.SetActive(false);
            settings.SetActive(true);
        }

        public void ReturnMainMenu(){
            mainMenu.SetActive(true);
            settings.SetActive(false);
            credits.SetActive(false);
        }

        public void OpenCredits(){
            settings.SetActive(false);
            mainMenu.SetActive(false);
            credits.SetActive(true);
        }

        public void Exit(){
            Application.Quit(1);
        }
    }
}

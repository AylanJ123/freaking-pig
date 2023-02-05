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
        public GameObject gameInstructions;
        public void Play()
        {
            Transitions.Transition(.5f, 0, () =>
            {
                SceneManager.LoadScene("Game1", LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
            }
            );
        }
        public void OpenGameInstructions()
        {
            mainMenu.SetActive(false);
            credits.SetActive(false);
            settings.SetActive(false);
            gameInstructions.SetActive(true);
        }

        public void OpenSettings(){
            mainMenu.SetActive(false);
            credits.SetActive(false);
            settings.SetActive(true);
            gameInstructions.SetActive(false);
        }

        public void ReturnMainMenu(){
            mainMenu.SetActive(true);
            settings.SetActive(false);
            credits.SetActive(false);
            gameInstructions.SetActive(false);
        }

        public void OpenCredits(){
            settings.SetActive(false);
            mainMenu.SetActive(false);
            credits.SetActive(true);
            gameInstructions.SetActive(false);
        }

        public void Exit(){
            Application.Quit(1);
        }
    }
}

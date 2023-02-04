using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class SliderSound : MonoBehaviour
    {

        public void SliderMusic(float amount)
        {
            SPlayer.Instance.GetComponent<AudioSource>().volume = amount;
        }

        public void SliderSFX(float amount)
        {
            SPlayer.Instance.transform.GetChild(0).GetComponent<AudioSource>().volume = amount;
        }


    }
}

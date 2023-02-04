using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace freakingpig
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        //Takes in a value from 0 to 1
        public void ChangeValue(float value){
            slider.value = value;
        }
    }
}

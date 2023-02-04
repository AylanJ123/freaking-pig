using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace freakingpig
{
    public class Points : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void ChangeScore(int score)
        {
            text.text = score.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;
using freakingpig.gameplay;

namespace freakingpig
{
    public class Points : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private void FixedUpdate()
        {
            text.text = FieldCreator.Instance.FieldCount.ToString();
        }

    }
}

using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.gameplay
{
    [ExecuteInEditMode]
    public class FieldCreator : MonoBehaviour
    {

        [SerializeField, InitializationField] private int sizeX;
        [SerializeField, InitializationField] private int sizeY;
        [SerializeField, InitializationField] private int rows;
        [SerializeField, InitializationField] private int columns;
        [SerializeField, InitializationField] private List<PlantGeneration> generation;

        private void Awake()
        {

        }

#if UNITY_EDITOR
        void Update()
        {
            ResizeField();
        }
#endif

        private void ResizeField()
        {
            SpriteRenderer rend = GetComponent<SpriteRenderer>();
            rend.size = new Vector2(sizeX, sizeY);
        }

        public class PlantGeneration
        {
            public float chances;
            [SearchableEnum] public PlantType type;
        }

    }
}

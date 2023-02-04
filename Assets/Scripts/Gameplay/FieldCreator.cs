using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using freakingpig.pooling;
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
        private Pool plantsPooler;
        public int FieldCount { get; private set; }

        private void Start()
        {
            if (!Application.isPlaying) return;
            PlantRandomManager plantRandom = new(generation, FieldCount = (sizeX - sizeX % rows) * (sizeY - sizeY % columns));
            plantsPooler = PoolingManager.Instance["Plants"];
            Vector3 anchor = transform.position - new Vector3(sizeX / 2f, sizeY / 2f, 0);
            for (int i = 1; i <= sizeX; i++)
                if (i % (rows + 1) != 0)
                    for (int j = 1; j <= sizeY; j++)
                        if (j % (columns + 1) != 0)
                        {
                            Plant plant = plantsPooler.Extract<Plant>();
                            plant.SetPlant(plantRandom.GetNext());
                            plant.transform.position = anchor + new Vector3(i - .5f, j - .5f, 0);
                        }
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

        [Serializable]
        public class PlantGeneration
        {
            public float chances;
            [SearchableEnum] public PlantType type;
        }

        private class PlantRandomManager
        {

            private readonly Stack<PlantType> plants;
            public PlantRandomManager(List<PlantGeneration> generation, int amount)
            {
                List<PlantType> plantList = new(amount);
                foreach (PlantGeneration gen in generation)
                    for (int i = 0; i < Mathf.Floor(amount * gen.chances); i++)
                        plantList.Add(gen.type);
                int amountLeft = amount - plantList.Count;
                for (int i = 0; i < amountLeft; i++)
                    plantList.Add(PlantType.Potato);
                plantList.Shuffle();
                plants = new Stack<PlantType>(plantList);
            }
            public PlantType GetNext()
            {
                return plants.Pop();
            }
        }
    }
}

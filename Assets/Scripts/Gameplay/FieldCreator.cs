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

        public static FieldCreator Instance { get; private set; }

        [SerializeField, InitializationField] private int sizeX;
        [SerializeField, InitializationField] private int sizeY;
        [SerializeField, InitializationField] private int rows;
        [SerializeField, InitializationField] private int columns;
        [SerializeField, InitializationField] private List<PlantGeneration> generation;
        [SerializeField, InitializationField] private Transform root;
        private Pool plantsPooler;
        public int FieldCount { get; set; }

        private void Awake()
        {
            Instance = this;
            if (!Application.isPlaying) return;
            int amount = (sizeX - (sizeX / rows - 1 - (sizeX / rows - 1) / 4)) * (sizeY - (sizeY / columns - 1 - (sizeY / columns - 1) / 4));
            FieldCount = amount;
            PlantRandomManager plantRandom = new(generation, FieldCount);
            plantsPooler = PoolingManager.Instance["Plants"];
            Vector3 anchor = transform.position - new Vector3(sizeX / 2f, sizeY / 2f, 0);
            for (int i = 1; i <= sizeX; i++)
                if (i % (rows + 1) != 0)
                {
                    for (int j = 1; j <= sizeY; j++)
                        if (j % (columns + 1) != 0)
                        {
                            Plant plant = plantsPooler.Extract<Plant>();
                            plant.SetPlant(plantRandom.GetNext());
                            plant.transform.position = anchor + new Vector3(i - .5f, j - .5f, 0);
                        }
                        else AddPoint(anchor + new Vector3(i - .5f, j - .5f, 0));
                }
                else for (int j = 1; j <= sizeY; j++) AddPoint(anchor + new Vector3(i - .5f, j - .5f, 0));
            AstarPath.active.Scan();
            root.position = transform.position;
            BoxCollider2D child1 = root.GetChild(0).GetComponent<BoxCollider2D>();
            child1.transform.localPosition = new(0, sizeY / 2 + 1, 0);
            child1.size = new(sizeX, 1);
            BoxCollider2D child2 = root.GetChild(1).GetComponent<BoxCollider2D>();
            child2.transform.localPosition = new(sizeX / 2 + .5f, 0, 0);
            child2.size = new(1, sizeY);
            BoxCollider2D child3 = root.GetChild(2).GetComponent<BoxCollider2D>();
            child3.transform.localPosition = new(0, -(sizeY / 2) - 1, 0);
            child3.size = new(sizeX, 1);
            BoxCollider2D child4 = root.GetChild(3).GetComponent<BoxCollider2D>();
            child4.transform.localPosition = new(-(sizeX / 2) - .5f, 0, 0);
            child4.size = new(1, sizeY);
        }

#if UNITY_EDITOR
        void Update()
        {
            ResizeField();
        }
#endif

        private void AddPoint(Vector3 pos)
        {
            GameObject child = new();
            child.transform.parent = transform;
            child.transform.position = pos;
        }
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

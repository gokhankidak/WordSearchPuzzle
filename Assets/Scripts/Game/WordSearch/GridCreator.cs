using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace WordSearch
{
    public class GridCreator : MonoBehaviour
    {
        [HideInInspector] public GridObject[,] GridObjects;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float yOffset;
        [SerializeField] private GridObject gridObject;

        // Start is called before the first frame update
        void Start()
        {
            GridObjects = new GridObject[gridSize.x, gridSize.y];
            Camera.main.orthographicSize = ((float)gridSize.x + 2)* Screen.height / Screen.width * 0.5f;

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    var spawnPos = new Vector3(i - gridSize.x / 2 + .5f, j - gridSize.y / 2 + yOffset + .5f);
                    var go = Instantiate(gridObject, spawnPos, quaternion.identity, transform);
                    go.Text.text = ((i + j) % 10).ToString();
                    go.Index = new Vector2Int(i, j);
                    GridObjects[i, j] = go;
                }
            }
        }
    }
}
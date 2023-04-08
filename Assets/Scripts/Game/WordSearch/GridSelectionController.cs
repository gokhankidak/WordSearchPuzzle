using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WordSearch
{
    public class GridSelectionController : MonoBehaviour
    {
        [SerializeField] private WordSearchSO wordSearchSo;

        private Vector3 startPos;
        private bool[,] visitedGrids;
        private GridObject startObject, currentObject;
        private List<Vector3> selectedGrids = new List<Vector3>();
        private List<Vector2Int> selectedIndexes = new List<Vector2Int>();
        private GridStriker gridStriker;

        private void Start()
        {
            gridStriker = GetComponent<GridStriker>();
            ResetValues();
        }

        private void ResetValues()
        {
            visitedGrids = new bool[10, 10];
            startObject = null;
            currentObject = null;
            selectedGrids.Clear();
            selectedIndexes.Clear();
            gridStriker.DrawStrike(selectedGrids);
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform != null && hit.transform.TryGetComponent(out GridObject gridObject))
                {
                    startPos = gridObject.transform.position;
                    startObject = gridObject;
                    selectedGrids.Add(startObject.transform.position);
                    selectedIndexes.Add(startObject.Index);
                    visitedGrids[startObject.Index.x, startObject.Index.y] = true;
                }
            }

            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform != null && hit.transform.TryGetComponent(out GridObject gridObject))
                {
                    if (!CheckForValidGrid(gridObject))
                    {
                        return;
                    }

                    currentObject = gridObject;
                    visitedGrids[currentObject.Index.x, currentObject.Index.y] = true;
                    selectedGrids.Add(currentObject.transform.position);
                    selectedIndexes.Add(currentObject.Index);
                    Debug.Log("Selected grid count : "+selectedGrids.Count);
                    gridStriker.DrawStrike(selectedGrids);
                }
            }

            if (Input.GetMouseButtonUp(0) && startObject != null)
            {
                wordSearchSo.OnWordSearch?.Invoke(selectedIndexes);
                ResetValues();
            }
        }

        private bool CheckForValidGrid(GridObject gridObject)
        {
            if (visitedGrids[gridObject.Index.x, gridObject.Index.y]) return false; // if visited before return false
            if (currentObject == null) return true;
            if (currentObject.Index == gridObject.Index + Vector2Int.up // check for adjacent(valid) movement
                || currentObject.Index == gridObject.Index + Vector2Int.down
                || currentObject.Index == gridObject.Index + Vector2Int.left
                || currentObject.Index == gridObject.Index + Vector2Int.right)
            {
                return true;
            }

            return false;
        }
    }
}
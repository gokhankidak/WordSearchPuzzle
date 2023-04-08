using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WordSearch
{
    public class GridStriker : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float lineLength;
        [SerializeField] private List<LineRenderer> lineRenderers;
        [SerializeField] private WordSearchSO wordSearchSo;

        private Mesh mesh;
        private int lineIndex;

        private void OnEnable()
        {
            wordSearchSo.OnKeepLine += OnWordFind;
        }
        private void OnDisable()
        {
            wordSearchSo.OnKeepLine -= OnWordFind;
        }

        void Start()
        {
            mesh = new Mesh();
        }

        public void DrawStrike(List<Vector3> selectedGrids)
        {
            lineRenderers[lineIndex].positionCount = selectedGrids.Count;
            if (selectedGrids.Count < 2) return;

            var startOffset = (selectedGrids[0] - selectedGrids[1]) / 3;
            var endOffset = (selectedGrids[^1] - selectedGrids[^2]) / 3;

            for (int i = 0; i < selectedGrids.Count; i++)
            {
                if (i == 0) lineRenderers[lineIndex].SetPosition(i, selectedGrids[i] + startOffset);
                else if (i == selectedGrids.Count - 1)
                    lineRenderers[lineIndex].SetPosition(i, selectedGrids[i] + endOffset);
                else lineRenderers[lineIndex].SetPosition(i, selectedGrids[i]);
            }
        }

        private void OnWordFind(bool isFound)
        {
            if (isFound)
            {
                lineIndex++;
                mesh = new Mesh();
            }
            else
            {
                lineRenderers[lineIndex].positionCount = 0;
            }
        }
    }
}
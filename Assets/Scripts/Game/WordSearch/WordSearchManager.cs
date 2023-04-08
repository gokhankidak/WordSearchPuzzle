using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WordSearch
{
    public class WordSearchManager : MonoBehaviour
    {
        [SerializeField] private WordSearchSO wordSearchSo;
        [SerializeField] private List<WordPuzzleLevelData> levelDatas;
        private bool[] isIndexesAvailable;
        private GridCreator gridCreator;

        private void OnEnable()
        {
            wordSearchSo.OnWordSearch += CheckForWord;
        }

        private void Start()
        {
            isIndexesAvailable = new bool[20];
            gridCreator = GetComponent<GridCreator>();
            wordSearchSo.OnLevelStart?.Invoke(levelDatas[0]);
        }

        private void CheckForWord(List<Vector2Int> wordList)
        {
            var word = GetWord(wordList);
            Debug.Log("Selected Word : "+word);
            for (int i = 0; i < levelDatas[0].Words.Count; i++)
            {
                if (levelDatas[0].Words[i] == word || levelDatas[0].Words[i] == Reverse(word))
                {
                    if (!isIndexesAvailable[i])
                    {
                        isIndexesAvailable[i] = true;
                        wordSearchSo.OnRemoveWordFromList?.Invoke(i);
                        wordSearchSo.OnKeepLine?.Invoke(true);
                        return;
                    }
                }
            }
            wordSearchSo.OnKeepLine?.Invoke(false);
        }


        private string GetWord(List<Vector2Int> wordList)
        {
            //horizontal selection
            StringBuilder str = new StringBuilder();

            foreach (var t in wordList)
            {
                str.Append(gridCreator.GridObjects[t.x, t.y].Text.text);
            }

            return str.ToString();
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    [Serializable]
    public struct WordPuzzleLevelData
    {
        public List<string> Words;
    }
}
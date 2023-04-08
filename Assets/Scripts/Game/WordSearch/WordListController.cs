using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WordSearch
{
    public class WordListController : MonoBehaviour
    {
        [SerializeField] private WordSearchSO wordSearchSo;
        [SerializeField] private List<TMP_Text> texts;

        private void OnEnable()
        {
            wordSearchSo.OnLevelStart += SetTexts;
            wordSearchSo.OnRemoveWordFromList += RemoveWordFromList;
        }


        private void SetTexts(WordPuzzleLevelData levelData)
        {
            for (int i = 0; i < levelData.Words.Count; i++)
            {
                texts[i].text = levelData.Words[i];
                texts[i].gameObject.SetActive(true);
            }
        }

        private void RemoveWordFromList(int index)
        {
            texts[index].fontStyle = FontStyles.Strikethrough;
        }
    }
}
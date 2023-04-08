using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WordSearch;

[CreateAssetMenu(menuName = "ScriptableObject/WordSearchSO", fileName = "WordSearchSO")]
public class WordSearchSO : ScriptableObject
{
    public Action<bool> OnKeepLine;
    public Action<int> OnRemoveWordFromList;
    public Action<WordPuzzleLevelData> OnLevelStart;
    public Action<List<Vector2Int>> OnWordSearch;
    public Action OnCheckForWord;
}

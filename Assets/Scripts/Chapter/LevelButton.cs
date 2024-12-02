using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class LevelButton
{
    public string chapterName;
    public Button button;
    public Color enabledColor;
    public Color disabledColor;

    public void UpdateButtonStatus()
    {
        GameChapter gameChapter = GameObject.FindObjectOfType<GameChapter>();
        bool isComplete = gameChapter.IsChapterComplete(chapterName);
        button.interactable = isComplete;
        button.image.color = isComplete? enabledColor : disabledColor;
    }
}

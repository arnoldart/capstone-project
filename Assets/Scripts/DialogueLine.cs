using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private string speakerName;           // Name of the character speaking
    [SerializeField][TextArea(3, 10)] private string dialogueText;  // The dialogue text

    [SerializeField] private Sprite speakerImageA;         // The image of the speaker (main speaking character)
    [SerializeField] private Sprite speakerImageB;         // The image of the secondary character (non-speaking character)

    // Public properties to access the serialized fields
    public string SpeakerName => speakerName;              // The name of the speaker
    public string DialogueText => dialogueText;            // The dialogue text
    public Sprite SpeakerImageA => speakerImageA;          // Image for the speaking character
    public Sprite SpeakerImageB => speakerImageB;          // Image for the non-speaking character
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private List<DialogueLine> dialogueLines;  // List of dialogue lines

    // Public property to access the dialogue lines
    public List<DialogueLine> DialogueLines => dialogueLines;
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRadius = 2f; // Radius for interaction
    private bool isPlayerInRange = false;                 // Tracks if player is in range

    [Header("Dialogue Settings")]
    [SerializeField] private List<DialogueLine> dialogueLines; // List of dialogue lines
    [SerializeField] private GameObject dialogueUI;            // Reference to the dialogue UI
    [SerializeField] private TextMeshProUGUI speakerText;       // UI for speaker name
    [SerializeField] private TextMeshProUGUI dialogueText;      // UI for dialogue content
    [SerializeField] private Image speakerImageA;              // Image for Speaker A
    [SerializeField] private Image speakerImageB;              // Image for Speaker B

    private Queue<DialogueLine> dialogueQueue; // Queue to manage dialogue lines
    private bool isDialogueActive = false;     // Tracks if dialogue is active

    private void Start()
    {
        dialogueQueue = new Queue<DialogueLine>();
        dialogueUI.SetActive(false); // Ensure dialogue UI is hidden initially

        // Ensure both images are hidden initially
        speakerImageA.enabled = false;
        speakerImageB.enabled = false;
    }

    private void Update()
    {
        // Check for interaction input when the player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    private void StartDialogue()
    {
        if (dialogueLines.Count == 0)
        {
            Debug.LogWarning("No dialogue lines assigned for this NPC!");
            return;
        }

        Debug.Log("Dialogue started.");
        isDialogueActive = true;
        dialogueQueue.Clear();

        foreach (var line in dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogueUI.SetActive(true); // Show dialogue UI
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        var currentLine = dialogueQueue.Dequeue();

        // Update the speaker's name and text
        speakerText.text = currentLine.SpeakerName;
        dialogueText.text = currentLine.DialogueText;

        // Define the scales for active and inactive speakers
        Vector3 activeScale = new Vector3(1.0f, 1.0f, 1.0f); // Original size
        Vector3 inactiveScale = new Vector3(0.99f, 0.99f, 1.0f); // 1% smaller

        // Define the colors for active and inactive speakers
        Color activeColor = Color.white; // Bright color for active speaker
        Color inactiveColor = new Color(0.8f, 0.8f, 0.8f, 1.0f); // Slightly darker color for inactive speaker

        // Adjust Speaker A
        if (currentLine.SpeakerImageA != null)
        {
            speakerImageA.sprite = currentLine.SpeakerImageA;
            speakerImageA.enabled = true;

            if (currentLine.IsSpeakerAActive)
            {
                speakerImageA.transform.localScale = activeScale;
                speakerImageA.color = activeColor;
            }
            else
            {
                speakerImageA.transform.localScale = inactiveScale;
                speakerImageA.color = inactiveColor;
            }
        }
        else
        {
            speakerImageA.enabled = false;
        }

        // Adjust Speaker B
        if (currentLine.SpeakerImageB != null)
        {
            speakerImageB.sprite = currentLine.SpeakerImageB;
            speakerImageB.enabled = true;

            if (!currentLine.IsSpeakerAActive)
            {
                speakerImageB.transform.localScale = activeScale;
                speakerImageB.color = activeColor;
            }
            else
            {
                speakerImageB.transform.localScale = inactiveScale;
                speakerImageB.color = inactiveColor;
            }
        }
        else
        {
            speakerImageB.enabled = false;
        }

        // Debug logs for testing
        Debug.Log($"{currentLine.SpeakerName}: {currentLine.DialogueText}");
        Debug.Log($"Speaker A active: {currentLine.IsSpeakerAActive}, Speaker B active: {!currentLine.IsSpeakerAActive}");
    }



    private void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        isDialogueActive = false;
        dialogueUI.SetActive(false); // Hide dialogue UI

        // Clear the UI text
        speakerText.text = "";
        dialogueText.text = "";

        // Hide both speaker images
        speakerImageA.enabled = false;
        speakerImageB.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered interaction range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player left interaction range.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

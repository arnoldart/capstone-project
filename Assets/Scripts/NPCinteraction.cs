using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 10f;  // Radius for interaction
    [SerializeField] private Dialogue dialogue;  // Reference to the dialogue asset
    private Transform playerTransform;  // Reference to the player’s transform

    private void Start()
    {
        // Find the player by tag (make sure the player has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Ensure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the distance between the NPC and the player
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            // Check if player is within interaction radius
            if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F key pressed, starting dialogue.");
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        if (dialogue != null)
        {
            Debug.Log("Starting dialogue with NPC.");
            DialogueManager.instance.StartDialogue(dialogue);  // Start the dialogue from the DialogueManager
        }
        else
        {
            Debug.LogWarning("Dialogue is not assigned to NPC.");
        }
    }

    // Optional: Visualize the interaction radius in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public GameObject instructionPanel; // Referensi ke panel instruksi
    public TMP_Text instructionText; // Untuk TextMeshPro
    private bool isPlayerNearby = false;
   

    void Start()
    {
        // Pastikan panel instruksi tidak aktif di awal
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Jika pemain dekat dan menekan tombol "E", panel akan ditampilkan
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowInstructions();
        }
    }

    private void ShowInstructions()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            instructionText.text = "Controls:\n" +
                                   "- WASD to move \n" +
                                   "- Left-click to attack\n" +
                                   "- Right-click to shoot a projectile\n\n" +
                                   "Mission Level 1:\n" +
                                   "Eliminate all minions in the area and defeat the boss to proceed to the next stage.\n\n" +
                                   "Press the 'Close' button to continue.";
        }
    }

    public void CloseInstructions()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
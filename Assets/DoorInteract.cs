using UnityEngine;
using TMPro;

public class DoorInteract : MonoBehaviour
{
    public int doorID = 1;

    public bool isOpen = true;
    public Collider2D doorBlocker;
    public GameObject statusPanel;
    public TextMeshProUGUI statusText;

    public GameManager gameManager;

    private bool playerNearby = false;

    void Start()
    {
        UpdateDoor();
        HideStatus();

        if (gameManager != null)
        {
            gameManager.SetDoorDone(doorID, !isOpen);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            UpdateDoor();

            if (gameManager != null)
            {
                gameManager.SetDoorDone(doorID, !isOpen);
            }

            ShowStatus(isOpen ? "Door Unlocked" : "Door Locked");
        }
    }

    void UpdateDoor()
    {
        if (doorBlocker != null)
        {
            doorBlocker.enabled = !isOpen;
        }
    }

    void ShowStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }

        if (statusPanel != null)
        {
            statusPanel.SetActive(true);

            CancelInvoke(nameof(HideStatus));
            Invoke(nameof(HideStatus), 2f);
        }
    }

    void HideStatus()
    {
        if (statusPanel != null)
        {
            statusPanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
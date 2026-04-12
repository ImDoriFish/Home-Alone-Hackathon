using UnityEngine;
using TMPro;

public class LightSwitchInteract : MonoBehaviour
{
    public bool lightsOn = false;
    public GameObject darkOverlay;

    public GameObject statusPanel;
    public TextMeshProUGUI statusText;

    public GameManager gameManager;

    private bool playerNearby = false;

    void Start()
    {
        UpdateLights();
        HideStatus();

        if (gameManager != null)
        {
            gameManager.SetLightDone(lightsOn);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            lightsOn = !lightsOn;
            UpdateLights();

            if (gameManager != null)
            {
                gameManager.SetLightDone(lightsOn);
            }

            ShowTemporaryStatus(lightsOn ? "Light On" : "Light Off");
        }
    }

    void UpdateLights()
    {
        if (darkOverlay != null)
        {
            darkOverlay.SetActive(!lightsOn);
        }
    }

    void ShowTemporaryStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }

        if (statusPanel != null)
        {
            statusPanel.SetActive(true);
            CancelInvoke(nameof(ShowIdlePrompt));
            CancelInvoke(nameof(HideStatus));
            Invoke(nameof(HideStatus), 2f);
        }
    }

    void ShowIdlePrompt()
    {
        if (playerNearby && statusText != null && statusPanel != null)
        {
            statusText.text = "Light Switch";
            statusPanel.SetActive(true);
        }
    }

    void HideStatus()
    {
        if (statusPanel != null)
        {
            statusPanel.SetActive(false);
        }

        if (playerNearby)
        {
            Invoke(nameof(ShowIdlePrompt), 0.05f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (statusText != null)
            {
                statusText.text = "Light Switch";
            }

            if (statusPanel != null)
            {
                statusPanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            CancelInvoke(nameof(ShowIdlePrompt));
            CancelInvoke(nameof(HideStatus));
            HideStatus();
        }
    }
}
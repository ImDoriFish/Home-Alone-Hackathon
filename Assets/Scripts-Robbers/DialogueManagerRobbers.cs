using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManagerRobbers : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI openingText;
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;

    [Header("Next Scene")]
    public string nextScene = "Floor1";

    private int currentLine = 0;
    private bool started = false;
    private bool canAdvance = false;

    // true = Harry, false = Marv
    private (string speaker, string line)[] lines = {
        ("Harry", "Christmas is the perfect time to steal, everyone will be on vacation."),
        ("Marv", "I think the McAllisters are going out of town, they seemed busy packing this morning."),
        ("Harry", "Hmm, let's go checkout their house!")
    };

    void Start()
    {
        dialoguePanel.SetActive(false);
        Invoke("EnableAdvance", 0.5f);
    }

    void Update()
    {
        if (canAdvance && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            if (!started)
            {
                // first click — hide opening text, show dialogue
                openingText.gameObject.SetActive(false);
                dialoguePanel.SetActive(true);
                ShowLine();
                started = true;
            }
            else
            {
                currentLine++;
                if (currentLine >= lines.Length)
                {
                    SceneManager.LoadScene(nextScene);
                    return;
                }
                ShowLine();
            }
        }
    }

    void ShowLine()
    {
        speakerName.text = lines[currentLine].speaker + ":";
        dialogueText.text = lines[currentLine].line;
    }

    void EnableAdvance()
    {
        canAdvance = true;
    }
}
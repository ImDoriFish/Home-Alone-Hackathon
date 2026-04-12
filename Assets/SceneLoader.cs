using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadRobber()
    {
        SceneManager.LoadScene("Robber");
    }
}
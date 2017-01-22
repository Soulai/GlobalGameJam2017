using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchListener : MonoBehaviour
{
    public string NextSceneName;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNavigation : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        TankShooter,
    }

    public void OpenGameScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}

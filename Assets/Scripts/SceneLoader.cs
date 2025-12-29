using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public enum SceneEnum
    {
        MainMenuScene,
        GamePlayScene,
        GameOverScene
    }
    public static void LoadScene(SceneEnum sceneEnum)
    {
        SceneManager.LoadScene(sceneEnum.ToString());
    }
}

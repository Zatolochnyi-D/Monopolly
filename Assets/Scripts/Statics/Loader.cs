using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scenes
    {
        MainMenu = 0,
        Game = 1,
    }

    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}

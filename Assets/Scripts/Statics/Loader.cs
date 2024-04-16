using System;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scenes
    {
        MainMenu = 0,
        Game = 1,
    }

    public static event Action<Scenes> OnSceneChangeStatic;

    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
        OnSceneChangeStatic?.Invoke(scene);
    }
}

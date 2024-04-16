using UnityEngine;
using UnityEngine.UI;

public class FromMainMenuToGame : MonoBehaviour
{
    void Awake()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.Game);
        });
    }
}

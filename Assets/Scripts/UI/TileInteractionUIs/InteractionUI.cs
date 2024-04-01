using UnityEngine;

public abstract class InteractionUI : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public abstract void Iteract(PlayerLogic player);
}

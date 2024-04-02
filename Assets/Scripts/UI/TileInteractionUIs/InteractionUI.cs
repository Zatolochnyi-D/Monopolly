using UnityEngine;

public abstract class InteractionUI : MonoBehaviour
{
    protected PlayerLogic currentPlayer;

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

    public virtual void EndTurn()
    {
        TurnManager.Instance.EndTurn();
    }

    public abstract void Iteract(PlayerLogic player);
}

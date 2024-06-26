using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    protected PlayerLogic.PlayerCommand playerCommand;

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

    public virtual void Close()
    {
        Hide();
        EndTurn();
    }

    public abstract void Interact();
}

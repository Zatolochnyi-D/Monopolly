using UnityEngine;

public class PlayerLogic : MonoBehaviour
{    
    void Start()
    {
        transform.position = MapManager.Instance.StartTile.transform.position + new Vector3(0, 0, -1);
    }
}

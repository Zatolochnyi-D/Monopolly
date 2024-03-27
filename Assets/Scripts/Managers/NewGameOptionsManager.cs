using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameOptionsManager : MonoBehaviour
{
    public static NewGameOptionsManager Instance { get; private set; }

    [SerializeField] private Sprite[] pawnVisuals;

    void Awake()
    {
        Instance = this;
    }
}

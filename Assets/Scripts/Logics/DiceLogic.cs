using System;
using System.Linq;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    [SerializeField] private Transform[] diceSides;
    [SerializeField] private Transform resetPoint;
    [SerializeField] private Transform target;
    // [SerializeField] private Vector3 allowedDeviation;
    

    private Rigidbody diceRigidbody;

    void Awake()
    {
        diceRigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        transform.position = resetPoint.position;
    }

    private int GetRolledNumber()
    {
        int rolledNumber = Array.IndexOf(diceSides, diceSides.Max(x => x.position.y)) + 1;

        return rolledNumber;
    }
}

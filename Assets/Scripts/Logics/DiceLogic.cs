using System;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    public event Action<int> OnDiceLanded;

    [SerializeField] private Transform[] diceSides;
    [SerializeField] private Transform resetPoint;
    [SerializeField] private Transform target;
    [SerializeField] private float throwStrength;
    [SerializeField] private float maxRotationSpeed;
    
    private Rigidbody diceRigidbody;

    private float isStoppedCheckDelay = 1.0f; // check speed after some delay, because early trigger on first frame
    private float throwTimeOut = 7.0f;
    private float timeFromBeingThrowed = 0.0f;
    private float distanceFromTarget = 5.0f;

    public bool IsStopped => diceRigidbody.velocity.magnitude == 0.0f;

    void Awake()
    {
        diceRigidbody = GetComponent<Rigidbody>();

        Reset();
    }

    void FixedUpdate()
    {
        if (!diceRigidbody.isKinematic)
        {
            timeFromBeingThrowed += Time.fixedDeltaTime;
            if (IsStopped && timeFromBeingThrowed > isStoppedCheckDelay)
            {
                OnDiceLanded?.Invoke(GetRolledNumber());
                timeFromBeingThrowed = 0.0f;
                diceRigidbody.isKinematic = true;
            }

            if (timeFromBeingThrowed > throwTimeOut)
            {
                // dice fell out of the field
                diceRigidbody.velocity = new Vector3(0f, 0f, 0f);
                timeFromBeingThrowed = 0f;
                transform.position = target.position + new Vector3(0f, distanceFromTarget, 0f);
            }
        }
    }

    public void Reset()
    {
        transform.position = resetPoint.position;
        diceRigidbody.isKinematic = true;
    }

    public void Throw()
    {
        diceRigidbody.isKinematic = false;

        Vector3 movementVector = (target.position - transform.position).normalized;

        diceRigidbody.AddForce(movementVector * throwStrength, ForceMode.VelocityChange);

        Vector3 rotationSpeed = new(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
        rotationSpeed *= maxRotationSpeed;
        diceRigidbody.angularVelocity = rotationSpeed;
    }

    public int GetRolledNumber()
    {
        int rolledNumber = 1;
        float highestPoint = diceSides[0].position.y;

        for (int i = 0; i < diceSides.Length; i++)
        {
            if (diceSides[i].position.y > highestPoint)
            {
                highestPoint = diceSides[i].position.y;
                rolledNumber = i + 1;
            }
        }

        return rolledNumber;
    }
}

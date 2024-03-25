using System;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    public event Action<int> OnMovementStopped;

    [SerializeField] private Transform[] diceSides;
    [SerializeField] private Transform resetPoint;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 allowedDeviation;
    [SerializeField] private float throwStrength;
    [SerializeField] private float maxRotationSpeed;
    
    private Rigidbody diceRigidbody;

    private float isStoppedCheckDelay = 1.0f; // when dice are thrown, on first frame their speed is 0, which means stop event triggers immediately
    private float isStoppedCheckTimer = 0.0f; // check dice speed a second after they were thrown

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
            isStoppedCheckTimer += Time.fixedDeltaTime;
            if (IsStopped && isStoppedCheckTimer > isStoppedCheckDelay)
            {
                OnMovementStopped?.Invoke(GetRolledNumber());
                isStoppedCheckTimer = 0.0f;
                diceRigidbody.isKinematic = true;
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

        (float xMin, float xMax) = (-allowedDeviation.x / 2.0f, allowedDeviation.x / 2.0f);
        (float yMin, float yMax) = (-allowedDeviation.y / 2.0f, allowedDeviation.y / 2.0f);
        (float zMin, float zMax) = (-allowedDeviation.z / 2.0f, allowedDeviation.z / 2.0f);

        movementVector.x += UnityEngine.Random.Range(xMin, xMax);
        movementVector.y += UnityEngine.Random.Range(yMin, yMax);
        movementVector.z += UnityEngine.Random.Range(zMin, zMax);

        diceRigidbody.AddForce(movementVector * throwStrength, ForceMode.VelocityChange);

        Vector3 rotationSpeed = new(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
        rotationSpeed *= maxRotationSpeed;
        diceRigidbody.angularVelocity = rotationSpeed;
    }

    public int GetRolledNumber()
    {
        int rolledNumber = 0;
        float y = diceSides[0].position.y;

        for (int i = 0; i < diceSides.Length; i++)
        {
            if (diceSides[i].position.y > y)
            {
                y = diceSides[i].position.y;
                rolledNumber = i + 1;
            }
        }

        return rolledNumber;
    }

    [ContextMenu("Rethrow")]
    public void Rethrow()
    {
        Reset();
        Throw();
    }
}

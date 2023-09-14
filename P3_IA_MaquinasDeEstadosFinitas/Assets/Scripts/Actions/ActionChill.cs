// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Entity wandering & chilling around when nobody around, action logic
/// </summary>
public class ActionChill : IAction
{
    GameObject entity_gameObject;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;

    /// <summary>
    ///     Movement values towards Citizen - Zombie => Chase - Escape
    /// </summary>
    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float movementSpeed = 500;
    public float attackSpeed = 500;
    public float maxSpeed = 10f;
    [SerializeField] bool randmRotateOnStart = false;

    /// <summary>
    ///     Controls what part of the routine must be played
    /// </summary>
    float chillingTimer = 0;
    /// <summary>
    ///     Routine randomly generated values
    /// </summary>
    int walkWait;
    int walkTime;
    int rotateWait;
    int rotateOrNot;
    int rotationTime;
    float rotationQuantity;


    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
    }
    /// <summary>
    ///     Visual feedback depending on entity & action logic
    /// </summary>
    public override void Act()
    {
        if (entityStats.gameObject.tag == "Hunter")
        {
            visualFeedback.stateImage_attacking.gameObject.SetActive(false);
            visualFeedback.stateImage_following.gameObject.SetActive(false);
            visualFeedback.stateImage_searching.gameObject.SetActive(true);
            visualFeedback.stateImage_resting.gameObject.SetActive(false);
        }
        if (entityStats.gameObject.tag == "Prey")
        {
            entityStats.isEscaping = false;

            visualFeedback.stateImage_escaping.gameObject.SetActive(false);
            visualFeedback.stateImage_chilling.gameObject.SetActive(true);
            visualFeedback.stateImage_lookingOut.gameObject.SetActive(false);
            visualFeedback.stateImage_resting.gameObject.SetActive(false);
        }

        ChillRoutineLogic();
    }

    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    void ChillRoutineLogic()
    {
        if (chillingTimer <= 0)
        {
            walkWait = Random.Range(1, 3);
            walkTime = walkWait + Random.Range(0, 3);
            rotateWait = walkTime + Random.Range(0, 3);
            rotateOrNot = Random.Range(1, 2);
            rotationTime = rotateWait + Random.Range(0, 3);
            rotationQuantity = Random.Range(-180, 180);

            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        chillingTimer += Time.deltaTime;
        Wander();
    }
    /// <summary>
    ///     Move around logic
    /// </summary>
    void Wander()
    {
        entity_gameObject = rb.gameObject;

        if (chillingTimer <= walkWait)
            return;

        if (chillingTimer <= walkTime)
        {
            Vector3 acceleration = entity_gameObject.transform.forward * movementSpeed * Time.fixedDeltaTime;
            acceleration.y = 0;
            rb.velocity = acceleration;
            return;
        }
        if (chillingTimer <= rotateWait)
            return;

        if (rotateOrNot == 1)
        {
            if (chillingTimer <= rotationTime)
            {
                entity_gameObject.transform.Rotate(entity_gameObject.transform.up * Time.deltaTime * rotationQuantity);
                return;
            }
        }
        chillingTimer = 0;
    }
}

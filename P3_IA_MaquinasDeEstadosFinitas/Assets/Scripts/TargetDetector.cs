// Miguel Rodríguez Gallego
using UnityEngine;

/// <summary>
///     Detects close targets
/// </summary>
public class TargetDetector : MonoBehaviour
{
    /// <summary>
    ///     Target & direction to follow
    /// </summary>
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 lastPositionSeen;

    [Header("Detection parameters")]
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] int seeTargetRange = 20;
    [SerializeField] Collider[] targetsCollider;
    Transform closestVisibleTarget;

    [Header("Closest taget parameters")]
    [SerializeField] int seeClosestTargetsRange = 15;
    [HideInInspector] public GameObject veryCloseTarget;
    [SerializeField] Collider[] targetsVeryCloseCollider;
    Transform closestTarget;

    /// <summary>
    ///     Constantly try to find if there's a citizen nearby
    /// </summary>
    void Update()
    {
        FindTarget();
    }
    /// <summary>
    ///     If there's a citizen nearby, this method instantly:
    ///         - Gets closer entity
    ///         - Adds him to targets list of citizens
    /// </summary>
    public void FindTarget()
    {
        FindTargets_CanSee();
        FindTargets_CanAttack();
    }
    /// <summary>
    ///     When prey is near, detect it to walk towards it
    /// </summary>
    void FindTargets_CanSee()
    {
        targetsCollider = Physics.OverlapSphere(transform.position, seeTargetRange, targetLayerMask);

        if (targetsCollider.Length != 0)
        {
            closestVisibleTarget = GetClosestEntity(targetsCollider);
            target = closestVisibleTarget.gameObject;
            lastPositionSeen = closestVisibleTarget.gameObject.transform.position; // Gets last position of the entitytoZ
        }
        else
        {
            targetsCollider = null;   // No citizen around in sight
            target = null;
        }
    }
    /// <summary>
    ///     When prey is extremely near, detect it and attack it
    /// </summary>
    void FindTargets_CanAttack()
    {
        targetsVeryCloseCollider = Physics.OverlapSphere(transform.position, seeClosestTargetsRange, targetLayerMask);

        if (targetsVeryCloseCollider.Length != 0)
        {
            closestTarget = GetClosestEntity(targetsVeryCloseCollider);
            veryCloseTarget = closestTarget.gameObject;
            lastPositionSeen = closestTarget.gameObject.transform.position; // Gets last position of the entitytoZ
        }
        else
        {
            targetsVeryCloseCollider = null;   // No citizen around in sight
            veryCloseTarget = null;
        }
    }

    /// <summary>
    ///     Gets closer entity in checking collider
    /// </summary>
    Transform GetClosestEntity(Collider[] entity)
    {
        Collider tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider t in entity)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }
}

// Miguel Rodríguez Gallego
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Detects close targets of same entity types
/// </summary>
public class TargetDetector_Colleague : MonoBehaviour
{
    EntityStats entityStats;
    StatesManager statesManager;
    [SerializeField] State panicState;
    [SerializeField] State escapeState;

    /// <summary>
    ///     Target & direction to follow
    /// </summary>
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 lastPositionSeen;

    [Header("Detection parameters")]
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] int seeTargetRange = 20;
    [SerializeField] List<Collider> targetsCollider;
    Transform closestVisibleTarget;

    [Header("Closest taget parameters")]
    [SerializeField] int seeClosestTargetsRange = 15;
    [HideInInspector] public GameObject veryCloseTarget;
    [SerializeField] List<Collider> targetsVeryCloseCollider;
    Transform closestTarget;

    void Awake()
    {
        entityStats = GetComponent<EntityStats>();
    }

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
        targetsCollider = Physics.OverlapSphere(transform.position, seeTargetRange, targetLayerMask).ToList();
        for (int i = 0; i < targetsCollider.Count; i++)
            if (targetsCollider[i].gameObject == gameObject)
                targetsCollider.RemoveAt(i);
        //targetsCollider.RemoveAt(0);

        if (targetsCollider.Count != 0)
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
        targetsVeryCloseCollider = Physics.OverlapSphere(transform.position, seeClosestTargetsRange, targetLayerMask).ToList();
        for (int i = 0; i < targetsVeryCloseCollider.Count; i++)
            if (targetsVeryCloseCollider[i].gameObject == gameObject)
                targetsVeryCloseCollider.RemoveAt(i);
        //targetsVeryCloseCollider.RemoveAt(0);

        if (targetsVeryCloseCollider.Count != 0)
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
    ///     Entity near is paniquing? => then panic
    /// </summary>
    void DetectIsPaniquing()
    {
        // Detect if is paniquing
        if (target != null)
            if (target.GetComponent<EntityStats>().gameObject.GetComponentInChildren<StatesManager>().activeState == escapeState)
            {
                statesManager.activeState = panicState;
            }


        if (veryCloseTarget != null)
            if (veryCloseTarget.GetComponent<EntityStats>().gameObject.GetComponentInChildren<StatesManager>().activeState == escapeState)
            {
                statesManager.activeState = panicState;
            }

        else
            entityStats.isPaniquing = false;
    }

    /// <summary>
    ///     Gets closer entity in checking collider
    /// </summary>
    Transform GetClosestEntity(List<Collider> entity)
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

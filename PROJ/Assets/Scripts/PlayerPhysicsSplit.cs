using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsSplit : MonoBehaviour
{
    //modifierar .y-värdet på velocity, kan alltså inte vara en property
    public Vector3 velocity;
    //går denna att göra private istället? 
    [SerializeField] private LayerMask collisionMask;
    private Vector3 colliderTopHalf, colliderBottomHalf;

    public RaycastHit groundHitInfo { get; private set; }

    [Header("Values")]

    [SerializeField] public float maxSpeed;
    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float gravityWhenFalling = 10f;
    [SerializeField] private float currentGravity;

    //Suggestions for more control
    /*[SerializeField] private float inputSensitivity ;  Affect left and right input values? 
     * PowerOf will not be obvious to designers, should probably write a separate class containing the easing functions instead.. although that will lead to slower testing.
     * actually rotate when turning
     */
    [Header("Smoothing")]
    [SerializeField] private float smoothingMaxDistance = 2f;
    [SerializeField] private int powerOf = 2;
    [SerializeField] private float surfThreshold;

    [Header("Friktion")]
    [Range(0f, 1f)] [SerializeField] private float staticFrictionCoefficient = 0.5f;
    [Range(0f, 1f)] [SerializeField] private float kineticFrictionCoefficient = 0.35f;
    [Range(0f, 1f)] [SerializeField] private float airResistance = 0.35f;

    private CapsuleCollider attachedCollider;
    private Vector3 startPosition;


    private void OnEnable()
    {
        startPosition = transform.position;
        attachedCollider = GetComponent<CapsuleCollider>();

        if (GetComponent<PlayerController>())
            maxSpeed = GetComponent<PlayerController>().GetMaxSpeed();
    }

    public void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + velocity);
        AddGravity();
        CheckForCollisions(0);
        SplitCollisionCheck(0);
        ClampSpeed();
        transform.position += velocity * Time.deltaTime;

        MoveOutOfGeometry();
    }
    private void SeparateInput()
    {
        if (velocity.magnitude > surfThreshold)
            //Do the surf
            return;
    }
    private void SplitCollisionCheck(int i)
    {
        if (velocity.magnitude < surfThreshold)
            return;

        float castLength = velocity.magnitude * Time.deltaTime + skinWidth;
        Physics.SphereCast(colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit smoothingCastHitInfo, castLength + smoothingMaxDistance, collisionMask);
        if (smoothingCastHitInfo.collider && smoothingCastHitInfo.collider.isTrigger == false)
        {
            Vector3 smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal) * Mathf.Pow((1 +  -(smoothingCastHitInfo.distance / smoothingMaxDistance)), powerOf);
            velocity += new Vector3(0, smoothingNormalForce.y, 0);
            /*if (i < 10)
                SplitCollisionCheck(i + 1);*/
        }

        //If we recieve no hit on the spherecast, we're not grounded? and therefore should use gravityWhenFalling
        //Should probably depend on a groundcheck and not any of these collision detectors
        else
            currentGravity = gravityWhenFalling;
    }

    private void CheckForCollisions(int i)
    {

        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {

            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);
            velocity += new Vector3(normalForce.x, 0 , normalForce.z);


            ApplyFriction(normalForce);

            if (i < 10)
                CheckForCollisions(i + 1);
        }
    }
    
    private void MoveOutOfGeometry()
    {
        Collider[] colliders = OverlapCast(transform.position);

        if (colliders.Length < 1)
        {
            return;
        }
           

        foreach (Collider currentCollider in colliders)
        {
            if (currentCollider == attachedCollider || currentCollider.isTrigger)
                continue;
            Physics.ComputePenetration(attachedCollider,
                                        transform.position,
                                        transform.rotation,
                                        currentCollider,
                                        currentCollider.transform.position,
                                        currentCollider.transform.rotation,
                                        out Vector3 separationVector,
                                               out float distance);

            Vector3 separationVectorDistance = separationVector * distance;
            transform.position += separationVectorDistance + separationVectorDistance.normalized * skinWidth;
            velocity += PhysicsFunctions.NormalForce3D(velocity, separationVector);
        }

    }

    #region Friction, Resistance and Gravity
    protected void AddGravity()
    {
        Vector3 gravityMovement = currentGravity * Vector3.down * Time.deltaTime;
        velocity += gravityMovement;
        currentGravity = gravity;
    }
    public void ApplyFriction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
            velocity = Vector3.zero;
        else
        {
            velocity -= velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
        }
        ApplyAirResistance();
    }
    public void ApplyAirResistance() { velocity *= Mathf.Pow(airResistance, Time.deltaTime); }
    #endregion
    public void AddForce(Vector3 input)
    {
        velocity += input.magnitude < inputThreshold ? Vector3.zero : input * Time.deltaTime;
    }
    public Vector3 GetXZMovement()
    {
        return new Vector3(velocity.x, 0, velocity.z);
    }
    private void ClampSpeed()
    {
        float temp = velocity.y;
        velocity = maxSpeed != 0 ? Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), maxSpeed) : velocity;
        velocity.y = temp;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
    #region capsuleCasts
    public RaycastHit CastCollision(Vector3 origin, Vector3 direction, float distance)
    {
        UpdateColliderPosition(origin);

        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf, attachedCollider.radius, direction.normalized, out var hitInfo, distance, collisionMask);

        return hitInfo;
    }
    public Collider[] OverlapCast(Vector3 currentPosition)
    {
        UpdateColliderPosition(currentPosition);
        return Physics.OverlapCapsule(colliderTopHalf, colliderBottomHalf, attachedCollider.radius, collisionMask);
    }
    /*  public Collider[] OverlapCastSphere(Vector3 currentPosition)
      {
          return Physics.OverlapSphere(currentPosition + sphereCollider.center, sphereCollider.radius, collisionMask, QueryTriggerInteraction.Collide);
      }*/
    public Collider[] OverlapCastBox(Vector3 currentPosition)
    {
        return Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, collisionMask);
    }
    private void UpdateColliderPosition(Vector3 currentPosition)
    {
        colliderTopHalf = (currentPosition + attachedCollider.center) + Vector3.up * (attachedCollider.height / 2 - attachedCollider.radius);
        colliderBottomHalf = (currentPosition + attachedCollider.center) + Vector3.down * (attachedCollider.height / 2 - attachedCollider.radius);
    }
    #endregion

    /* private void MoveOutOfGeometrySphere()
 {
     //Collider[] colliders = OverlapCast(transform.position);
     Collider[] colliders = OverlapCastSphere(transform.position);
     Debug.Log("Colliders from overlapcastsphere.." + colliders.Length);
     if (colliders.Length < 1) return;

     foreach (Collider currentCollider in colliders)
     {
         if (currentCollider == attachedCollider || currentCollider == sphereCollider || currentCollider.isTrigger)
             continue;
         Physics.ComputePenetration(attachedCollider,
                                     transform.position,
                                     transform.rotation,
                                     currentCollider,
                                     currentCollider.transform.position,
                                     currentCollider.transform.rotation,
                                     out Vector3 separationVector,
                                            out float distance);

         Vector3 separationVectorDistance = separationVector * distance;
         transform.position += separationVectorDistance + separationVectorDistance.normalized * skinWidth;
         Vector3 velocityToApply = PhysicsFunctions.NormalForce3D(velocity, separationVector);
         velocity += velocityToApply;

     }

 }*/
}


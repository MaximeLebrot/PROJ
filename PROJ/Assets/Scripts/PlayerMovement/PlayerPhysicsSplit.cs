using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsSplit : MonoBehaviour
{
    //modifierar .y-v�rdet p� velocity, kan allts� inte vara en property
   

    public Vector3 velocity;
    public RaycastHit groundHitInfo { get; private set; }

    [Header("Values")]    
    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    [SerializeField] private float gravityWhenFalling = 10f;
    [SerializeField] private float currentGravity;
    [SerializeField] private LayerMask collisionMask;

    public float SurfThreshold { get => surfThreshold; }

    //Public variables temporary for debugging via inspector
    public float maxSpeed = 12f;
    public float gravity = 9.81f;

    public float smoothingMaxDistance = 3f;
    public int powerOf = 2;
    public float surfThreshold = 8;

    public float staticFrictionCoefficient = 0.5f;
    public float kineticFrictionCoefficient = 0.35f;
    public float airResistance = 0.35f;

    private CapsuleCollider attachedCollider;
    private Vector3 startPosition;
    private Vector3 colliderTopHalf, colliderBottomHalf;
    private Rigidbody rigidbody;


    private void OnEnable()
    {
        startPosition = transform.position;
        attachedCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        AddGravity();
        ClampSpeed();
        MoveOutOfGeometry();
        ClampSpeed();
    }
    private void Update()
    {     
        transform.position += velocity * Time.deltaTime;
    }
    public void GlideInput()
    {
        CheckForCollisions(0);
        SplitCollisionCheck(0);
        Debug.DrawLine(transform.position, transform.position + velocity, Color.red);      
    }
    public void WalkInput()
    {
        CheckForCollisionsWalk(0);
        Debug.DrawLine(transform.position, transform.position + velocity, Color.red);
    }
    public void SetValues(ControllerValues values)
    {
        Debug.Log("SetValues " + values.GetType());
        //Friction and air resistance
        staticFrictionCoefficient = values.staticFriction;
        kineticFrictionCoefficient = values.kineticFriction;
        airResistance = values.airResistance;

        maxSpeed = values.maxSpeed;
        gravity = values.gravity;

        //Glide
        if (typeof(GlideValues) == values.GetType())
        {
            GlideValues glideValues = (GlideValues)values;
            smoothingMaxDistance = glideValues.smoothingMaxDistance;
            powerOf = glideValues.powerOf;
            surfThreshold = glideValues.surfThreshold;
        }
    }

    private void SplitCollisionCheck(int i)
    {
        float castLength = velocity.magnitude * Time.fixedDeltaTime + skinWidth;
        Physics.SphereCast(colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit smoothingCastHitInfo, castLength + smoothingMaxDistance, collisionMask);
        if (smoothingCastHitInfo.collider && smoothingCastHitInfo.collider.isTrigger == false)
        {
            Vector3 smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal) * Mathf.Pow((1 +  -(smoothingCastHitInfo.distance / smoothingMaxDistance)), powerOf);
            velocity += new Vector3(0, smoothingNormalForce.y, 0);
        }
        /*else
            currentGravity = gravityWhenFalling;
        if (i < 10)
            SplitCollisionCheck(i + 1);*/
    }
    private void CheckForCollisions(int i)
    {
        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.fixedDeltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {
            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);

            //Do not apply normal force on y-axis if gliding, that will be done by split collision check
            Vector3 normalForceApplication = new Vector3(normalForce.x, 0, normalForce.z);
            velocity += normalForceApplication;
            ApplyFriction(normalForceApplication);

            if (i < 10)
                CheckForCollisions(i + 1);
        }
    }
    private void CheckForCollisionsWalk(int i)
    {
        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.fixedDeltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {
            // Calculate the allowed distance to the collision point
            float distanceToColliderNeg = skinWidth / Vector3.Dot(velocity.normalized, hitInfo.normal);
            float allowedMovementDistance = hitInfo.distance + distanceToColliderNeg;

            // Are we allowed to move further than we are able to this frame? 
            if (allowedMovementDistance > velocity.magnitude * Time.fixedDeltaTime)
            {
                return;
            }
            // Are we allowed to move forward?
            if (allowedMovementDistance > 0)
            {
                transform.position += velocity.normalized * allowedMovementDistance;
            }

            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal);

            velocity += -hitInfo.normal * (hitInfo.distance - skinWidth);
            velocity += normalForce;

            ApplyFriction(normalForce);

            if (i < 10)
                CheckForCollisionsWalk(i + 1);
        }
    }
    private void MoveOutOfGeometry()
    {
       Collider[] colliders = OverlapCast(transform.position);
        if (colliders.Length < 1)
            return;   

        foreach (Collider currentCollider in colliders)
        {
            if (currentCollider == attachedCollider || currentCollider.isTrigger)
                continue;
            Physics.ComputePenetration( attachedCollider,
                                        transform.position,
                                        transform.rotation,
                                        currentCollider,
                                        currentCollider.transform.position,
                                        currentCollider.transform.rotation,
                                        out Vector3 separationVector,
                                               out float distance);

            Vector3 separationVectorDistance = separationVector * distance;
            transform.position += separationVectorDistance + separationVectorDistance.normalized * skinWidth;


            velocity += PhysicsFunctions.NormalForce3D(velocity, separationVector);;
        }
    }

    #region Friction, Resistance and Gravity
    protected void AddGravity()
    {
        Vector3 gravityMovement = currentGravity * Vector3.down * Time.fixedDeltaTime;
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
    public void ApplyAirResistance() { velocity *= Mathf.Pow(airResistance, Time.fixedDeltaTime); }
    #endregion
    public void AddForce(Vector3 input)
    {
        velocity += input.magnitude < inputThreshold ? Vector3.zero : input * Time.fixedDeltaTime;
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
    #region CollisionCast

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
    private void UpdateColliderPosition(Vector3 currentPosition)
    {
        colliderTopHalf = (currentPosition + attachedCollider.center) + Vector3.up * (attachedCollider.height * 0.5f - attachedCollider.radius);
        colliderBottomHalf = (currentPosition + attachedCollider.center) + Vector3.down * (attachedCollider.height * 0.5f - attachedCollider.radius);
    }
    #endregion

}


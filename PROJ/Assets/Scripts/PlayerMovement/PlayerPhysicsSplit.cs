using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsSplit : MonoBehaviour
{
    const int MAX_ITER = 10;
    const int MOVE_OUT_ITERATIONS = 5;

    public Vector3 velocity;
    public RaycastHit groundHitInfo { get; private set; }

    [Header("Values")]
    [SerializeField] private float glideHeight = 0.5f;
    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    [SerializeField] private float currentGravity;
    [SerializeField] private float airControl = 0.2f;
    [SerializeField] private float minimumPenetrationForPenalty = 0.01f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float gravityWhenFalling = 10f;

    //Properties
    public float SurfThreshold { get => surfThreshold; }
    public float AirControl { get => airControl; }

    //Public variables temporary for debugging via inspector
    //pls dont judge
    [Header("Values set by States")]
    public float maxSpeed = 12f;
    public float gravity = 9.81f;

    public float smoothingMaxDistance = 3f;
    public int powerOf = 2;
    public float surfThreshold = 8;

    public float staticFrictionCoefficient = 0.5f;
    public float kineticFrictionCoefficient = 0.35f;
    public float airResistance = 0.35f;

    //Collision
    private CapsuleCollider attachedCollider;
    private Vector3 colliderTopHalf, colliderBottomHalf;

    private bool isGliding;
    private float glideNormalForceMargin = 1.1f;
    private float setValuesLerpSpeed = 2f;
    private void OnEnable()
    {
        attachedCollider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        AddGravity();
        CollisionCheck();
        ClampSpeed();
        Debug.DrawLine(transform.position, transform.position + velocity * Time.deltaTime, Color.red);
    }
    public void CollisionCheck()
    {
        if (isGliding)
            SmoothingCollisionCheck(0);
        else
            CheckForCollisions(0);
    }
    public void SetValues(ControllerValues values)
    {
        StartCoroutine("LerpValues", values);
    }
    private IEnumerator LerpValues(ControllerValues values)
    {
        float time = 0f;

        //Glide, remove this entirely?? 
        if (typeof(GlideValues) == values.GetType())
        {
            GlideValues glideValues = (GlideValues)values;
            smoothingMaxDistance = glideValues.smoothingMaxDistance;
            powerOf = glideValues.powerOf;
            surfThreshold = glideValues.surfThreshold;
        }

        while (time < setValuesLerpSpeed)
        {
            //Friction and air resistance
            staticFrictionCoefficient = Mathf.Lerp(staticFrictionCoefficient, values.staticFriction, time * (1 / setValuesLerpSpeed));
            kineticFrictionCoefficient = Mathf.Lerp(kineticFrictionCoefficient, values.kineticFriction, time * (1 / setValuesLerpSpeed));
            airResistance = Mathf.Lerp(airResistance, values.airResistance, time * (1 / setValuesLerpSpeed));

            maxSpeed = Mathf.Lerp(maxSpeed, values.maxSpeed, time * (1 / setValuesLerpSpeed));
            gravity = Mathf.Lerp(gravity, values.gravity, time * (1 / setValuesLerpSpeed));          
            
            time += Time.deltaTime;
            yield return null;
        }
       
    }
    /// <summary>
    /// Divides the collision into XZ & Y-components, to be able to apply the smoothing to normalforce along only the y-axis
    /// </summary>
    /// <param name="i"></param>
    private void SmoothingCollisionCheck(int i)
    {
        float castLength = velocity.magnitude * Time.deltaTime + skinWidth;
        Physics.SphereCast(colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit smoothingCastHitInfo, castLength + smoothingMaxDistance, collisionMask);
        if (smoothingCastHitInfo.collider && smoothingCastHitInfo.collider.isTrigger == false)
        {
            Vector3 smoothingNormalForce;
            if (smoothingCastHitInfo.distance <= castLength)
            {
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal);
                Debug.Log("Distance is less than castLength, using full normal force, sepa");
            }
            //glideNormalForceMargin seems to alleviate the problem but not eliminate it,
            //probably because not quite enough normalforce is applied in the y-direction without it,
            //causing us to apply 99-something % of normalforce one frame, and intersecting the collider in the next (frame).
            else
            {
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal) 
                                       * (1 - smoothingCastHitInfo.distance / smoothingMaxDistance)
                                       * glideNormalForceMargin
                                       + glideHeight * Vector3.up;
                                       /* (Mathf.Pow(((1 - smoothingCastHitInfo.distance / smoothingMaxDistance)), powerOf) */
            }

            ApplyFriction(smoothingNormalForce);
            velocity += new Vector3(0, smoothingNormalForce.y, 0);
        }

        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {
            float distanceToColliderNeg = skinWidth / Vector3.Dot(velocity.normalized, hitInfo.normal);
            float allowedMovementDistance = hitInfo.distance + distanceToColliderNeg;
                      

            if (allowedMovementDistance > velocity.magnitude * Time.deltaTime)
            {
                MoveOutOfGeometry(velocity * Time.deltaTime);
                return;
            }
            if(allowedMovementDistance > 0)
            {
                MoveOutOfGeometry(allowedMovementDistance * velocity.normalized);
            }

            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal);
            ApplyFriction(normalForce);

            velocity += new Vector3(normalForce.x, 0, normalForce.z);

            if (i < MAX_ITER)
                SmoothingCollisionCheck(i + 1);
        }
        else 
            MoveOutOfGeometry(velocity * Time.deltaTime);

        ApplyAirResistance();
    }
    private void CheckForCollisions(int i)
    {
        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {
            // Calculate the allowed distance to the collision point
            float distanceToColliderNeg = skinWidth / Vector3.Dot(velocity.normalized, hitInfo.normal);
            float allowedMovementDistance = hitInfo.distance + distanceToColliderNeg;

            // Are we allowed to move further than we are able to this frame? 
            if (allowedMovementDistance > velocity.magnitude * Time.deltaTime)
            {
                MoveOutOfGeometry(velocity * Time.deltaTime);
                return;
            }
            if (allowedMovementDistance > 0)
            {
                MoveOutOfGeometry(allowedMovementDistance* velocity.normalized);
            }

            //RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal);

            //velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);
            velocity += normalForce;
            ApplyFriction(normalForce);

            if (i < MAX_ITER)
                CheckForCollisions(i + 1);
        }
        else 
            MoveOutOfGeometry(velocity * Time.deltaTime);

        ApplyAirResistance();
    }
    private void MoveOutOfGeometry(Vector3 movement)
    {
        transform.position += movement;    
       
        for (int i = 0; i < MOVE_OUT_ITERATIONS && velocity.magnitude > 0.001f; i++) 
        {
            Collider[] colliders = OverlapCast(transform.position);

            if (colliders.Length < 1)
                return;
           
            Vector3? separation = null;
            float maxDistance = 0f;
            foreach (Collider currentCollider in colliders)
            {
                if (currentCollider == attachedCollider ||currentCollider.isTrigger)
                    continue;

                bool overlap = Physics.ComputePenetration(
                                            attachedCollider,
                                            transform.position,
                                            transform.rotation,
                                            currentCollider,
                                            currentCollider.transform.position,
                                            currentCollider.transform.rotation,
                                            out Vector3 direction,
                                            out float distance);
                
                //Compute penetration does not always return true (?), and if it doesnt, we can skip this loop iteration.
                if (distance < minimumPenetrationForPenalty)
                    continue;

                if (distance < (separation?.magnitude ?? float.MaxValue))
                {                   
                    separation = direction * distance;
                }
                else if (distance > separation?.magnitude)
                    maxDistance = distance;
            }
            
            //Move out of geometry and apply normalforce since this collision was missed by collisioncheck
            if (separation.HasValue)
            {
                transform.position += separation.Value + separation.Value.normalized * skinWidth;
                Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, separation.Value.normalized);
                velocity += normalForce;
            }
            else
            {              
                Debug.Log("Separation has no value!, maxDistance x100 is  " + maxDistance);
                return;
            }
        }
        //The move fails, or the character has no velocity
        if(velocity.magnitude > 0.001f)
            Debug.Log("Didnt trigger exit condition");
        //transform.position = cachedPosition;
    }

    #region Friction, Resistance and Gravity
    private void AddGravity()
    {
        Vector3 gravityMovement = currentGravity * Vector3.down * Time.deltaTime;
        velocity += gravityMovement;
    }
    private void ApplyFriction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
            velocity = Vector3.zero;
        else
        {
            velocity -= velocity.normalized * normalForce.magnitude * (kineticFrictionCoefficient);            
        }
    }
    public void SetFallingGravity()
    {
        currentGravity = gravityWhenFalling;
    }
    public void SetNormalGravity()
    {
        currentGravity = gravity;
    }

    private void ApplyAirResistance() { velocity *= Mathf.Pow(airResistance, Time.deltaTime); }
    #endregion
    #region Force and Speed
    private void ClampSpeed()
    {
        float temp = velocity.y;
        velocity = maxSpeed != 0 ? Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), maxSpeed) : velocity;
        velocity.y = temp;
    }
    public void AddForce(Vector3 input)
    {
        velocity += input.magnitude < inputThreshold ? Vector3.zero : input * Time.fixedDeltaTime;
    }
    public Vector3 GetXZMovement()
    {
        return new Vector3(velocity.x, 0, velocity.z);
    }
    #endregion
    public void SetGlide(bool gliding)
    {
        isGliding = gliding;
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


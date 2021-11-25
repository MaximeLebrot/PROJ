using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPhysicsSplit : MonoBehaviour
{
    const int MAX_ITER = 10;
    const int MOVE_OUT_ITERATIONS = 5;

    public Vector3 velocity;
    public RaycastHit groundHitInfo { get; private set; }

    [Header("Values")]

    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    [SerializeField] private float currentGravity;
    [SerializeField] private float minimumPenetrationForPenalty = 0.01f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float gravityWhenFalling = 10f;
    [SerializeField] private float glideHeight;
    [SerializeField] private float moveThreshold = 0.05f;
    /*[SerializeField]*/ private float stepHeight = 0.2f;
   

    //Properties
    public float GlideHeight { get; private set; }
    public float SurfThreshold { get => surfThreshold; }



    [Header("Values set by States")]
    private float maxSpeed = 12f;
    [SerializeField]private float defaultGravity = 9.81f;

    private float smoothingMaxDistance = 3f;
    private int powerOf = 2;
    public float surfThreshold = 8;

    private float staticFrictionCoefficient = 0.5f;
    private float kineticFrictionCoefficient = 0.35f;
    private float airResistance = 0.35f;

    //Collision
    private CapsuleCollider attachedCollider;
    private Vector3 colliderTopHalf, colliderBottomHalf;
    private Vector3 stepHeightDisplacement;

    private bool isGliding;
    private float glideNormalForceMargin = 1.1f;
    private float setValuesLerpSpeed = 2f;

    //Input Debug/Fix/Fuckery
    private Vector3 forceInput;
    private PlayerController pc;

    private delegate void CollisionDelegate(int i);
    private CollisionDelegate collisionMethod; 


    private void OnEnable()
    {
        pc = GetComponent<PlayerController>();
        attachedCollider = GetComponent<CapsuleCollider>();
        stepHeightDisplacement = new Vector3(0, stepHeight, 0);
    }

    private void Update()
    {
        //Add velocity and reset force vector in playercontroller       
        velocity += forceInput * Time.deltaTime;
        pc.ResetForceVector();

        AddGravity();
        CollisionCheck();
        ClampSpeed();
        
        //Debug
        Debug.DrawLine(transform.position, transform.position + velocity, Color.red);
        glideHeight = GlideHeight;
        
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
        StopCoroutine("LerpValues");
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

        while (time <= setValuesLerpSpeed)
        {
            //Friction and air resistance
            staticFrictionCoefficient = Mathf.Lerp(staticFrictionCoefficient, values.staticFriction, time * (1 / setValuesLerpSpeed));
            kineticFrictionCoefficient = Mathf.Lerp(kineticFrictionCoefficient, values.kineticFriction, time * (1 / setValuesLerpSpeed));
            airResistance = Mathf.Lerp(airResistance, values.airResistance, time * (1 / setValuesLerpSpeed));

            GlideHeight = Mathf.Lerp(GlideHeight, values.glideHeight, time * (1 / setValuesLerpSpeed));
            maxSpeed = Mathf.Lerp(maxSpeed, values.maxSpeed, time * (1 / setValuesLerpSpeed));
            currentGravity = Mathf.Lerp(defaultGravity, values.gravity, time * (1 / setValuesLerpSpeed));          
            
            time += Time.deltaTime;
            yield return null;
        }
       
    }

    private void SmoothingCollisionCheck(int i)
    {
        collisionMethod = SmoothingCollisionCheck;
        YCollisionSmoothing();
        XZCollision(i);
    }

    private void CheckForCollisions(int i)
    {
        StepHeightCollision(i);
        //WalkCollision(i);
        /*collisionMethod = CheckForCollisions;
        YCollision();
        XZCollision(i);  */    
    }
     private void StepHeightCollision(int i)
    {
        StepHeightYCollision();
        StepHeightXZCollision(0);
    }
    private void StepHeightYCollision()
    {
        //Y-axis normalforce
        //Could use sphere coll here instead of bottomhalf etc
        Vector3 castOrigin = transform.position + attachedCollider.center + (attachedCollider.height * 0.5f) * Vector3.down + stepHeight * Vector3.up;
        Vector3 yVelocity = new Vector3(0, velocity.y, 0);
        Vector3 normalForce = Vector3.zero;
        float castLength = yVelocity.magnitude * Time.deltaTime + skinWidth;
        Physics.CapsuleCast(castOrigin, castOrigin, stepHeight, Vector3.down, out RaycastHit yHitInfo, castLength, collisionMask);
        if (yHitInfo.collider && yHitInfo.collider.isTrigger == false)
        {
            float distanceToColliderNeg = skinWidth / Vector3.Dot(yVelocity.normalized, yHitInfo.normal);
            float allowedMovementDistance = yHitInfo.distance + distanceToColliderNeg;

            if (allowedMovementDistance > yVelocity.magnitude * Time.deltaTime)
            {
                MoveOutOfGeometry(yVelocity * Time.deltaTime);
                return;
            }
            if (allowedMovementDistance > 0)
            {
                MoveOutOfGeometry(allowedMovementDistance * yVelocity.normalized);
            }
            normalForce = PhysicsFunctions.NormalForce3D(yVelocity, yHitInfo.normal)
                                        + GlideHeight * Vector3.up;
            //}

            velocity += new Vector3(0, normalForce.y, 0);
        }
        else
            MoveOutOfGeometry(velocity * Time.deltaTime);
    }

    private void StepHeightXZCollision(int i)
    {
        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf + stepHeightDisplacement, attachedCollider.radius, GetXZMovement().normalized, out var hitInfo, GetXZMovement().magnitude * Time.deltaTime + skinWidth, collisionMask);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {
            // Calculate the allowed distance to the collision point
            float distanceToColliderNeg = skinWidth / Vector3.Dot(velocity.normalized, hitInfo.normal);
            float allowedMovementDistance = hitInfo.distance + distanceToColliderNeg;

            // Are we allowed to move further than we are able to this frame? 
            if (allowedMovementDistance > GetXZMovement().magnitude * Time.deltaTime)
            {
                MoveOutOfGeometry(GetXZMovement() * Time.deltaTime);
                return;
            }
            if (allowedMovementDistance > 0)
            {
                MoveOutOfGeometry(allowedMovementDistance * GetXZMovement().normalized);
            }

            //GlideHeight should be zero (or close to it) when walking, but needs to be added here to get a smooth transition along with the lerp in SetValues
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(GetXZMovement(), hitInfo.normal);
            ApplyFriction(normalForce);
            velocity += new Vector3(normalForce.x, 0, normalForce.z);


            if (i < MAX_ITER)
                CheckForCollisions(i + 1);
        }
        else
            MoveOutOfGeometry(GetXZMovement() * Time.deltaTime);

        ApplyAirResistance();
    }
    private void YCollisionSmoothing()
    {
        //Y-axis normalforce
        float castLength = velocity.magnitude * Time.deltaTime + skinWidth;
        Physics.SphereCast(colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit smoothingCastHitInfo, castLength + smoothingMaxDistance, collisionMask);
        if (smoothingCastHitInfo.collider && smoothingCastHitInfo.collider.isTrigger == false)
        {
            Vector3 smoothingNormalForce;
            if (smoothingCastHitInfo.distance <= castLength)
            {
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal);
            }
            //glideNormalForceMargin seems to alleviate the problem but not eliminate it,
            //probably because not quite enough normalforce is applied in the y-direction without it,
            //causing us to apply 99-something % of normalforce one frame, and intersecting the collider in the next (frame).
            else
            {
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, smoothingCastHitInfo.normal)
                                        * (Mathf.Pow(((1 - smoothingCastHitInfo.distance / smoothingMaxDistance)), powerOf)
                                        * glideNormalForceMargin)
                                        + GlideHeight * Vector3.up;
                //*((1 - smoothingCastHitInfo.distance / smoothingMaxDistance)
            }

            ApplyFriction(smoothingNormalForce);
            velocity += new Vector3(0, smoothingNormalForce.y, 0);
        }
    }
    private void YCollision()
    {
        //Y-axis normalforce
        //Could use sphere coll here instead of bottomhalf etc
        Vector3 castOrigin = transform.position + (attachedCollider.center + attachedCollider.height * 0.5f * Vector3.down) + stepHeight * 0.5f * Vector3.up;
        float castLength = velocity.magnitude * Time.deltaTime + skinWidth;
        Physics.SphereCast(colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit yHitInfo, castLength, collisionMask);
        if (yHitInfo.collider && yHitInfo.collider.isTrigger == false)
        {
            Vector3 smoothingNormalForce;
            /*
            if (yHitInfo.distance < castLength)
            {
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, yHitInfo.normal);
            }
            else
            {*/
                smoothingNormalForce = PhysicsFunctions.NormalForce3D(velocity, yHitInfo.normal)
                                        + GlideHeight * Vector3.up;
            //}

            //ApplyFriction(smoothingNormalForce);
            velocity += new Vector3(0, smoothingNormalForce.y, 0);
        }
    }
    private void XZCollision(int i  )
    {
        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf + stepHeightDisplacement, attachedCollider.radius, velocity.normalized, out var hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask);
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
                MoveOutOfGeometry(allowedMovementDistance * velocity.normalized);
            }

            //GlideHeight should be zero when walking, but needs to be added here to get a smooth transition along with the lerp in SetValues
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal);
            velocity += new Vector3(normalForce.x, 0, normalForce.z);
            ApplyFriction(normalForce);


            if (i < MAX_ITER)
                collisionMethod(i + 1);
        }
        else
            MoveOutOfGeometry(velocity * Time.deltaTime);

        ApplyAirResistance();
    }
    private void WalkCollision(int i)
    {
        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf, attachedCollider.radius, velocity.normalized, out RaycastHit hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask);
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
                MoveOutOfGeometry(allowedMovementDistance * velocity.normalized);
            }

            //GlideHeight should be zero when walking, but needs to be added here to get a smooth transition along with the lerp in SetValues
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal)
                                  + GlideHeight * Vector3.up;
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
        //Debug.Log("movement magnitude is: " + movement.magnitude);
        
        //Do not move at all if the distance is tiny.
        if (movement.magnitude < moveThreshold)
            return;

        transform.position += movement;    
       
        for (int i = 0; i < MOVE_OUT_ITERATIONS && velocity.magnitude > 0.001f; i++) 
        {
            Collider[] colliders = OverlapCast(transform.position);

            if (colliders.Length < 1)
                return;
           
            Vector3? separation = null;
            float maxDistance = 0f;
            Vector3 direction = Vector3.zero;
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
                                            out direction,
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
                Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity * minimumPenetrationForPenalty, direction.normalized);
                velocity += normalForce;
                //Debug.Log("Separation has no value!, maxDistance x100 is  " + maxDistance);
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
        currentGravity = defaultGravity;
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
    /*  When FPS drops too low, the multiplication from AddForce makes you unable to move - it multiplies by a delta time of 0.02 (50FPS)
     *  but if your FPS is 10, the adding of force to velocity apparently becomes very small. Time.deltaTime does not return actual deltaTime inside this method, because it is called 
     *  from a FixedUpdate, hence fixedDeltaTime and deltaTime here are the same. The deltaTime multiplication must happen outside this method to use actual deltaTime. Garbage. 
     *  velocity += forceInput * Time.deltaTime;
        forceInput = Vector3.zero;
     */
    public void AddForce(Vector3 input)
    {
        forceInput = Vector3.zero;
        forceInput = input.magnitude < inputThreshold ? Vector3.zero : input;
        //Obsolete
        //velocity += input.magnitude < inputThreshold ? Vector3.zero : input * Time.fixedDeltaTime;
    }
    public Vector3 GetXZMovement()
    {
        return new Vector3(velocity.x, 0, velocity.z);
    }
    #endregion
    #region CollisionCast
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
    public void SetGlide(bool gliding){ isGliding = gliding; }
    //Walk collision
    /*private void CheckForCollisions(int i)
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

            //GlideHeight should be zero when walking, but needs to be added here to get a smooth transition along with the lerp in SetValues
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal)
                                  + GlideHeight * Vector3.up;
            velocity += normalForce;
            ApplyFriction(normalForce);

            if (i < MAX_ITER)
                CheckForCollisions(i + 1);
        }
        else 
            MoveOutOfGeometry(velocity * Time.deltaTime);
        
     ApplyAirResistance();
    }*/
}


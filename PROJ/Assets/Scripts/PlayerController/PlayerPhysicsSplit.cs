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

    [Header("Collision")]
    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float minimumPenetrationForPenalty = 0.01f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float stepHeight = 0.2f;

    [Header("Movement Restraints")]
    [SerializeField] private float moveThreshold = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    [SerializeField] private float gravityWhenFalling = 10f;
    [SerializeField] private float currentGravity;
    private float defaultGravity = 9.81f;

    #region Values from States
    //Values set from States
    private float maxSpeed = 12f;
    private float staticFrictionCoefficient = 0.5f;
    private float kineticFrictionCoefficient = 0.35f;
    private float airResistance = 0.35f;
    private float setValuesLerpSpeed = 2f;
    #endregion
    //Collision
    private CapsuleCollider attachedCollider;
    private Vector3 colliderTopHalf, colliderBottomHalf;
    private Vector3 stepHeightDisplacement;

    //Input Debug/Fix/Fuckery
    private Vector3 forceInput;
    private PlayerController pc;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        attachedCollider = GetComponent<CapsuleCollider>();
        stepHeightDisplacement = new Vector3(0, stepHeight, 0);
    }
    private void OnEnable()
    {
        velocity = Vector3.zero;
    }
    private void Update()
    {
        //Add velocity and reset force vector in playercontroller
        velocity += forceInput * Time.deltaTime;
        pc.ResetForceVector();

        AddGravity();
        CheckForCollisions(0);
        ClampSpeed();
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
        }

        while (time <= setValuesLerpSpeed)
        {
            //Friction and air resistance
            staticFrictionCoefficient = Mathf.Lerp(staticFrictionCoefficient, values.staticFriction, time * (1 / setValuesLerpSpeed));
            kineticFrictionCoefficient = Mathf.Lerp(kineticFrictionCoefficient, values.kineticFriction, time * (1 / setValuesLerpSpeed));
            airResistance = Mathf.Lerp(airResistance, values.airResistance, time * (1 / setValuesLerpSpeed));

            maxSpeed = Mathf.Lerp(maxSpeed, values.maxSpeed, time * (1 / setValuesLerpSpeed));
            currentGravity = Mathf.Lerp(currentGravity, values.gravity, time * (1 / setValuesLerpSpeed));          
            
            time += Time.deltaTime;
            yield return null;
        }
       
    }

    private void CheckForCollisions(int i)
    {

        YCollisionRayCast();
        XZCollision(i);
    }
    private void YCollisionRayCast()
    {
        float castLength = attachedCollider.radius + stepHeight + skinWidth;
        Debug.DrawLine(colliderBottomHalf, colliderBottomHalf + (Vector3.down * castLength), Color.green);
        Physics.Raycast(colliderBottomHalf, Vector3.down, out RaycastHit hit, castLength, collisionMask);

        //some sort of force inverse to the distance and which the raycast hits the ground
        //Do we actually want to apply more normalforce if the character is intersecting another collider? Maybe not... in that case, this code is basically fine, in principle
        Vector3 yNormalForce;
        if (hit.collider && hit.collider.isTrigger == false)
        {
            //Debug.Log("Raycast hit at height: " + hit.point.y);
            float partDistanceHit = hit.distance / castLength; // => 0.75 / 1 = 0.75 1 + (1 - hit.distance / castLength)
            yNormalForce = PhysicsFunctions.NormalForce3D(velocity /** (1 + (1 - partDistanceHit))*/, hit.normal)
                                        + (1 - partDistanceHit) * Vector3.up;            
        }
        else
        {
            Vector3 yVelocity = new Vector3(0, velocity.y, 0);
            yNormalForce = PhysicsFunctions.NormalForce(yVelocity, Vector3.down);
        }
        velocity += yNormalForce;
        ApplyFriction(yNormalForce);
    }
    private void XZCollision(int i)
    {
        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf + stepHeightDisplacement, attachedCollider.radius, new Vector3(velocity.x, 0 , velocity.z), out var hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask);
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

            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, hitInfo.normal);
            velocity += new Vector3(normalForce.x, 0, normalForce.z);
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
        //Debug.Log("movement magnitude is :" + movement.magnitude);
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

            Vector3 direction = Vector3.zero;
            foreach (Collider currentCollider in colliders)
            {
                if (currentCollider == attachedCollider || currentCollider.isTrigger)
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
            }
            
            //Move out of geometry and apply normalforce since this collision was missed by collisioncheck
            //Cannot apply the normalforce created in these if-else statements, when using stepheight - it stops the player from crossing over barriers
            //because it thinks a collision was simply missed, instead of intentionally ignored.
            if (separation.HasValue)
            {
                transform.position += separation.Value + separation.Value.normalized * skinWidth;
                //Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, separation.Value.normalized);
                //velocity += normalForce;
            }
            else
            {
                //Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity * minimumPenetrationForPenalty, direction.normalized);
                //velocity += normalForce;
                //This can also happen if the player intersects only colliders that are not valid, such as attachedCollider or if the collider is a trigger
                Debug.Log("Separation has no value!");
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
        defaultGravity = currentGravity;
        currentGravity = gravityWhenFalling;

    }
    public void RestoreGravity()
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
    
    
}


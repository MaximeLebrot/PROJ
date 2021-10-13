using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PhysicsComponent : MonoBehaviour
{
    //modifierar .y-värdet på velocity, kan alltså inte vara en property
    public Vector3 velocity;
    //går denna att göra private istället? 
    [SerializeField] private LayerMask collisionMask;
    private Vector3 colliderTopHalf, colliderBottomHalf;

    public RaycastHit groundHitInfo { get; private set; }

    [Header("Values")]
    [SerializeField] public float maxSpeed;
    [SerializeField] public float gravity = 10f;
    [SerializeField] protected float skinWidth = 0.05f;
    [SerializeField] private float inputThreshold = 0.1f;
    //[SerializeField] private float gravityModifier = 1f;

    [Header("Friktion")]
    [Range(0f, 1f)] [SerializeField] private float staticFrictionCoefficient = 0.5f;
    [Range(0f, 1f)] [SerializeField] private float kineticFrictionCoefficient = 0.35f;
    [Range(0f, 1f)] [SerializeField] private float airResistance = 0.35f;

    private CapsuleCollider attachedCollider;

    private void OnEnable()
    {
        attachedCollider = GetComponent<CapsuleCollider>();

        if (GetComponent<PlayerController>())
            maxSpeed = GetComponent<PlayerController>().GetMaxSpeed();

      /*  if (attachedCollider is BoxCollider)
            collisionCaster = new BoxCaster(attachedCollider, collisionMask);

        if (attachedCollider is SphereCollider)
            collisionCaster = new SphereCaster(attachedCollider, collisionMask);

        if (attachedCollider is CapsuleCollider)
            collisionCaster = new CapsuleCaster(attachedCollider, collisionMask);

        if (attachedCollider is MeshCollider)
            collisionCaster = new MeshCaster(attachedCollider, collisionMask);
      */
    }

    public void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + velocity);
        AddGravity();
        CheckForCollisions(0);
        ClampSpeed();
        
        //AddSmoothing(0);
        //Silvertejpslösning för att inte få -Infinity eller NaN
        if (float.IsNaN(velocity.x) == false && float.IsNegativeInfinity(velocity.x) == false && float.IsPositiveInfinity(velocity.x) == false)
            transform.position += velocity * Time.deltaTime;

        MoveOutOfGeometry();
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
    #region capsuleCasts
    public  RaycastHit CastCollision(Vector3 origin, Vector3 direction, float distance)
    {
        UpdateColliderPosition(origin);

        Physics.CapsuleCast(colliderTopHalf, colliderBottomHalf, attachedCollider.radius, direction.normalized, out var hitInfo, distance, collisionMask);

        return hitInfo;
    }

    public  Collider[] OverlapCast(Vector3 currentPosition)
    {
        UpdateColliderPosition(currentPosition);
        return Physics.OverlapCapsule(colliderTopHalf, colliderBottomHalf, attachedCollider.radius, collisionMask);
    }

    private void UpdateColliderPosition(Vector3 currentPosition)
    {
        colliderTopHalf = (currentPosition + attachedCollider.center) + Vector3.up * (attachedCollider.height / 2 - attachedCollider.radius);
        colliderBottomHalf = (currentPosition + attachedCollider.center) + Vector3.down * (attachedCollider.height / 2 - attachedCollider.radius);
    }
    #endregion


    private void RayCastMovement(int i)
    {
        //cast point 1 and point 2
        /* Calculate normalForce
         * velocity += normalForce
         * 
         */


    }



    //Could this method ONLY apply the smoothing, and not the normalforce itself (when in contact with surfaces?) We dont want to include collision against vertical surfaces here either, and also only do this when gliding
    //Maybe the vertical surfaces rule doesnt need to apply if we only use this method while gliding, which is what we should do anyway..? 

    //Apply mean velocity
    public Vector3 accruedVelocity;
    public float meanCount = 60;
    //Smooth at certain distance
    private void SmoothMovement(int i)
    {
        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth /*+ smoothingMaxDistance*/);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {

            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            accruedVelocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);
            //velocity += normalForce;

            accruedVelocity += normalForce;
            Vector3 meanVelocity = accruedVelocity / meanCount;
            velocity += meanVelocity;
            //Should first 60 or so iterations count up slowly
            accruedVelocity -= accruedVelocity / meanCount;

            ApplyFriction(normalForce);

            if (i < 10)
                SmoothMovement(i + 1);
        }
    }

    //Could this method ONLY apply the smoothing, and not the normalforce itself (when in contact with surfaces?) We dont want to include collision against vertical surfaces here either, and also only do this when gliding
    //Maybe the vertical surfaces rule doesnt need to apply if we only use this method while gliding, which is what we should do anyway..? 
    public float smoothingMaxDistance = 2f;
    public float powerOf = 2f;
    private void AddSmoothing(int i)
    {
        float margin = velocity.magnitude * Time.deltaTime + skinWidth;
        RaycastHit hitInfo = CastCollision(transform.position/* * (1 + margin)*/, velocity.normalized, margin + smoothingMaxDistance);
        RaycastHit hitInfoSmoothing = CastCollision(transform.position * (1 + margin), velocity.normalized, margin + smoothingMaxDistance);
        if (hitInfoSmoothing.collider && hitInfoSmoothing.collider.isTrigger == false)
        {
            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            //velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);

            // velocity += normalForce * (Mathf.Pow(smoothingMaxDistance / hitInfo.distance -1, distanceSmoother)); // Vilken faktor vill vi multiplicera normalkraften med? => avstånd till mark fast reversed | 
            //Application should be 0 when distance is 0 (or close to it), but also when distance == smoothingMaxDistance
            Vector3 smoothingVector = normalForce * Mathf.Pow(Mathf.Abs((hitInfo.distance / smoothingMaxDistance) - 1), powerOf);

            velocity += smoothingVector;

            /*if (i < 10)
                AddSmoothing(i + 1);*/
        }
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {

            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            //Move player to contact surface, within skinWidth distance
            velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);

            // velocity += normalForce * (Mathf.Pow(smoothingMaxDistance / hitInfo.distance -1, distanceSmoother)); // Vilken faktor vill vi multiplicera normalkraften med? => avstånd till mark fast reversed | 
            //Application should be 0 when distance is 0 (or close to it), but also when distance == smoothingMaxDistance
            Vector3 smoothingVector = normalForce * Mathf.Abs((hitInfo.distance / smoothingMaxDistance) - 1);
            Vector3 normalForceApplication =  normalForce + smoothingVector;

            velocity += normalForceApplication; 
            ApplyAirResistance();

           if (i < 10)
                AddSmoothing(i + 1);
        }


    }
    private float accruedY;
    private void SmoothMovement3(int i)
    {
        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {

            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);
            
            Vector3 normalForceNoY = new Vector3(normalForce.x, 0, normalForce.z);
            accruedY += normalForce.y;
            velocity.y += accruedY / meanCount;
            accruedY -= accruedY / meanCount;
            velocity += normalForceNoY;


            ApplyFriction(normalForce);

            if (i < 10)
                SmoothMovement3(i + 1);
        }
    }
    /// <summary>
    /// Boxcast cast to see if this object would hit anything next frame : direction = velocity.normalized, distance = velocity.magnitude * deltaTime
    /// </summary>
    /// <param name="i"></param>
    private void CheckForCollisions(int i)
    {

        RaycastHit hitInfo = CastCollision(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime + skinWidth);
        if (hitInfo.collider && hitInfo.collider.isTrigger == false)
        {

            RaycastHit normalHitInfo = CastCollision(transform.position, -hitInfo.normal, hitInfo.distance);
            Vector3 normalForce = PhysicsFunctions.NormalForce3D(velocity, normalHitInfo.normal);

            velocity += -normalHitInfo.normal * (normalHitInfo.distance - skinWidth);
            velocity += normalForce;


            ApplyFriction(normalForce);

            if (i < 10)
                CheckForCollisions(i + 1);
        }
    }

    private void MoveOutOfGeometry()
    {
        Collider[] colliders = OverlapCast(transform.position);

        if (colliders.Length < 1) return;

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


    protected void AddGravity()
    {
        Vector3 gravityMovement = gravity * Vector3.down * Time.deltaTime;
        velocity += gravityMovement;
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
    public void AddForce(Vector3 input)
    {
        velocity += input.magnitude < inputThreshold ? Vector3.zero : input * Time.deltaTime;

    }


}

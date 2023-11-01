using DOTS;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 100f;
    [SerializeField] private float maxTravelDistance = 300f;
    [SerializeField] private float damage = 1;

    public UnityEvent<Projectile> OnReturnToPoolEvent;

    private Vector3 spawnPosition;
    private CapsuleCollider capsuleCollider;

    void Awake()
    {
        spawnPosition = transform.position;
        capsuleCollider = GetComponent<CapsuleCollider>();
        
    }

    void OnEnable()
    {
        spawnPosition = transform.position;
    }

    void OnDisable()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * travelSpeed;


        if (Vector3.Distance(spawnPosition, transform.position) >= maxTravelDistance)
        {
            OnReturnToPoolEvent.Invoke(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter");
        // Check if the collision involves an object with the IHealth interface
        IHealth healthInterface = collision.gameObject.GetComponent<IHealth>();

        if (healthInterface != null)
        {
            healthInterface.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger enter");
        // Check if the collision involves an object with the IHealth interface
        IHealth healthInterface = collider.gameObject.GetComponent<IHealth>();

        if (healthInterface != null)
        {
            healthInterface.TakeDamage(damage);
        }
    }

    public void SpawnFromPool(Vector3 SpawnPos, Quaternion SpawnDirection)
    {
        transform.position = SpawnPos;
        transform.rotation = SpawnDirection;
        spawnPosition = transform.position;

        enabled = true;
    }

    //public class ProjectileBaker : Baker<Projectile>
    //{
    //    public override void Bake(Projectile authoring)
    //    {
    //        var unitEntity = GetEntity(TransformUsageFlags.Dynamic);

    //        AddComponent<NewUnitTag>(unitEntity);
    //    }
    //}
}

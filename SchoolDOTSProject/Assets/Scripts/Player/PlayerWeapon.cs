using System.Collections.Generic;
using System.Diagnostics;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;
using Debug = UnityEngine.Debug;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private GameObject projectileSpawnPos_Right;
    [SerializeField] private GameObject projectileSpawnPos_Left;
    private List<Projectile> projectilePool = new();
    private bool shootRight;

    void Awake()
    {

    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
    }

    void Update()
    {

    }

    public void FireWeapon()
    {

        Transform SpawnPos = SwitchAndGetGunPos();

        if (projectilePool == null || projectilePool.Count <= 0)
        {
            // Spawn new projectile

            Projectile spawnedProjectile =
                Instantiate(projectile, SpawnPos.position, SpawnPos.transform.rotation);
            spawnedProjectile.OnReturnToPoolEvent.AddListener(ReturnToPool);
        }
        else
        {
            // Get projectile from pool
            ;
            projectilePool[0].SpawnFromPool(SpawnPos.position, SpawnPos.rotation);
            projectilePool.RemoveAt(0);
        }
    }

    private Transform SwitchAndGetGunPos()
    {
        if (shootRight)
        {
            shootRight = !shootRight;
            return projectileSpawnPos_Right.transform;
        }

        shootRight = !shootRight;
        return projectileSpawnPos_Left.transform;
    }

    private void ReturnToPool(Projectile returnedProjectile)
    {
        returnedProjectile.enabled = false;
        projectilePool.Add(returnedProjectile);

    }
}

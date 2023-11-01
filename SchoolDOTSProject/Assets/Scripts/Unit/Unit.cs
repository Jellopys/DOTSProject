using DOTS;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Unit : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float walkSpeed;

    void Update()
    {
        MoveTowardTarget();
        RotateTowardTarget();
    }

    private void MoveTowardTarget()
    {
        transform.position = transform.forward * walkSpeed * Time.deltaTime;
    }

    private void RotateTowardTarget()
    {
        transform.rotation = quaternion.RotateY(MathHelpers.GetHeading(transform.position, target.transform.position));
    }
}

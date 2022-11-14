using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("EnemyData")]
    public int currentLife;
    public int maxLife;
    public int enemyScorePoints;

    [Header("EnemyMovement")]
    public float speed;
    public float attackRange;
    public float yPathOffSet;
    public float followRange;
    public bool alwaysFollow;

    private List<Vector3> listPath;
    private WeaponController weaponController;
    private PlayerController target;// player

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        target = FindObjectOfType<PlayerController>();

        InvokeRepeating(nameof(UpdatePaths), 0.0f, 0.5f);
    }

    /// <summary>
    /// Update every 0.5 sec the path points to the target
    /// </summary>
    private void UpdatePaths()
    {
        // Create a MeshPath object
        NavMeshPath navMeshPath = new NavMeshPath();
        // Calculate all the points in the path to reach the target
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);
        // Convert all points to the List
        listPath = navMeshPath.corners.ToList();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance >= attackRange)
            ReachTarget();
        else
            if (weaponController.CanShoot())
                weaponController.Shoot();

    }

    /// <summary>
    /// Move the enemy to reach the target 
    /// following the path calculated
    /// </summary>
    private void ReachTarget()
    {
        if (listPath.Count == 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, listPath[0] + new Vector3(0, yPathOffSet, 0), speed * Time.deltaTime);

        if (transform.position == listPath[0] + new Vector3(0, yPathOffSet, 0))
        {
            listPath.RemoveAt(0);
        }
    }
}

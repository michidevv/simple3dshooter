using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private NavMeshAgent navMeshAgent;

    public Transform Target { get; set; }

    [SerializeField]
    private float shootDelay = 0.5f;

    [SerializeField]
    private int scorePoint = 25;

    [SerializeField]
    private GameObject explosionPrefab = null;

    private bool seeTarget;

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, shootDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            navMeshAgent.SetDestination(Target.position);
            CheckTargetVisibility();
        }
    }

    private void Shoot()
    {
        if (seeTarget)
        {
            // TODO: For now, refactor.
            float value = Random.Range(0f, 20f);
            if (value > 15)
            {
                ShootRocket();
            }
            else
            {
                var direction = GetTargetDirection();
                ShootBullet(direction);
            }
        }
    }

    Vector3 GetTargetDirection()
    {

        return Target.position - gun.transform.position;
    }

    void CheckTargetVisibility()
    {
        Vector3 targetDirection = GetTargetDirection();

        Ray ray = new Ray(gun.transform.position, targetDirection);

        RaycastHit hit;

        seeTarget = Physics.Raycast(ray, out hit) && hit.transform == Target;
    }

    private void Destroyed()
    {
        if (explosionPrefab != null)
        {
            Explosion.Create(transform.position, explosionPrefab);
        }

        if (Random.Range(0, 100) < 50)
        {
            HealthBonus.Create(transform.position);
        }

        ScoreLabel.Score += scorePoint;
    }
}

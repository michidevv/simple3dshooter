using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private float damage = 0f;

    [SerializeField]
    private GameObject owner = null;

    [SerializeField]
    private float radius = 0f;

    [SerializeField]
    private GameObject explosionPrefab = null;

    public float Damage { get => damage; set => damage = value; }

    public GameObject Owner { get => owner; set => owner = value; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CauseExplosionDamage()
    {
        Collider[] explosionVictims = 
            Physics.OverlapSphere(transform.position, radius);
        foreach (var v in explosionVictims)
        {
            Vector3 vectorToVictim = v.transform.position - transform.position;
            float decay = 1 - (vectorToVictim.magnitude - radius);
            Destructable vDestructable = v.gameObject.GetComponent<Destructable>();
            if (vDestructable != null)
            {
                vDestructable.Hit(damage * decay);
            }
            Rigidbody vRigidbody = v.gameObject.GetComponent<Rigidbody>();
            if (vRigidbody != null)
            {
                // TODO: Move 1000 as a [SerializeField].
                vRigidbody.AddForce(vectorToVictim.normalized * decay * 1000);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Equals(collision.gameObject, owner))
        {
            return;
        }

        if (radius > 0f)
        {
            CauseExplosionDamage();
        }
        else
        {
            Destructable target = collision.gameObject.GetComponent<Destructable>();
            if (target != null)
            {
                target.Hit(damage);
            }
        }



        if (explosionPrefab != null)
        {
            Explosion.Create(transform.position, explosionPrefab);
        }

        ParticleSystem trail = 
            gameObject.GetComponentInChildren<ParticleSystem>();
        if (trail != null)
        {
            Destroy(trail.gameObject, trail.startLifetime);
            trail.Stop();
            trail.transform.SetParent(null);
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private float hitPoints = 100f;

    public float HitPoints { get => hitPoints; set => hitPoints = value; }
    public float HitPointsCurrent { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        HitPointsCurrent = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(float damage)
    {
        HitPointsCurrent -= damage;

        if (HitPointsCurrent <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        BroadcastMessage("Destroyed");
    }
}

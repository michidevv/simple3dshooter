using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private GameObject rocketPrefab = null;

    [SerializeField]
    private float bulletDamage = 5.0f;

    [SerializeField]
    private float rocketDamage = 20.0f;

    [SerializeField]
    protected Transform gun = null;

    [SerializeField]
    protected Transform rocketLauncher = null;

    [SerializeField]
    private float shootPower = 500f;

    [SerializeField]
    private float rocketDelay = 0f;

    private float rocketDelayCurrent = 0f;

    private void UpdateDamager(ref GameObject damagerObject)
    {
        var damager = damagerObject.GetComponent<Damager>();
        damager.Damage = bulletDamage;
        damager.Owner = gameObject;
    }

    protected void ShootBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position,
               gun.rotation);
        direction.Normalize();
        bullet.GetComponent<Rigidbody>().AddForce(direction * shootPower);
        UpdateDamager(ref bullet);

        Destroy(bullet, 5);
    }

    protected void ShootRocket()
    {
        if (rocketPrefab == null || rocketLauncher == null)
        {
            // Oops, no rocket related components.
            return;
        }

        if (rocketDelayCurrent > 0)
        {
            return;
        }

        rocketDelayCurrent = rocketDelay;
        GameObject rocket = Instantiate(rocketPrefab, rocketLauncher.position,
                rocketLauncher.rotation);
        UpdateDamager(ref rocket);

        Destroy(rocket, 5);
    }

    protected void UpdateTimer()
    {
        if (rocketDelayCurrent > 0)
        {
            rocketDelayCurrent -= Time.deltaTime;
        }
    }
}

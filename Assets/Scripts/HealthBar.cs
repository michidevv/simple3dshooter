using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    [SerializeField]
    private Destructable owner = null;

    [SerializeField]
    private bool rotateBar = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponent<Image>();

        rotateBar = gameObject.GetComponent<CharacterController>() == null;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0.0f, owner.HitPoints, 
            owner.HitPointsCurrent);

        if (rotateBar)
        {
            //transform.forward = Camera.main.transform.position - transform.position;
            transform.forward = Camera.main.transform.forward;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField]
    //private float moveSpeed;

    [SerializeField]
    private GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
            //transform.position = Vector3.Lerp(transform.position, 
                //target.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}

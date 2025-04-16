using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRot : MonoBehaviour
{
    public float targetRot;
    public Rigidbody2D rb2d;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, targetRot, force * Time.fixedDeltaTime));
    }
}

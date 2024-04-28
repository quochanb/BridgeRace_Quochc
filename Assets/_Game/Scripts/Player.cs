using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Joystick joystick;
    
    void Start()
    {
        OnInit();
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pos = Tf.position + joystick.direction * speed * Time.deltaTime;
            Tf.position = CheckGround(pos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}

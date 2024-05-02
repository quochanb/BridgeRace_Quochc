using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;

    void Start()
    {
        OnInit();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPos = Tf.position + Joystick.direction * speed * Time.deltaTime;
            if (true)
            {
                Tf.position = CheckGround(nextPos);
            }
            if(Joystick.direction != Vector3.zero)
            {
                Tf.rotation = Quaternion.LookRotation(Joystick.direction);
            }
            ChangeAnim(Constants.ANIM_RUN);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}

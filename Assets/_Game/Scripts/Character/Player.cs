using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPos = Tf.position + Joystick.direction * speed * Time.deltaTime;
            
            if (CanMove(nextPos) && Joystick.direction != Vector3.zero)
            {
                Tf.position = CheckGround(nextPos);
                Tf.rotation = Quaternion.LookRotation(Joystick.direction);
                ChangeAnim(Constants.ANIM_RUN);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
}

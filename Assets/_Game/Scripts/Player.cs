using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Joystick joystick;

    private bool isClimbing;

    private void Start()
    {

    }
    void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        RaycastHit hit;

        isClimbing = Physics.Raycast(transform.position, Vector3.down, out hit, 2f, stairLayer);

        if (!isClimbing)
        {
            rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
        }
        else
        {
            Vector3 nextPos = hit.point + Vector3.up * 0.5f;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            if (transform.forward.z > 0)
            {

            }
        }
    }
}

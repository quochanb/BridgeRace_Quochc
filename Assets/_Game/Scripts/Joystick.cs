using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Vector3 direction;
    public delegate void JoystickDirection(Vector3 dir);
    public JoystickDirection directionEvent;
    [SerializeField] private GameObject joystick;
    [SerializeField] private RectTransform bg, knob;
    [SerializeField] private float knobRange;
    private Vector3 startPos, currentPos;
    private Vector3 screen;
    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;
    }

    void OnEnable()
    {
        direction = Vector3.zero;
        directionEvent?.Invoke(Vector3.zero);
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = MousePosition;
            joystick.SetActive(true);
            bg.anchoredPosition = startPos;
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = MousePosition;
            //calculate position of knob
            knob.anchoredPosition = Vector3.ClampMagnitude((currentPos - startPos), knobRange) + startPos;

            Vector3 currentDir = (currentPos - startPos).normalized;
            currentDir.z = currentDir.y;
            currentDir.y = 0;
            direction = currentDir;
            directionEvent?.Invoke(currentDir);
        }
        if(Input.GetMouseButtonUp(0))
        {
            joystick.SetActive(false);
            direction = Vector3.zero;
            directionEvent?.Invoke(Vector3.zero);
        }
    }
}

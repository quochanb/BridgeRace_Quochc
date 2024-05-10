using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    public delegate void WinGameDelegate();
    public static WinGameDelegate winGameEvent;
    [SerializeField] private float speed = 5f;
    int coin = 0;

    private void Update()
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Constants.TAG_BRICK) && other.GetComponent<ColorObject>().ColorType == ColorType)
        {
            UIManager.Instance.GetUI<CanvasGamePlay>().UpdateScore(coin += 10);
        }
        if (other.CompareTag(Constants.TAG_FINISH))
        {
            winGameEvent?.Invoke();
            Tf.position = level.GetFinishPoint();
            ChangeAnim(Constants.ANIM_DANCE);
            UIManager.Instance.GetUI<CanvasVictory>().SetBestScore(coin);
            GameManager.Instance.ChangeGameState(GameState.Victory);
        }
    }
}

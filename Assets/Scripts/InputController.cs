using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 moveValue;


    public void Move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>() * Time.deltaTime * speed;
    }

    private void Update()
    {
        Debug.Log("TEST2" + moveValue);

        transform.Translate(moveValue);
    }


    //public void VibrateController(float duration, float min = 0f, float max = 0.5f)
    //{
    //    StartCoroutine(Vibrate(duration, min, max));

    //}

    //private IEnumerator Vibrate(float duration, float min = 0f, float max = 0.5f)
    //{
    //    gamepad.SetMotorSpeeds(0, 0.5f);
    //    gamepad.ResumeHaptics();
    //    yield return new WaitForSeconds(duration);
    //    gamepad.PauseHaptics();
    //}
}

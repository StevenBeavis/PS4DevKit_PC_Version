using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;

public class Aiming : MonoBehaviour
{
    public Vector2 sensitivity;
    private Vector2 rotation;

    public int playerID;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        PS4Input.PadResetOrientation(playerID);
    }

    void Update()
    {
        Vector4 v = PS4Input.PadGetLastOrientation(0);

        Vector2 velocity = GetInput(v) * sensitivity;

        rotation += velocity * Time.deltaTime;

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }

    Vector2 GetInput(Vector4 v)
    {
        Vector2 input = new Vector2(
            Mathf.Clamp(v.x * 10, -1, 1),
            Mathf.Clamp(v.x * 10, -1, 1));
        return input;
    }
}

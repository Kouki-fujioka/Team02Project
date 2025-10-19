using System.Collections;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;
    [SerializeField] private float LockTime;
    [SerializeField] private Transform batonPos;

    private float pitch;
    private float yaw;
    public bool input;

    void Start()
    {
        CameraLock();
    }

    void Update()
    {
        if (input)
        {
            float input_x = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float input_y = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            yaw += input_x;
            pitch -= input_y;
            pitch = Mathf.Clamp(pitch, -minAngle, maxAngle);
            pitch -= input_y;
            batonPos.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }

    IEnumerator CameraLock()
    {
        while (LockTime > 0)
        {
            LockTime -= Time.deltaTime;

            if (LockTime < 0)
            {
                input = true;
            }
        }

        yield return new WaitForEndOfFrame();
    }
}

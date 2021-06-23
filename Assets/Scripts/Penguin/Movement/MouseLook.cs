using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    [SerializeField]
    MovementSettings settings;
    [SerializeField]
    LayerMask hitMask;

    Vector3 target;
    void Start()
    {
        playerBody = (Transform)GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtMouse();

    }

    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, hitMask))
        {
            target = hit.point;
            target.y = 0;
        }

        Vector3 rayDir = target - Camera.main.transform.position;
        playerBody.LookAt(target);
    }

    //For controller
    private void RotateWithMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * settings.mouseSensitivity * Time.deltaTime;

        playerBody.rotation = Quaternion.Euler(Vector3.up * mouseX);
    }

}

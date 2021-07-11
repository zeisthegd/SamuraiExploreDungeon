using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool useStaticBillboard;
    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        BillBoard();
    }

    void BillBoard()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(mainCam.transform);
        }
        else
        {
            transform.rotation = mainCam.transform.rotation;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }
}

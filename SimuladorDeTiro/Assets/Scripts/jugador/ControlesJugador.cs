using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesJugador : MonoBehaviour
{
    [SerializeField] Transform camara;
    [SerializeField] float mousesensi = 0.5f;

    float verticalrotation;
   // public GameObject centerCam;


    void Start()
    {
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mousesensi);
        verticalrotation -= Input.GetAxisRaw("Mouse Y") * mousesensi;
        verticalrotation = Mathf.Clamp(verticalrotation, -90f, 90f);
        camara.localEulerAngles = new Vector3(verticalrotation, 0, 0);

    }
}

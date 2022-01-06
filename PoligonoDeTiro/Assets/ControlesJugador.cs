using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlesJugador : MonoBehaviour
{
    [SerializeField] Transform camara;
    [SerializeField] float mousesensi = 0.5f;
    
    float verticalrotation;
    public GameObject centerCam;


    public float horizontalSpeed = 1.0F;
    public float verticalSpeed = 1.0F;
    float speed = 1.0F;
    public float RotationSpeed = 1;
    public Transform tar;
    public float mover = 2f;
    public Vector2 turn;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
    }
    void Update()
    {
        /*transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mousesensi);
        verticalrotation -= Input.GetAxisRaw("Mouse Y") * mousesensi;
        verticalrotation = Mathf.Clamp(verticalrotation, -90f, 90f);
        camara.localEulerAngles = new Vector3(verticalrotation, 0, 0);*/

        puntero();

        /*float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0f);*/




       /* string msg = File.ReadAllText(@"C:/Users/User/Desktop/Simulador de Tiro/DetectorDeRostro/Datospuntero.json");
        

        if (!string.IsNullOrEmpty(msg))
        {
            string[] coordenadas = msg.Split(';');
            float x = float.Parse(coordenadas[1]);
            float y = float.Parse(coordenadas[0])/100;

            print(x);

            transform.Rotate(2.1F, y,0F);

        }*/

    }
    private void puntero()
    {

        string msg = File.ReadAllText(@"C:/Users/User/Desktop/opencv/puntero/DatosPuntero.json");
        print(msg);


        if (!File.Exists(msg))
        {

            string[] coordenadas = msg.Split(';');
            float x = float.Parse(coordenadas[0])/10;
            float y = float.Parse(coordenadas[1])/10;

            x += Input.GetAxis("Mouse X");
            y += Input.GetAxis("Mouse Y");
            transform.localRotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            //string msg = File.ReadAllText(@"C:/Users/User/Desktop/Simulador de Tiro/DetectorDeRostro/DatosSalida.json");
            File.Create(msg).Dispose();
        }


        
    }
}

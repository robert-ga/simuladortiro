using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /*public GameObject blanco;
    //public GameObject puntero;
    private float distacia;
    private void Update()
    {
        //distacia = Vector3.Distance(blanco.transform.position, puntero.transform.position);
        print(distacia);
    }*/
    public void Hit()
    {
        transform.position = TargetBouns.Instance.GetRamdomPosition();
    }
}

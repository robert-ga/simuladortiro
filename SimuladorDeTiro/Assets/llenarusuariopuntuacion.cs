using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class llenarusuariopuntuacion : MonoBehaviour
{
    public GameObject nombres;
    public GameObject kills;

    public GameObject precision;
    public GameObject tiempo;
    public GameObject fecha;

    public void setdatospuntuacion(string nombres, int kills, string precision, string tiempo, string fecha)
    {
        this.nombres.GetComponent<Text>().text = nombres;
        this.kills.GetComponent<Text>().text = kills.ToString();

        this.precision.GetComponent<Text>().text = precision;
        this.tiempo.GetComponent<Text>().text = tiempo;
        this.fecha.GetComponent<Text>().text = fecha;
    }
}

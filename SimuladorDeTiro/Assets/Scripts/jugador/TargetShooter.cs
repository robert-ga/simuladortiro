using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;
    int canti = 0;
    private float timeRemaining = 60;
    public Text timeText;
    public Text aciertos;
    public Text preci;
    int pre = 0;
    int p = 0;
    DateTime fecha = DateTime.Today;
    IDbConnection dbcon;

    private void Start()
    {
        
    }
    void Update()
    {
        kills();
        timer();
        precision();
       // regresar();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float timer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        else
        {
            
            timeRemaining = 0;
            if (timeRemaining == 0)
            {
                timeRemaining = 1;
                conexiondb();
                IDbCommand cmnd = dbcon.CreateCommand();
                cmnd.CommandText = "INSERT INTO puntuacion (usuario,kills,precision,tiempo,fecha,id_usuario) VALUES(\'" + recuperar.name + "\',\'" + canti + "\',\'" + precision()+"%" + "\',\'" + timeRemaining + "min" + "\',\'" + fechaa() + "\',\'" + recuperar.idilogin + "\')";
                cmnd.ExecuteNonQuery();
                desconexiondb();
                SceneManager.LoadScene("Estadisticas");
                //print( timeRemaining+ " " + canti + " " + precision());
            }
            //print("1:30    " + kills() +"   "+ p+"%");
            //SceneManager.LoadScene("MenuEntrenador");

        }
        DisplayTime(timeRemaining);
        return timeRemaining;
        //return p.ToString();
    }
    public string kills()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if (target != null)
                {
                    target.Hit();
                    canti++;
                    aciertos.text =canti.ToString();
                     regresar();

                    //print(canti);
                }
            }
        }
        return canti.ToString();
    } 
    public string precision()
    {
        pre = canti * 100;
        p = pre / 30;
        preci.text = p.ToString()+"%";
        return p.ToString(); ;
    }
    public void regresar()
    {
        
        if (canti == 30)
        {
            float t = (60 - (int)Math.Round(timeRemaining));
            conexiondb();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO puntuacion (usuario,kills,precision,tiempo,fecha,id_usuario) VALUES(\'" + recuperar.name + "\',\'" + canti + "\',\'" + precision()+"%" + "\',\'" + t+"seg" + "\',\'" + fechaa() + "\',\'" + recuperar.idilogin + "\')";

            cmnd.ExecuteNonQuery();
            desconexiondb();
            Cursor.visible = true;
            SceneManager.LoadScene("Estadisticas");

            
            //print(recuperar.name+" "+t + " " + canti + " " + precision()+" "+ fechaa()+" "+recuperar.idilogin);
        }
        /*else if (timer() == 0)
        {
            
            //print(recuperar.name + " " + timeRemaining + " " + canti + " " + precision()+" " + fechaa() + " " + recuperar.idilogin);
        }*/
        
    }
    public string fechaa()
    {
        return fecha.ToShortDateString();
    }
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        print("se conecto");
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
    /*private void OnCollisionEnter(Collision other)

    {

        //Debug.Log("Me choco");

        //cambindo de color

        if ("Player" == other.gameObject.tag)

        {
            Destroy(other.gameObject);

        }

    }*/
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System.IO;

public class menuestadisitcajugadores : MonoBehaviour
{
    IDbConnection dbcon;
    public GameObject cell;
    public Transform ce;
    public InputField nombre;
    bool lleno;
    public Button reu;
    public Button ret;
    // Start is called before the first frame update
    void Start()
    {
        //llenardatos();
        //lleno=false;
        // lleno = false;

        reu.gameObject.SetActive(false);
        ret.gameObject.SetActive(false);

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
    public class Datospuntuacion
    {
        public string name;
        public int kills;
        public string precision;
        public string tiempo;
        public string fecha;
        public Datospuntuacion(string name, int kills, string precision, string tiempo, string fecha)
        {

            this.name = name;
            this.kills = int.Parse(kills.ToString());
            this.precision = precision;
            this.tiempo = tiempo;
            this.fecha = fecha;
        }
    }
    public List<Datospuntuacion> getLeaderBoard()
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(),int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
        }

        return datospuntuacion;
    }
    public void llenardatos()
    {
        //limpiardatos();
        
        List<Datospuntuacion> datospuntuacion = getLeaderBoard();
        //print(datospuntuacion);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            
            GameObject da = Instantiate(cell);
            
            Datospuntuacion datosp = datospuntuacion[i];
            
            da.GetComponent<llenarpuntuacionusuarios>().setdatospuntuacion(datosp.name, datosp.kills, datosp.precision, datosp.tiempo, datosp.fecha);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }
    
    public List<Datospuntuacion> buscar(string Search_by_name)
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion where usuario LIKE\'" + Search_by_name + "\'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {       
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
        
            
            //print(reader[1].ToString());
        }

        return datospuntuacion;
    }
    public void llenardatosbuscados()
    {
        //limpiardatos();
        List<Datospuntuacion> datospuntuacion = buscar(nombre.text);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            GameObject da = Instantiate(cell);
            
            Datospuntuacion datosp = datospuntuacion[i];
            da.GetComponent<llenarpuntuacionusuarios>().setdatospuntuacion(datosp.name, datosp.kills, datosp.precision, datosp.tiempo, datosp.fecha);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }
    }
    public void limpiardatos()
    {
        /*List<Datospuntuacion> datospuntuacion = getLeaderBoard();
        GameObject da = Instantiate(cell);
        da.transform.SetParent(ce);
        Destroy(da);*/
        /*if (lleno == true)
        {
            lleno = false;
        }
        else
        {
            lleno= true;
        }
        
        print("1"+lleno);*/
        //scene escena = "EstadisticasUsuarios";
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        //Debug.Log("La escena activa es '" + scene.name + "'.");
        SceneManager.LoadScene("EstadisticasUsuarios");


    }
    public void mostrartodo()
    {
        //print(limpiardatos());


        /*if(lleno == true)
        {
            print("entro");
            
            lleno = false;
            SceneManager.LoadScene("EstadisticasUsuarios");
            print("se acrtualizo");
            
            if(lleno == false)
            {
                print("ingreso");
                llenardatos();
                
                print("se lleno");
                lleno = false;
            }
            
        }
        
        else if(lleno==false)
        {
            llenardatos();
            lleno=(true);
            print("es true");
        }*/
        ret.gameObject.SetActive(true);
        reu.gameObject.SetActive(false);
        llenardatos();
        

    }
    public void usuariopuntuacion()
    {
        //List<Datospuntuacion> datospuntuacion = buscar(nombre.text);
        //llenardatos();
        //limpiardatos();
        //limpiardatos();
        reu.gameObject.SetActive(true);
        ret.gameObject.SetActive(false);
        llenardatosbuscados();
    }
    public void CrearArchivoCSVusu(string Search_by_name)
    {
        string ruta = @"C:/Users/User/Desktop/Simulador de Tiro/Reportes/" + "reporte de " + Search_by_name + ".csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("usuario;kills;precision;tiempo;fecha");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion where usuario LIKE\'" + Search_by_name + "\'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
        }
        tw = new StreamWriter(ruta, true);
        foreach (var i in datospuntuacion)
        {
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.fecha.ToString());

        }
        tw.Close();
        Application.OpenURL(ruta);
    }
    public void CrearArchivoCSVtodo()
    {
        string ruta = @"C:/Users/User/Desktop/Simulador de Tiro/Reportes/" + "reporte de todas las puntuaciones.csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("usuario;kills;precision;tiempo;fecha");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
        }
        tw = new StreamWriter(ruta, true);
        foreach (var i in datospuntuacion)
        {
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.fecha.ToString());

        }
        tw.Close();
        Application.OpenURL(ruta);
    }
    public void reporteusu()
    {
        CrearArchivoCSVusu(nombre.text);
    }
    public void reportetodo()
    {
        CrearArchivoCSVtodo();
    }
    public void atras()
    {
        
        SceneManager.LoadScene("MenuAdministrador");
    }
}

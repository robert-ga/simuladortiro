using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

public class manuestadisticajugador : MonoBehaviour
{
    public Text nombrere;
    public GameObject cell;
    public Transform ce;
    IDbConnection dbcon;
    DateTime fecha = DateTime.Today;
    // Start is called before the first frame update
    void Start()
    {
        nombrere.text = recuperar.name;
        Cursor.lockState = CursorLockMode.None;
        llenaresadisticarusuarios();
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generarreporteusuario()
    {
        CrearArchivoCSV(nombrere.text);
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
    public void llenaresadisticarusuarios()
    {
        List<Datospuntuacion> datospuntuacion = buscar(nombrere.text);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            GameObject da = Instantiate(cell);
            Datospuntuacion datosp = datospuntuacion[i];
            da.GetComponent<llenarusuariopuntuacion>().setdatospuntuacion(datosp.name, datosp.kills, datosp.precision, datosp.tiempo, datosp.fecha);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }
    public void CrearArchivoCSV(string Search_by_name)
    {
        string ruta = @"C:/Users/User/Desktop/Simulador de Tiro/Reportes/"+"reporte de "+Search_by_name+".csv";
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
        /*var sr = File.CreateText(ruta);
        string datosCSV = "";
        //datosCSV += puntuaciones[0].userId.ToString() + ",\n";
        //datosCSV += "\n," + "\n," + "\n" + "\n," + "\n";*/
        
        tw= new StreamWriter(ruta,true);
        //datosCSV += "usuario," + "kills," + "precision," + "tiempo," + "fecha," + "\n";
        foreach (var i in datospuntuacion)
        {
            //datosCSV += i.name.ToString() + "," +int.Parse(i.kills.ToString()) + "," + i.precision.ToString() + "," + i.tiempo.ToString() + "," +i.fecha.ToString() + ",\n";
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.fecha.ToString());

        }
        //Crear el archivo
        /*sr.WriteLine(datosCSV);
        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(ruta);
        fInfo.IsReadOnly = true;*/
        //Cerrar
        tw.Close();
        //Abrimos archivo recién creado
        Application.OpenURL(ruta);
    }
    public void atras()
    {
        SceneManager.LoadScene("MenuEntrenador");
    }
}

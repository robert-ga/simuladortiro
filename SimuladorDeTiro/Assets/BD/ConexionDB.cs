using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class ConexionDB : MonoBehaviour
{
    IDbConnection dbcon;
    public InputField usuarioregistro;
    public InputField contrasenaregistro;
    public void Start()
    {
       readdb();
    }
    public void readdb()
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT usuario,contrasena FROM usuarios";
        //cmnd.ExecuteNonQuery();
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            string usuario = reader.GetString(0);
            string contrasena = reader.GetString(1);
            Debug.Log("usuario= " + usuario + "  contraseña =" + contrasena);
        }
        //desconexiondb();
    }
    public void registrarusuario()
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO usuarios (usuario,contrasena) VALUES(\'" + usuarioregistro.text + "\',\'" + contrasenaregistro.text + "\')";
        cmnd.ExecuteNonQuery();
        desconexiondb();
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
}

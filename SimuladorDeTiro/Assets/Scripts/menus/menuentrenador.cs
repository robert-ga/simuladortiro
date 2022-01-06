using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuentrenador : MonoBehaviour
{
    public Text nombrere;

    // Start is called before the first frame update
    private void Start()
    {
        nombrere.text = recuperar.name;
        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public void entrenar()
    {
        SceneManager.LoadScene("Entrenamiento");
    }
    public void estadistica()
    {
        SceneManager.LoadScene("Estadisticas");
    }
    public void salir()
    {
        SceneManager.LoadScene("Login");
    }
    
}

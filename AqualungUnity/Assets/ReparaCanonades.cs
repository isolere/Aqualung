using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparaCanonades : MonoBehaviour
{
    //Controlarem les canonades reparades durant el nivell 1
    private int _canonadesReparades;

    public int CanonadesReparades
    {
        get { return _canonadesReparades; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _canonadesReparades = 0;
    }

    //Controlarem la quantitat de canonades reparades per l'objectiu de la missió del nivell 1
    public void ReparaCanonada()
    {
        _canonadesReparades += 1;
        Debug.Log("Reparada= " + _canonadesReparades);
    }
}

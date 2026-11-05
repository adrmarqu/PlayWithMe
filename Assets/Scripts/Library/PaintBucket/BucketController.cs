using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    [SerializeField]
    GameObject screwObj;             // Destornillador fisico

    GameObject screw;               // Destornillador animado
    GameObject detectScrew;         // Objeto que detecta si el destornillador esta cerca

    DetectSystem _detect;
    Animator _anim;
    
    [SerializeField]
    InputSystemKeyboard _input;

    bool extOpened;                 // Si la tapa exterior esta abierta
    bool intOpened;                 // Si la tapa interior esta abierta
    bool fullOpen;                  // Si las dos tapas estan abiertas

    void Awake()
    {
        screw = gameObject.transform.GetChild(0).GetChild(2).gameObject;

        _anim = screw.GetComponent<Animator>();
    }

    void Start()
    {
        extOpened = false;
        intOpened = true;
        fullOpen = false;

        detectScrew = gameObject.transform.GetChild(0).GetChild(1).GetChild(5).gameObject;

        _detect = detectScrew.GetComponent<DetectSystem>();

        screw.SetActive(false);

        //_input = FindObjectOfType<InputSystemKeyboard>();
    }


    void Update()
    {
        if (_input.interact)
        {
            if (intOpened && !extOpened && _detect.GetCurrentState())
            {

                screwObj.SetActive(false);          // Desactivar objeto
                screw.SetActive(true);              // Activar objeto animado
                _anim.SetTrigger("openFirstPlug");  // Iniciar animación
                intOpened = false;

            }
            else if (!intOpened && extOpened && _detect.GetCurrentState())
            {

                screwObj.SetActive(false);          // Desactivar objeto
                screw.SetActive(true);              // Activar objeto animado
                _anim.SetTrigger("openSecondPlug"); // Iniciar animación
                intOpened = true;                   // Segunda tapa abierta
                fullOpen = true;

            }
        }
    }

    // Devuelve el objeto destornillador no animado
    public GameObject GetScrewObject()
    {
        return screwObj;
    }

    // Al acabar la animación para que las animaciones del objeto no se hagan a la vez
    public void OpenendPlug()
    {
        extOpened = true;
    }

    // Devulve el estado del cubo
    public bool FullOpenedState()
    {
        return fullOpen;
    }
}

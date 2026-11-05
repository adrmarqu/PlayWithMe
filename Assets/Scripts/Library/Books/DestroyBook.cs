using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para cambiar el material del libro
// Este scripta cambiara el material cuando el libro se destruya
// Materiales: Libro pintado, libro mojado, libro cortado

public class DestroyBook : MonoBehaviour
{
    public static bool bookDestroyed;   // Si el libro ha sido destruido

    [SerializeField]
    GameObject canvasLibrary;
    
    [SerializeField]
    Material wetMat;
    
    [SerializeField]
    Material cutMat;

    [SerializeField]
    GameObject realBucket;       // Cubo de pintura fisico

    [SerializeField]
    int indexDestructible;

    [SerializeField]
    GameObject waterCup;        // Taza de agua fisico
    
    [SerializeField]
    GameObject cutter;          // Tijeras fisico

    Animator[] _anim = new Animator[5];
    
    GameObject bucket;          // Cubo de pintura animado
    GameObject cup;             // Taza de agua animado
    GameObject scissor;         // Tijeras animado

    GameObject paintSphere;     // Esfera de pintura animada

    GameObject detectBucket;    // Objeto que detecta si hay cubo de pintura cerca
    GameObject detectWater;     // Objeto que detecta si hay taza de agua cerca
    GameObject detectCutter;    // Objeto que detecta si hay tijeras cerca

    DetectSystem _dp;           // Detect paint
    DetectWater _dw;            // Detect water
    DetectSystem _ds;           // Detect scissors

    Renderer _render;
    DestroyedBookMaterial _dbm;     // Canvas
    
    [SerializeField]
    InputSystemKeyboard _input;
    
    BucketController _controller;   // Mira el estado del cubo de pintura

    [SerializeField]
    GameObject gameManager;
    
    GameManager _task;

    void Awake()
    {
        paintSphere = gameObject.transform.GetChild(0).GetChild(0).gameObject;  // El objeto de pintura animado

        detectBucket = gameObject.transform.GetChild(3).gameObject;             // El objeto que detectara si el cubo de pintura esta al alcance
        detectWater = gameObject.transform.GetChild(4).gameObject;             // El objeto que detectara si la taza de agua esta al alcance
        detectCutter = gameObject.transform.GetChild(5).gameObject;             // El objeto que detectara si las tijeras esta al alcance

        bucket = gameObject.transform.GetChild(0).gameObject;                   // El objeto de cubo que hara la animacion
        cup = gameObject.transform.GetChild(1).gameObject;                      // El objeto de taza de agua que hara la animacion
        scissor = gameObject.transform.GetChild(2).gameObject;                  // El objeto de tijeras que hara la animacion

        _render = GetComponent<Renderer>();     

        _dp = detectBucket.GetComponent<DetectSystem>();
        _dw = detectWater.GetComponent<DetectWater>();
        _ds = detectCutter.GetComponent<DetectSystem>();

        _anim[0] = bucket.GetComponent<Animator>();
        _anim[1] = cup.GetComponent<Animator>();
        _anim[2] = scissor.GetComponent<Animator>();

        _task = gameManager.GetComponent<GameManager>();

        _controller = realBucket.gameObject.transform.parent.GetComponent<BucketController>();  // Para mirar si el cubo esta abierto o no

    }

    void Start()
    {
        _dbm = canvasLibrary.GetComponent<DestroyedBookMaterial>();     // Para pintar el canvas (cambiar color)

        bucket.SetActive(false);
        cup.SetActive(false);
        scissor.SetActive(false);

        bookDestroyed = false;                      
    }

    // Pintar el libro

    public void PaintBook()
    {
        _render.materials[1].color = SelectPaintingColor.colorPainting;     // Cambiar color objeto
        _dbm.BookPainted();                                                 // Cambiar canvas

        realBucket.transform.GetChild(0).gameObject.SetActive(false);       // Quitar la pintura fisica

        DestroyObjective();
    }

    // Mojar el libro

    public void WetBook()
    {
        _render.materials[1].CopyPropertiesFromMaterial(wetMat);            // Cambiar material objeto
        _dbm.BookWet();                                                     // Cambiar canvas

        waterCup.transform.GetChild(0).gameObject.SetActive(false);         // Quitar el agua fisica

        DestroyObjective();
    }

    // Cortar el libro

    public void CutBook()
    {
        _render.materials[1].CopyPropertiesFromMaterial(cutMat);            // Cambiar material objeto
        _dbm.BookCut();                                                     // Cambiar canvas

        DestroyObjective();
    }

    void Update()
    {
        if (_input.interact && !bookDestroyed)
        {
            if (_dp.GetCurrentState() && _controller.FullOpenedState())
            {
                realBucket.SetActive(false);
                bucket.SetActive(true);

                _anim[0].SetTrigger("paint");  // Animación de pintar
            }
            else if (_dw.GetCurrentState())
            {
                waterCup.SetActive(false);
                cup.SetActive(true);

                _anim[1].SetTrigger("wet");  // Animación de mojar
            }
            else if (_ds.GetCurrentState())
            {
                cutter.SetActive(false);
                scissor.SetActive(true);

                _anim[2].SetTrigger("cut");  // Animación de cortar
            }
        }
    }

    public GameObject GetPaintSphere()  // Esfera de pintura del cubo animado
    {
        return paintSphere;
    }

    public GameObject GetBucketObj()
    {
        return realBucket;
    }

    public GameObject GetCupObj()
    {
        return waterCup;
    }

    public GameObject GetScissorObj()
    {
        return cutter;
    }

    public void DestroyObjective()
    {
        bookDestroyed = true;

        _task.TaskComplete(indexDestructible);
    }
}

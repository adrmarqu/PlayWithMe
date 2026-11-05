using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Para pintar los libros
public class PaintBooks : MonoBehaviour
{
    [SerializeField]
    Material[] materials;

    GameObject bookPart;

    int bookList;   // Cantidad de libros
    int numParts;   // Numero de partes de un libro (4 partes)

    void Start()
    {
        SetMaterialBook();
    }

    void SetMaterialBook()
    {
        bookList = gameObject.transform.childCount;

        for (int i = 0; i < bookList; i++)  // Pasa por cada libro
        {
            // Seleccionar material

            int x = Random.Range(0, materials.Length);

            // Cantidad de partes de un libro

            numParts = gameObject.transform.GetChild(i).childCount;

            for (int j = 1; j < numParts; j++)  // Pasa por cada parte
            {
                // Seleccionar parte de libro y pintar

                bookPart = gameObject.transform.GetChild(i).GetChild(j).gameObject;

                bookPart.GetComponent<Renderer>().material = materials[x];
            }
        }
    }
}

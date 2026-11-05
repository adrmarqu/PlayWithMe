using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectUpdateCanvas : MonoBehaviour
{
    [SerializeField]
    Text carNumber;
    [SerializeField]
    Text pieceNumber;

    int numCar;
    int numPiece;

    void OnEnable()
    {
        Collectable.collectObject += updateCollect;
    }
    void OnDisable()
    {
        Collectable.collectObject -= updateCollect;
    }
    void Start()
    {
        numCar = 0;
        numPiece = 0;
    }

    void updateCollect(int k)
    {
        if (k == 0)
        {
            Car();
        }
        else
        {
            Piece();
        }
    }

    void Car()
    {
        numCar++;
        carNumber.text = numCar + "/1";
    }

    void Piece()
    {
        numPiece++;
        pieceNumber.text = numPiece + "/10";
    }

    public int ReturnCar()
    {
        return numCar;
    }

    public int ReturnPieces()
    {
        return numPiece;
    }
}

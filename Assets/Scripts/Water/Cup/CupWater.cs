using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupWater : MonoBehaviour
{
    [SerializeField]
    Transform mLiquid;

    MeshRenderer water;

    float waterPlacement;
    float placementVelocity;
    float heightWater;

    void Start()
    {
        placementVelocity = 0.02f;
        heightWater = 0.1f;
        water = mLiquid.GetComponent<MeshRenderer>();
        water.material.SetFloat("_HeightMax", heightWater);
        water.material.SetFloat("_Placement", placementVelocity);
        
    }
    void Update()
    {
        waterPlacement += placementVelocity;
        water.material.SetFloat("_Placement", waterPlacement);
    }
}

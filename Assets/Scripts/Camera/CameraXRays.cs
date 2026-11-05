using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraXRays : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    CapsuleCollider rayXCapsule;

    Collider[] saveObj;
    Material[] m;
    Collider[] hit;

    float distance;
    float offset;
    float minDist;

    Vector3 direction;
    Vector3 localP0;
    Vector3 localP1;
    Vector3 point0;
    Vector3 point1;

    void Start()
    {
        saveObj = new Collider[200];
        m = new Material[200];
        minDist = 2.6f;

        for (int i = 0; i < saveObj.Length; i++)
        {
            saveObj[i] = null;
            m[i] = null;
        }
        
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.localPosition, transform.localPosition);

        offset = rayXCapsule.height / 2 - rayXCapsule.radius;
        direction = new Vector3 { [rayXCapsule.direction] = 1 };
        localP0 = rayXCapsule.center - direction * offset;
        localP1 = rayXCapsule.center + direction * offset;
        point0 = rayXCapsule.transform.TransformPoint(localP0);
        point1 = rayXCapsule.transform.TransformPoint(localP1);

        hit = Physics.OverlapCapsule(point0, point1, rayXCapsule.radius);

        for (int i = 0; i < m.Length; i++)
        {
            if (m[i] != null)
            {
                m[i].SetFloat("_Mode", 0);
                m[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                m[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                m[i].SetInt("_ZWrite", 1);
                m[i].DisableKeyword("_ALPHATEST_ON");
                m[i].DisableKeyword("_ALPHABLEND_ON");
                m[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                m[i].renderQueue = -1;
                m[i].color = new Color(m[i].color.r, m[i].color.g, m[i].color.b, 1f);
                m[i] = null;
            }
        }
        if (distance > minDist)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i] != null && hit[i].gameObject.layer != Constants.layerWalls)
                {
                    if (m[i] == null)
                    {
                        if (hit[i].TryGetComponent<MeshRenderer>(out MeshRenderer outM))
                        {
                            m[i] = hit[i].GetComponent<MeshRenderer>().material;
                            m[i].SetFloat("_Mode", 3);
                            m[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                            m[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                            m[i].SetInt("_ZWrite", 0);
                            m[i].DisableKeyword("_ALPHATEST_ON");
                            m[i].DisableKeyword("_ALPHABLEND_ON");
                            m[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                            m[i].renderQueue = 3000;
                            m[i].color = new Color(m[i].color.r, m[i].color.g, m[i].color.b, 0f);
                            saveObj[i] = hit[i].GetComponent<Collider>();
                        }
                        else if (hit[i].transform.childCount > 0)
                        {
                            for (int j = 0; j < hit[i].transform.childCount; j++)
                            {
                                if (hit[i].transform.GetChild(j).TryGetComponent<MeshRenderer>(out MeshRenderer outCM))
                                {
                                    m[i + j] = hit[i].transform.GetChild(j).GetComponent<MeshRenderer>().material;
                                    m[i + j].SetFloat("_Mode", 3);
                                    m[i + j].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                                    m[i + j].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                                    m[i + j].SetInt("_ZWrite", 0);
                                    m[i + j].DisableKeyword("_ALPHATEST_ON");
                                    m[i + j].DisableKeyword("_ALPHABLEND_ON");
                                    m[i + j].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                                    m[i + j].renderQueue = 3000;
                                    m[i + j].color = new Color(m[i + j].color.r, m[i + j].color.g, m[i + j].color.b, 0f);
                                    saveObj[i + j] = hit[i].transform.GetChild(j).GetComponent<Collider>();
                                }
                            }
                        }
                    }

                    if (hit[i] != saveObj[i] && m[i] != null)
                    {
                        m[i].SetFloat("_Mode", 0);
                        m[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        m[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                        m[i].SetInt("_ZWrite", 1);
                        m[i].DisableKeyword("_ALPHATEST_ON");
                        m[i].DisableKeyword("_ALPHABLEND_ON");
                        m[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        m[i].renderQueue = -1;
                        m[i].color = new Color(m[i].color.r, m[i].color.g, m[i].color.b, 1f);
                        m[i] = null;
                    }

                    if (!hit[i].TryGetComponent<MeshRenderer>(out MeshRenderer mR))
                    {
                        if (hit[i].transform.childCount > 0)
                        {
                            for (int j = 0; j < hit[i].transform.childCount; j++)
                            {
                                if (hit[i].transform.GetChild(j) != saveObj[i + j] && m[i + j] != null)
                                {
                                    if (hit[i].transform.GetChild(j).TryGetComponent<MeshRenderer>(out MeshRenderer outCM))
                                    {
                                        m[i + j].SetFloat("_Mode", 0);
                                        m[i + j].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                                        m[i + j].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                                        m[i + j].SetInt("_ZWrite", 1);
                                        m[i + j].DisableKeyword("_ALPHATEST_ON");
                                        m[i + j].DisableKeyword("_ALPHABLEND_ON");
                                        m[i + j].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                                        m[i + j].renderQueue = -1;
                                        m[i + j].color = new Color(m[i + j].color.r, m[i + j].color.g, m[i + j].color.b, 1f);
                                        m[i + j] = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}

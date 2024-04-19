using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConeVision : MonoBehaviour
{
    [Header("Settings")]
    public Material VisionMaterial;
    public EnemyAIVision EnemyVision;
    public LayerMask LayerWalls;
    private float _visionRange;
    private float _visionAngle;
    public int VisionConeFidelity;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    
    void Start()
    {
        EnemyVision = GetComponent<EnemyAIVision>();
        _visionRange = EnemyVision.ViewDistance;
        _visionAngle = EnemyVision.Radius;

        transform.AddComponent<MeshRenderer>().material = VisionMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        _visionAngle *= Mathf.Deg2Rad;

        VisionConeFidelity = 120;
    }

    
    void Update()
    {

        DrawVisionCone();
    }

    void DrawVisionCone()
    {
	int[] triangles = new int[(VisionConeFidelity - 1) * 3];
    	Vector3[] Vertices = new Vector3[VisionConeFidelity + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -_visionAngle / 2;
        float angleIcrement = _visionAngle / (VisionConeFidelity - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeFidelity; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, _visionRange, LayerWalls))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * _visionRange;
            }


            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }


}
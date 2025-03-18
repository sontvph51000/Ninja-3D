using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public EnemyBase enemyBase;
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer; // Layer chứa các vật cản
    public int VisionConeResolution = 120;  // Độ phân giải vùng nhìn
    public Transform player;                // Tham chiếu tới Player
    public Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    private bool isVisible;

    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad; // Chuyển sang radian
    }

    void Update()
    {

    }

    public void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), RaycastDirection, out RaycastHit hit, VisionRange))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
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

    

    public void CheckPlayerInSight()
    {
        if (player == null || enemyBase == null) return;

        // Tính hướng và khoảng cách đến Player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Kiểm tra nếu Player nằm trong khoảng cách và góc nhìn
        if (distanceToPlayer <= VisionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= VisionAngle * Mathf.Rad2Deg / 2)
            {
               
                if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), directionToPlayer, distanceToPlayer, VisionObstructingLayer))
                {
                    enemyBase.playerDetected = false;
                }
                else
                {
                    enemyBase.ChangeState(new AttackStateExample(enemyBase));
                    //isVisible = true;
                    //enemyBase.playerDetected = true;
                    //enemyBase.lastKnownPosition = player.transform.position;
                    //Vector3 directionToPlayerXZ = new Vector3(directionToPlayer.x, 0, directionToPlayer.z); // Giữ nguyên hướng XZ, bỏ trục Y
                    //Quaternion lookRotation = Quaternion.LookRotation(directionToPlayerXZ);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7f); // Tốc độ quay

                    return;
                }

            }
           
        }
        if(distanceToPlayer > VisionRange)
        {
            if(isVisible)
            {
                Debug.Log("Chạy ra ngoài");
                isVisible = false;
              //  enemyBase.MoveToLastKnownPosition();
            }
        }
        else
        {

            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer > VisionAngle * Mathf.Rad2Deg / 2)
            {
                if (isVisible)
                {
                    Debug.Log("Chạy ra ngoài");
                    isVisible = false;
                 //   enemyBase.MoveToLastKnownPosition();
                }
            }
            else
            {
                if (!Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), directionToPlayer, distanceToPlayer, VisionObstructingLayer))
                {
                    if (isVisible)
                    {
                        Debug.Log("Chạy ra ngoài");
                        isVisible = false;
                  //      enemyBase.MoveToLastKnownPosition();
                    }
                }
            }
        }
           
    }

    public void CheckingPlayerInSight()
    {
        if (player == null || enemyBase == null) return;

        // Tính hướng và khoảng cách đến Player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Kiểm tra nếu Player nằm trong khoảng cách và góc nhìn
        if (distanceToPlayer <= VisionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= VisionAngle * Mathf.Rad2Deg / 2)
            {

                if (!Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), directionToPlayer, distanceToPlayer, VisionObstructingLayer))
                {
                    enemyBase.ChangeState(new AttackStateExample(enemyBase));
                }

            }
        }
    }

    public void CheckingPlayerOutSight()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > VisionRange)
        {
            Debug.Log("AAAAAAAAA");
            enemyBase.ChangeState(new RunStateExample(enemyBase));
            return;
        }
        else
        {

            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer > VisionAngle * Mathf.Rad2Deg / 2)
            {
                Debug.Log("AAAAAAAAA");
                enemyBase.ChangeState(new RunStateExample(enemyBase));
                return;
            }
            else
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), directionToPlayer, distanceToPlayer, VisionObstructingLayer))
                {
                    Debug.Log("AAAAAAAAA");
                    enemyBase.ChangeState(new RunStateExample(enemyBase));
                }
            }
        }
    }
}

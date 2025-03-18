using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; // Sử dụng NavMeshAgent để di chuyển

public class EnemyController : MonoBehaviour
{

    NavMeshAgent navMeshAgent;              // NavMeshAgent để điều khiển Enemy

    public Vector3 lastKnownPosition;      // Điểm cuối cùng mà Player xuất hiện
    public bool playerDetected = false;   // Kiểm tra xem Player có trong vùng nhìn không
    private bool isRotating = false;       // Kiểm tra Enemy đang quay
    private bool facingRight = true;       // Trạng thái hướng quay
    public bool isTrackingPlayer = false; // Enemy đang theo dõi Player
    public EnemyState enemyState;

    private IEnemyState currentState; // Trạng thái hiện tại của Enemy
    public Transform[] patrolPoints; // Các điểm tuần tra
    public Transform player; // Tham chiếu tới người chơi
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public float speed = 2f; // Tốc độ di chuyển

    public int currentPatrolIndex = 0; // Điểm tuần tra hiện tại

    void Start()
    {
        // Bắt đầu với trạng thái Idle
        SetState(new IdleState());

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not attached to the Enemy! Add a NavMeshAgent component.");
        }

        StartCoroutine(RotateEveryThreeSeconds()); // Bắt đầu coroutine quay 180 độ
    }

    //void Update()
    //{


    //    if (playerDetected)
    //    {
    //        MoveToLastKnownPosition(); // Di chuyển tới vị trí cuối cùng
    //    }
    //}
    void Update()
    {
        currentState.UpdateState(this);

        //if (playerDetected)
        //{
        //    isTrackingPlayer = true; // Đang theo dõi Player
        //}
        //else if (!isTrackingPlayer)
        //{
        //    MoveToLastKnownPosition(); // Chỉ di chuyển nếu không theo dõi Player
        //}
    }



    //void MoveToLastKnownPosition()
    //{
    //    if (navMeshAgent != null && lastKnownPosition != Vector3.zero)
    //    {
    //        isRotating = false;
    //        navMeshAgent.SetDestination(lastKnownPosition); // Di chuyển tới vị trí cuối cùng
    //    }
    //}
    #region Move&Rotate
    public void MoveToLastKnownPosition()
    {
        //navMeshAgent.SetDestination(lastKnownPosition);
    }

    IEnumerator RotateEveryThreeSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Chờ 3 giây

            if (!isRotating && !playerDetected) // Quay nếu không đang quay và không phát hiện Player
            {
                StartCoroutine(RotateToOppositeDirection());
            }
        }
    }

    IEnumerator RotateToOppositeDirection()
    {
        isRotating = true;

        // Xác định góc quay dựa trên trạng thái hướng hiện tại
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (facingRight)
        {
            endRotation = startRotation * Quaternion.Euler(0, 180, 0); // Quay sang trái
        }
        else
        {
            endRotation = startRotation * Quaternion.Euler(0, -180, 0); // Quay sang phải
        }

        facingRight = !facingRight; // Đảo trạng thái hướng quay

        float elapsedTime = 0f;
        float rotationDuration = 1f; // Thời gian quay (1 giây)

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Đảm bảo kết thúc tại góc mong muốn
        isRotating = false;
    }

    #endregion
    public void SetState(IEnemyState newState)
    {
        // Rời trạng thái hiện tại (nếu có)
        currentState?.ExitState(this);

        // Chuyển sang trạng thái mới
        currentState = newState;

        // Kích hoạt trạng thái mới
        currentState.EnterState(this);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        // Di chuyển tới một vị trí cụ thể
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public bool IsPlayerInRange()
    {
        // Kiểm tra nếu người chơi trong phạm vi
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }
}

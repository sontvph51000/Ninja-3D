using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private EnemyBase enemyBase;
    public GameObject bulletPrefab;
    private float bulletSpeed = 1000f;


    void Start()
    {
        enemyBase = GetComponentInParent<EnemyBase>();

    }

    public void Shot()
    {
        if (enemyBase.Player == null) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Tính hướng bắn (từ vị trí enemy đến vị trí Player)
            Vector3 direction = (enemyBase.Player.transform.position - transform.position).normalized;

            // Thêm lực để bắn viên đạn
            rb.AddForce(direction * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        Destroy(bullet, 1.5f);
    }


}

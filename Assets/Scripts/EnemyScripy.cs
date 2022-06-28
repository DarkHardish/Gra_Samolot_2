using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripy : MonoBehaviour
{
    public Transform []gunPoint;
    public GameObject enemyBullet;
    public GameObject enemyExplosionPrefab;
    public HealthBar healthbar;
    public GameObject damageEffect;
    public float enemyBulletSpawnTime = 1f;
    public float speed = 1f;
    public float health = 10f;
    public GameObject coinPrefab;

    float barSize = 1f;
    float damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShooting());
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            DamageHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVfx= Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if(health<=0)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
            }
        }
    }
    void DamageHealthBar()
    {
        if (health>0)
        {
            health -= 1;
            barSize = barSize - damage;
            healthbar.SetSize(barSize);
        }
    }
    void EnemyFire()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
        //Instantiate(enemyBullet, gunPoint1.position, Quaternion.identity);
        //Instantiate(enemyBullet, gunPoint2.position, Quaternion.identity);
        //Instantiate(enemyBullet, gunPoint3.position, Quaternion.identity);
        //Instantiate(enemyBullet, gunPoint4.position, Quaternion.identity);

    }
    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            EnemyFire();
        }
    }
}

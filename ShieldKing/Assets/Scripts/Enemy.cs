using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    Transform tr_Player;
    float f_RotSpeed = 3.0f, f_MoveSpeed = 3.0f;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = tr_Player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * f_MoveSpeed);

        /* Move at Player*/
        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        Instantiate(enemy, new Vector3(2.0f, 0, 0), Quaternion.identity);

        yield return new WaitForSeconds(5);
    }
}
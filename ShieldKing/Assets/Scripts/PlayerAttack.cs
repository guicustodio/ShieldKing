using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float pushForce;

    // Start is called before the first frame update
    void Start()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            
            Rigidbody col = collision.collider.GetComponent<Rigidbody>();

            if (col != null)
            {
               Vector3 direction = collision.transform.position - player.transform.position;
                direction.y = 0;

                col.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
            }
            //col.AddExplosionForce(pushForce, transform.position, radius);
        }
    }

}

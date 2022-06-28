using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool homing = true;
    public GameObject boss;
    public GameObject target;
    public float speed = 10f;
    public Rigidbody2D projectileRB2d;
    public Vector2 direction;
    public float lifeSpan = 3f;
    // Start is called before the first frame update
    void Start()
    {
        projectileRB2d = GetComponent<Rigidbody2D>();
        if(homing)StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        if(homing){
            projectileRB2d.AddForce(-(transform.position - target.transform.position).normalized * speed);
            direction = projectileRB2d.velocity;
            RotateTowardsTarget();
        }
        if(!homing){

        }
        
    }
    IEnumerator Death(){
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
    public void SendToTarget(){
        projectileRB2d = GetComponent<Rigidbody2D>();
        //projectileRB2d.AddForce(-(transform.position - target.transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground") && homing){
            Destroy(gameObject);
        }
    }
}

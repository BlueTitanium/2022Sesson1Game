using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject target;
    public Vector3 direction;
    public float[] cooldowns;
    public float maxHp;
    public float currentHp;
    public bool followTarget = true;
    public float speed = .05f;
    public float normalSpeed;
    public float maxSpeed = 20f;
    public bool lunge = false;
    public GameObject projectilePrefab;
    public GameObject Shield;
    public GameObject Shockwave;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        speed = normalSpeed;
        Physics2D.IgnoreCollision(target.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(),true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(followTarget && !lunge){
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime*speed);
        } else if (followTarget && lunge){
            transform.Translate(new Vector3(direction.normalized.x, direction.normalized.y*.5f,0) * Time.deltaTime*speed);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            direction = target.transform.position - transform.position;
            StartCoroutine(Lunge());
        }
        if(Input.GetKeyDown(KeyCode.E)){
            SpawnHoming();
        }
        if(Input.GetKeyDown(KeyCode.R)){

            ShockwaveAttack();
        }
    }

    private void FixedUpdate() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            //Shield.GetComponent<Shield>().SetGravity(0f);
            rb2d.gravityScale = 0f;
            StartCoroutine(Shockwave.GetComponent<Shockwave>().wave());
            Shield.SetActive(true);
            followTarget = true;
        }
    }
    
    void ShockwaveAttack(){
        followTarget = false;
        rb2d.gravityScale = 3f;
        //Shield.GetComponent<Shield>().SetGravity(1f);
        Shield.SetActive(false);
        //yield return StartCoroutine(Shockwave.GetComponent<Shockwave>().wave());
        //followTarget = true;
    }

    void SpawnHoming(){
        GameObject proj = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        proj.GetComponent<Projectile>().homing = true;
        proj.GetComponent<Projectile>().boss = this.gameObject;
        proj.GetComponent<Projectile>().target = target;
        proj.GetComponent<Projectile>().SendToTarget();
    }

    IEnumerator Lunge(){
        speed = Mathf.Lerp(normalSpeed,maxSpeed,.5f);
        lunge = true;
        yield return new WaitForSeconds(.5f);
        lunge = false;
        speed = normalSpeed;
    }

    //
    /*
    =========
    ATTACKS
    ---------
    [0] lunge attack
    [1] homing projectile
    [2] Shielding Projectiles
    [3] Shockwave
    [4] Explosive teleport
    ---------
    =========
    PHASES
    ---------
    Phase 1
    Lunge attacks and homing projectiles
    66% hp
    Phase 2
    + Shielding Projectiles + Shockwave
    33% hp
    Phase 3
    + shockwave
    */
}

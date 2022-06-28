using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        speed = normalSpeed;
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
    }

    private void FixedUpdate() {
        
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

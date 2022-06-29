using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public List<GameObject> shields;
    public float radius = 10f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shields.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / shields.Count;
            shields[i].transform.localPosition = (new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius));
        }
    }
    public void SetGravity(float grav){
        for (int i = 0; i < shields.Count; i++)
        {
            shields[i].GetComponent<Rigidbody2D>().gravityScale = grav;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * speed);
    }

    public void ChangeRadius(float r){
        radius = r;
        for (int i = 0; i < shields.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / shields.Count;
            shields[i].transform.localPosition = (new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public List<GameObject> hitBoxes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator wave(){
        hitBoxes[0].SetActive(true);
        yield return new WaitForSeconds(.25f);
        hitBoxes[1].SetActive(true);
        yield return new WaitForSeconds(.25f);
        hitBoxes[0].SetActive(false);
        hitBoxes[2].SetActive(true);
        yield return new WaitForSeconds(.25f);
        hitBoxes[1].SetActive(false);
        yield return new WaitForSeconds(.25f);
        hitBoxes[2].SetActive(false);
    }
}

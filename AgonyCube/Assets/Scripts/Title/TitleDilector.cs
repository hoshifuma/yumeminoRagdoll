using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDilector : MonoBehaviour {

    public GameObject star;



	// Use this for initialization
	void Start () {
        StartCoroutine(starCreate());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator starCreate()
    {
        while (true) {
            Vector3 pos = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-2.0f, 5.0f), 0);

            Quaternion q = new Quaternion();

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

            GameObject starClone;

            starClone = Instantiate(star, pos, q);


            yield return new WaitForSeconds(1.5f);

            Destroy(starClone);
        }
    }

}

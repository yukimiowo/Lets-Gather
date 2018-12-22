using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fall(){
        StartCoroutine("Up");
    }

    private IEnumerator  Up(){
        while(this.transform.position.y < 5){
            this.transform.position += new Vector3(0.0f, 0.5f, 0.0f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }
}

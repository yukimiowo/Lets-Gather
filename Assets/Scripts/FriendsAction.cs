using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsAction : MonoBehaviour {

    GameObject Me;
    Vector3 MyPosition;
    Vector3 FriendsPosition;
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Meのいるところに集まる
    public void Gather(){
        MyPosition = GameObject.Find("Me").transform.position;
        FriendsPosition = this.transform.position;
        //StartCoroutine("Move");s

        if (Vector3.Distance(MyPosition, FriendsPosition) < 0.8f){
            transform.position = Vector3.Lerp(FriendsPosition, MyPosition, 0.5f);

            Manager.AmountOfFriends++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
    }

    //private IEnumerator Move(){
    //    while(true){
    //        transform.position = Vector3.MoveTowards(FriendsPosition, MyPosition, Time.deltaTime * 2f);
    //        //if(Vector3.Distance(MyPosition, FriendsPosition) < 0.25){
    //        //    break;
    //        //}
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}
}

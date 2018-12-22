using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAction : MonoBehaviour {

    private float speed = 0.05f;
    private GameObject[] targets;
    private AudioSource audioSource;
    public static bool gameover;

    // Use this for initialization
    void Start () {
        gameover = true;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameover)
        {
            // 左に移動
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //this.transform.Translate(-speed, 0.0f, 0.0f);
                this.transform.position += new Vector3(-speed, 0.0f, 0.0f);
            }
            // 右に移動
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //this.transform.Translate(speed, 0.0f, 0.0f);
                this.transform.position += new Vector3(speed, 0.0f, 0.0f);
            }
            // 前に移動
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //this.transform.Translate(0.0f, 0.0f, speed);
                this.transform.position += new Vector3(0.0f, 0.0f, speed);
            }
            // 後ろに移動
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //this.transform.Translate(0.0f, 0.0f, -speed);
                this.transform.position += new Vector3(0.0f, 0.0f, -speed);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Gather();
            }
        }

        this.transform.position = (new Vector3(Mathf.Clamp(this.transform.position.x, -2.5f, 2.5f), this.transform.position.y,
                                               Mathf.Clamp(this.transform.position.z, -2.5f, 2.5f)));
    }

    //近くにいるものを計算・集合をかける
    void Gather(){
        Manager.AmountOfFriends = 0;
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine("Wait");

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
    }

    private IEnumerator Wait(){
        yield return new WaitForSeconds(0.5f); //1秒待つ
        targets = null;
        targets = GameObject.FindGameObjectsWithTag("Friend");
        for (int i = 0; i < targets.Length; i++)
        {
            var script = targets[i].GetComponent<FriendsAction>();
            script.Gather();
        }
        //Debug.Log(Manager.AmountOfFriends);
        if (Manager.AmountOfFriends > 6)
        {
            var manager = GameObject.Find("GameManager").GetComponent<Manager>();
            manager.Fall();
        }
    }
}

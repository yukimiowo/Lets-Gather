using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    //床のオブジェクト
    public GameObject Floor;
    public GameObject Friend;
    public static int AmountOfFriends;
    private GameObject[] upfloors;

    // Use this for initialization
    void Start()
    {
        Initiate();
        upfloors = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //fall();
            //Debug.Log("A");
        }
    }

    void CreateFloor()
    {
        GameObject NextFloor = Instantiate(Floor, new Vector3 (0, -2, 0), new Quaternion (0, 0, 0, 0));
        StartCoroutine("Up", NextFloor);

    }

    void CreateFriends(){
        Vector3 MyPosition = GameObject.Find("Me").transform.position;
        for (int i = 0; i < 17; i++)
        {
            // Instantiateの引数にPrefabを渡すことでインスタンスを生成する
            GameObject presentFriends = Instantiate(Friend) as GameObject;
            // ランダムな場所に配置する
            presentFriends.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), -1.7f, Random.Range(-2.5f, 2.5f));
        }
        for (int i = 0; i < 3; i++){
            GameObject presentFriends = Instantiate(Friend) as GameObject;
            // ランダムな場所に配置する
            presentFriends.transform.position = new Vector3(Random.Range(MyPosition.x - 0.4f, MyPosition.x + 0.4f), 0.25f, Random.Range(MyPosition.z - 0.4f, MyPosition.z + 0.4f));
        }
    }

    public void Fall()
    {
        //落とす床を決定
        upfloors = null;
        GameObject Me = GameObject.Find("Me");
        Vector3 MyPosition = Me.transform.position;
        MyPosition.x += 2.5f;
        MyPosition.z += 2.5f;
        int TileNumber = 26 - (Mathf.CeilToInt(MyPosition.z - 1) * 5 + Mathf.CeilToInt(MyPosition.x));
        Debug.Log("Tile" + TileNumber);


        //それ以外の床をあげる
        upfloors = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < upfloors.Length; i++){
            if(i+1 != 30){
                var script = upfloors[i].GetComponent <FloorAction> ();
                script.Fall();  //呼び出せる時と呼び出せない時がある
            }
        }
        var targets = GameObject.FindGameObjectsWithTag("Friend");
        foreach (var target in targets)
        {
            Destroy(target);
        }

        //新しい床の作成と、前の床の消滅、次の床の移動
        CreateFloor();
        Manager.AmountOfFriends = 0;
        CreateFriends();
        UIManager.floorNumber++;
        GameObject.Find("GameManager").GetComponent<UIManager>().ShowYourFloor();
    }

    private void Initiate(){
        AmountOfFriends = 0;
        Instantiate(Floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        for (int i = 0; i < 20; i++)
        {
            // Instantiateの引数にPrefabを渡すことでインスタンスを生成する
            GameObject presentFriends = Instantiate(Friend) as GameObject;
            // ランダムな場所に配置する
            presentFriends.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 0.25f, Random.Range(-2.5f, 2.5f));
        }
    }

    private IEnumerator Up(GameObject floor){
        while(floor.transform.position.y < 0){
            floor.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static int floorNumber;
    public Text floor;
    private float time = 60;
    public Text timerimit;
    public GameObject gameOverText;
    public GameObject startText;

    // Use this for initialization
    void Start () {
        floorNumber = 0;
        timerimit.text = ((int)time).ToString();
        gameOverText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)){
            MyAction.gameover = false;
            startText.SetActive(false);
        }
        //1秒に1ずつ減らしていく
        if (!MyAction.gameover) time -= Time.deltaTime;
        //マイナスは表示しない
        if (time < 0){
            time = 0;
            gameOverText.SetActive(true);
            MyAction.gameover = true;
        }
        timerimit.text = ((int)time).ToString();
    }

    public void ShowYourFloor(){
        floor.text = "地下" +floorNumber + "階";
    }
}

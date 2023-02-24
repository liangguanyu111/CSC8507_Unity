using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnPlayerDie = new UnityEvent();

    public GameObject Player;


    //
    public float width;
    public float length;
    public float distance;
    public Texture2D crosshair;
    private GUIStyle lineStyle;
    private Texture tex;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        OnPlayerDie.AddListener(OnPlayerDied);
        lineStyle = new GUIStyle();
        lineStyle.normal.background = crosshair;
    }

    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width - distance) / 2 - length, (Screen.height - width) / 2, length, width), tex, lineStyle);
        GUI.Box(new Rect((Screen.width + distance) / 2, (Screen.height - width) / 2, length, width), tex, lineStyle);
        GUI.Box(new Rect((Screen.width - width) / 2, (Screen.height - distance) / 2 - length, width, length), tex, lineStyle);
        GUI.Box(new Rect((Screen.width - width) / 2, (Screen.height + distance) / 2, width, length), tex, lineStyle);
    }
    void OnPlayerDied()
    {
        Player.gameObject.GetComponent<CharacterController>().enabled = false;  //Creazy
        Player.transform.position = new Vector3(0, 3, -20);
        Player.gameObject.GetComponent<CharacterController>().enabled = true;
        Debug.Log(Player.transform.position);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}


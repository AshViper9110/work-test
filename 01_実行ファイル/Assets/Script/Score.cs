using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoretext;
    [SerializeField] Text resulttext;
    [SerializeField] Text timetext;
    [SerializeField] GameObject endText;
    [SerializeField] GameObject resultText;
    [SerializeField] GameObject retrybotton;
    [SerializeField] GameObject Targetprefab;
    [SerializeField] GameObject StartBotton;
    PlayerMove PlayerMove;
    int score = 0;
    int time = 10;
    int timecount = 0;
    public bool isStop = false;
    public bool isOpen = false;
    public bool isStart = false;
    public AudioClip Hit;
    AudioSource audioSource;
    // Start is called before the first frame update
    public void Start()
    {
        time = 10;
        timecount = 0;
        score = 0;
        scoretext.GetComponent<Text>().text = $"Score:{score}";
        timetext.GetComponent<Text>().text = $"[{time}]";
        PlayerMove= GameObject.Find("Player").GetComponent<PlayerMove>();
        audioSource = GetComponent<AudioSource>();
        Instantiate(Targetprefab, new Vector3(-13f, 1, 18.5f), Quaternion.identity);
        Instantiate(Targetprefab, new Vector3(13f, 1, 18.5f), Quaternion.identity);
        Instantiate(Targetprefab, new Vector3(5.5f, 1.5f, 25f), Quaternion.identity);
        Instantiate(Targetprefab, new Vector3(-6f, 1.5f, 27f), Quaternion.identity);
        endText.SetActive(false);
        resultText.SetActive(false);
        retrybotton.SetActive(false);
    }

    // Update is called once per frame
    public void CountUp()
    {
        audioSource.PlayOneShot(Hit);
        if (!isStop)
        {
            score += 100;
            if (score % 1000 == 0)
            {
                time++;
            }
        }
        Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(1, 5), Random.Range(5, 20));
        Instantiate(Targetprefab, pos, Quaternion.identity);
        scoretext.GetComponent<Text>().text = $"Score:{score}";
        resulttext.GetComponent<Text>().text = $"Your Score : {score}";
    }
    private void FixedUpdate()
    {
        if (!isOpen && isStart && !isStop)
        {
            timecount++;
            if (timecount % 60 == 0)
            {
                time--;
                timetext.GetComponent<Text>().text = $"[{time}]";
            }
        }
    }
    private void Update()
    {
        if (time == 0)
        {
            endText.SetActive(true);
            resultText.SetActive(true);
            retrybotton.SetActive(true);
            isStop = true;
        }
    }
    public void Set()
    {
        Start();
        StartBotton.SetActive(false);
        isStop = false;
        PlayerMove.GetComponent<PlayerMove>().Start();
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}

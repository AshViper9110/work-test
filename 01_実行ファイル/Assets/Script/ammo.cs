using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ammo : MonoBehaviour
{
    Score GameMane;
    private int power = 2500;
    private int life = 300;
    private void Start()
    {
        GameMane = GameObject.Find("GameMane").GetComponent<Score>();
    }
    public void Shot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir * power);
    }
    private void FixedUpdate()
    {
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "retry" && !GameMane.GetComponent<Score>().isOpen)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject i in obj)
            {
                Destroy(i);
            }
            GameMane.GetComponent<Score>().Set();
        }
        if (collision.gameObject.tag == "Start" && !GameMane.GetComponent<Score>().isOpen)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject i in obj)
            {
                Destroy(i);
            }
            GameMane.GetComponent<Score>().Set();
            GameMane.GetComponent<Score>().isStart = true;
        }
    }
}

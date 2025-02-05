using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    Score GameMane;
    private bool isHit = false;
    private void Start()
    {
        GameMane = GameObject.Find("GameMane").GetComponent<Score>();
    }
    void Update()
    {
        if (isHit == true)
        {
            Destroy(gameObject);
            GameMane.GetComponent<Score>().CountUp();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            isHit = true;
        }
    }

}

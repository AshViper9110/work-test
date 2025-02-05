using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Text bulletcounttext;
    [SerializeField] GameObject endText;
    [SerializeField] GameObject resultText;
    [SerializeField] GameObject retrybotton;
    public int shootCount = 0;
    public int Count;
    private void Start()
    {
        bulletcounttext.GetComponent<Text>().text = $"Bullet:{shootCount}";
        Count = 200;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shootCount > 0)
        {
            Vector3 pos = Input.mousePosition;//マウスをクリックした位置を取得
            // カメラからクリックした点を通るRayを作成
            Ray ray = Camera.main.ScreenPointToRay(pos);
            //Rayのベクトルを正規化して方向を取得  
            Vector3 dir = Camera.main.ScreenPointToRay(pos).direction.normalized;
            //弾を生成して、弾のインスタンスをbulletに代入する
            //引数１：弾のPrefab,引数２：弾の出現位置（＝カメラの位置）引数３：回転していない状態で生成
            GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            //生成したたまにアタッチしているBulletスクリプトから、Shotメソッドを呼び出してたまに力を加える
            bullet.GetComponent<ammo>().Shot(dir);
            //カメラからクリックして点を通るRayをデバックように可視化してみる
            Debug.DrawRay(ray.origin, ray.direction * 15.0f, Color.green, 5, false);
            shootCount--;
            bulletcounttext.GetComponent<Text>().text = $"Bullet:{shootCount}";
        }
        if (Count == 0)
        {
            endText.SetActive(true);
            resultText.SetActive(true);
            retrybotton.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (shootCount == 0)
        {
            Count--;
        }
    }
}

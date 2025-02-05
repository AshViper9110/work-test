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
            Vector3 pos = Input.mousePosition;//�}�E�X���N���b�N�����ʒu���擾
            // �J��������N���b�N�����_��ʂ�Ray���쐬
            Ray ray = Camera.main.ScreenPointToRay(pos);
            //Ray�̃x�N�g���𐳋K�����ĕ������擾  
            Vector3 dir = Camera.main.ScreenPointToRay(pos).direction.normalized;
            //�e�𐶐����āA�e�̃C���X�^���X��bullet�ɑ������
            //�����P�F�e��Prefab,�����Q�F�e�̏o���ʒu�i���J�����̈ʒu�j�����R�F��]���Ă��Ȃ���ԂŐ���
            GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            //�����������܂ɃA�^�b�`���Ă���Bullet�X�N���v�g����AShot���\�b�h���Ăяo���Ă��܂ɗ͂�������
            bullet.GetComponent<ammo>().Shot(dir);
            //�J��������N���b�N���ē_��ʂ�Ray���f�o�b�N�悤�ɉ������Ă݂�
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

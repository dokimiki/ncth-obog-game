using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RespornSystem : MonoBehaviour
{
    public Transform StartRange;//生成する範囲の始まり
    public Transform EndRange;//生成する範囲の終わり
    public GameObject Obj;//リスポーンさせるオブジェ
    public int Amount = 400;//リスポーンさせる数。
    private int MaxAmount = 0;//実行中にこれ以上増やせないか調べるための変数。随時更新される。
    private List<GameObject> ObjList = new List<GameObject>();
    public bool RunEnd = false;//処理が終わったか？
    private float EndTime = 0.7f;//処理が終わる時間。MaxAmountが更新されるとリセットされるタイマーがこれを超えると処理が終わる。
    public float Timer = 0.0f;//タイマー。
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Obj.SetActive(false);
        RunEnd = false;
        StartCoroutine(RespornMethodWhile());
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Timer > EndTime){
            RunEnd = true;
            return;
        }
        Timer += Time.deltaTime;
        if(Amount > ObjList.Count){
            RespornMethod();
        }
        if(MaxAmount < ObjList.Count){
            MaxAmount = ObjList.Count;
            Timer = 0.0f;
        }
        ObjList.RemoveAll(obj => obj == null);*/
    }

    void RespornMethod(){
        float PosX = Random.Range(StartRange.position.x,EndRange.position.x);
        float PosY = Random.Range(StartRange.position.y,EndRange.position.y);
        float PosZ = Random.Range(StartRange.position.z,EndRange.position.z);

        PosX = Mathf.Round(PosX);
        PosY = Mathf.Round(PosY);
        PosZ = Mathf.Round(PosZ);

        float randomAngle = 90f * Random.Range(0, 2);
        Obj.transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);

        GameObject Obj2 = Instantiate(Obj,new Vector3(PosX,PosY,PosZ),Obj.transform.rotation);
        ObjList.Add(Obj2);
        Obj2.SetActive(true);
    }

    IEnumerator RespornMethodWhile()
    {
        Timer = 0.0f;
        while(Timer <= EndTime){
            Timer += Time.deltaTime;
            if(Amount > ObjList.Count){
                RespornMethod();
            }
            if(MaxAmount < ObjList.Count){
                MaxAmount = ObjList.Count;
                Timer = 0.0f;
            }
            ObjList.RemoveAll(obj => obj == null);

            yield return null;
        }

        RunEnd = true;
    }
}

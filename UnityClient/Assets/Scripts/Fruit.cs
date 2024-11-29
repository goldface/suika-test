using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitArray vFruitArray;
    public int vFruitIndex = 0;
    public bool IsCheckEvent = false;
    private void OnCollisionEnter2D(Collision2D aOther)
    {
        var lFruit = aOther.transform.GetComponent<Fruit>();
        if (lFruit != null)
        {
            // 이벤트가 이미 실행한 이력이 있으면 실행하지않는다.
            if (lFruit.IsCheckEvent)
                return;
                
            // 마지막 과일이면 체크하지않는다.
            if (lFruit.vFruitIndex >= vFruitArray.GetLastFruitIndex())
                return;
                
            if (lFruit.vFruitIndex == vFruitIndex)
            {
                IsCheckEvent = true;
                ContactPoint2D lContactPoint = aOther.GetContact(0);
                Instantiate(vFruitArray.GetFruit(vFruitIndex + 1), lContactPoint.point, Quaternion.identity);
                
                Destroy(lFruit.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}

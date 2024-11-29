using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverLine : MonoBehaviour
{

    public float vLimitTime = 2f;

    private float mDeltaTime = 0f;

    private BoxCollider2D mCollider;

    public Action OnGameOverAction;

    private bool mGameOver = false;
    private bool mIsActiveGameOverCheck = false;

    private void Awake()
    {
        mGameOver = false;
        mIsActiveGameOverCheck = true;
        mCollider = GetComponent<BoxCollider2D>();
    }


    public void SetGameOverCheck(bool aIsActive)
    {
        mIsActiveGameOverCheck = aIsActive;
    }

    public bool GetGameOverCheck()
    {
        return mIsActiveGameOverCheck;
    }
    
    private void OnTriggerStay2D(Collider2D aOther)
    {
        if (mGameOver)
            return;

        if (mIsActiveGameOverCheck == false)
            return;
            
        Debug.Log($"{aOther.name}");
        Fruit lFruit = aOther.transform.GetComponent<Fruit>();

        if (lFruit != null)
        {
            // 과일이 닿아있는 동안 카운팅
            mDeltaTime += Time.deltaTime;
            if (mDeltaTime >= vLimitTime)
            {
                // 게임 오버
                mGameOver = true;
                if (OnGameOverAction != null)
                    OnGameOverAction();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D aOther)
    {
        mDeltaTime = 0f;
    }
}

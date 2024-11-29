using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public FruitArray vFruitArray;
    public GameObject vStartPoint;
    private Rigidbody2D mCurrentFruitRigidbody;
    private GameObject mCurrentFuruit;

    public GameOverLine vGameOverLine;

    private bool mGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        mGameOver = false;
        vGameOverLine.OnGameOverAction += _OnGameOver;
        _ReadyToNextRandomFruit();
    }

    // Update is called once per frame
    void Update()
    {
        if (mGameOver)
            return;
            
        if (Input.GetKey(KeyCode.A))
        {
            vStartPoint.transform.Translate(Vector3.left * 0.01f);
            if (vStartPoint.transform.position.x <= -1.7f)
            {
                var lPosition = vStartPoint.transform.position;
                lPosition = new Vector3(-1.7f, lPosition.y, lPosition.z);
                vStartPoint.transform.position = lPosition;
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            vStartPoint.transform.Translate(Vector3.right * 0.01f);
            
            if (vStartPoint.transform.position.x >= 1.7f)
            {
                var lPosition = vStartPoint.transform.position;
                lPosition = new Vector3(1.7f, lPosition.y, lPosition.z);
                vStartPoint.transform.position = lPosition;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (vGameOverLine.GetGameOverCheck() == false)
                return;
                
            vGameOverLine.SetGameOverCheck(false);
            mCurrentFuruit.transform.parent = null;
            mCurrentFruitRigidbody.simulated = true;
            Invoke("_FruitWaitingTime", 0.5f);
        }
    }

    private void _FruitWaitingTime()
    {
        vGameOverLine.SetGameOverCheck(true);
        _ReadyToNextRandomFruit();
    }

    private void _ReadyToNextRandomFruit()
    {
        GameObject lRandomFruitPrefab = vFruitArray.GetFruit(Random.Range(0, 5));
        mCurrentFuruit = Instantiate(lRandomFruitPrefab, vStartPoint.transform.position, Quaternion.identity);
        mCurrentFruitRigidbody = mCurrentFuruit.GetComponent<Rigidbody2D>();
        mCurrentFruitRigidbody.simulated = false;
        mCurrentFuruit.transform.parent = vStartPoint.transform;
    }

    private void _OnGameOver()
    {
        Debug.LogError("Game Over");
        mGameOver = true;
        Invoke("_OnGameReset", 2f);
    }

    private void _OnGameReset()
    {
        SceneManager.LoadScene("EmptyScene");
    }
}
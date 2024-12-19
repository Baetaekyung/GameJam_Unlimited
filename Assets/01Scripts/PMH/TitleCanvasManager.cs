using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvasManager : MonoBehaviour
{
    public static TitleCanvasManager Instance;
    public RectTransform RectTrm => transform as RectTransform;
    public bool IsTurnning = false;

    [SerializeField] private float smoothSpeed = 5f;

    private float targetRotation = 270f; // ��ǥ ����
    private float currentRotation = 0f; // ���� ���� (�ε巯�� ȸ�� ����)

    private int currentScene = 1;

    private float lastTurnTime = 0;
    private float turnningCooldown = 1f;

    [SerializeField] private float titleSceneXPos, challengeSceneXPos, timeattackSceneXPos, settingSceneXPos, tip1SceneXPos, tip2SceneXPos;
    private float currenPosX;

    [SerializeField] private Transform _uiControllerTrm;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("관리자 권한으로 모든 레벨 잠금해제");
            GameManager.Instance.SetCurrentSceneNumber(45);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("관리자 권한으로 모든 레벨 잠금");
            GameManager.Instance.SetCurrentSceneNumber(1);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        SlideView();
    }

    public void SlideScene(int v)
    {
        currentScene += v;

        if(currentScene > 6)
        {
            currentScene = 6;
        }

        if (currentScene <= 1)
        {
            currentScene = 1;
        }
        switch (currentScene)
        {
            case 1:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Title);
                currenPosX = titleSceneXPos;
                break;
            case 2:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Challenge);
                currenPosX = challengeSceneXPos;
                break;
            case 3:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Timeattck);
                currenPosX = timeattackSceneXPos;
                break;
            case 4:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Setting);
                currenPosX = settingSceneXPos;
                break;
            case 5:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Tip1);
                currenPosX = tip1SceneXPos;
                break;
            case 6:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Tip2);
                currenPosX = tip2SceneXPos;
                break;
        }

        SoundManager.Instance.SetVolumes();
    }
    private void SlideView()
    {
        // �ε巴�� ȸ��
        RectTrm.anchoredPosition = new Vector2(-currenPosX, 0);
    }
    private bool CheckIsTurnningEnd()
    {
        //float offset = Mathf.Abs(targetRotation - RectTrm.eulerAngles.z);
        //if (offset > 350) return true;
        //else return offset < 5f;

        return lastTurnTime + turnningCooldown < Time.time;
    }
    private void RotateView()
    {
        // �ε巴�� ȸ��
        currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * smoothSpeed);
        RectTrm.localRotation = Quaternion.Euler(0, 0, currentRotation);
    }
}

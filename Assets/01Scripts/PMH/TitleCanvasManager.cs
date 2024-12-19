using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float titleSceneXPos, selectLevelSceneXPos, settingSceneXPos, shopSceneXPos;
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
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     //if (CheckIsTurnningEnd())
        //     //{
        //     //    lastTurnTime = Time.time;
        //     //    SetChangeView(90);
        //     //}
        //     SlideScene(1);
        // }
        // else if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     //if (CheckIsTurnningEnd())
        //     //{
        //     //    lastTurnTime = Time.time;
        //     //    SetChangeView(-90);
        //     //}
        //     SlideScene(-1);
        // }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SestSettingView();
        }

        //RotateView();
        SlideView();
    }
    public void SlideScene(int v)
    {
        currentScene += v;

        if(currentScene > 4)
        {
            currentScene = 4;
        }

        if (currentScene <= 1)
        {
            currentScene = 1;
        }
        switch (currentScene)
        {
            case 1:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.InGame); //90
                currenPosX = titleSceneXPos;
                break;
            case 2:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.SelectLevel); //180
                currenPosX = selectLevelSceneXPos;
                break;
            case 3:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Setting); //270
                currenPosX = settingSceneXPos;
                break;
            case 4:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Store); //0
                currenPosX = shopSceneXPos;
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

    private void SetChangeView(float offset)
    {
        // ��ǥ ȸ���� 90���� ����
        targetRotation += offset;

        //���� -> ���� -> �޴� �� ���� -> �ܰ輱��
        currentScene++;

        switch(currentScene)
        {
            case 1:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Setting); //90
                break;
            case 2:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.Store); //180
                break;
            case 3:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.InGame); //270
                break;
            case 4:
                SceneStatus.Instance.ChangeSceneState(SceneStateType.SelectLevel); //0
                break;
        }

        // ������ �׻� 0~360 ���̷� ����
        if (targetRotation >= 360f)
        {
            currentScene = 0;
            targetRotation -= 360f;
        }
    }

    public void SetMainView()
    {
        targetRotation = 270f;
        SceneStatus.Instance.ChangeSceneState(SceneStateType.InGame);
    }

    public void SestSettingView()
    {
        targetRotation = 90f;
        SceneStatus.Instance.ChangeSceneState(SceneStateType.Setting);
    }
}

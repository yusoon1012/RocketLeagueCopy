using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;   // instance ����

    public int[] score = new int[2];   // score[0] : 1��, score[1] : 2�� ���ھ�
    public bool isGoal = false;   // Goal �� ���� �������� �ƴ��� üũ

    void Awake()
    {
        if (instance == null || instance == default) { instance = this; }   // TestManager instance �ߺ��� �˻�
        else { Destroy(this.gameObject); }
    }

    public void GoalTeam1()   // 1���� 2���� ��뿡 ���� �־����� ����
    {
        score[0] += 1;   // 1���� score �� 1 ���� �����ش�
    }

    public void GoalTeam2()   // 2���� 1���� ��뿡 ���� �־����� ����
    {
        score[1] += 1;   // 2���� score �� 1 ���� �����ش�
    }

}

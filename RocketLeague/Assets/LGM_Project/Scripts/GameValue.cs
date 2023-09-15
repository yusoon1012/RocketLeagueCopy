using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValue : MonoBehaviour
{
    public int playerTeamCheck = default;   // �÷��̾ � ���� �����ߴ��� üũ
    public int gameMaxPlayer = default;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   // �ٸ� ���� �ε� �Ǿ �� ��ũ��Ʈ�� �ʱ�ȭ ���� �ʵ��� ���ش�

        playerTeamCheck = 0;
        gameMaxPlayer = 0;
    }
}

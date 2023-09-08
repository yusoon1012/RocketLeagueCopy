using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGlowColorForDOTween_Choi : MonoBehaviour
{
    // ������ �ð����� TMP_Text�� Glow ������ �����ϴ� �Լ�
    public static void DOColor(Material material, int id, Color targetColor, float t)
    {
        DOTween.To(() => material.GetColor(id),
            color => material.SetColor(id, color),
            targetColor, t);
    }
}

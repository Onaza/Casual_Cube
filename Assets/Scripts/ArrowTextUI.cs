using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowTextUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI swipeText;
    private TextMeshProUGUI textMesh;
    private MovementUI movement;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        movement = GetComponent<MovementUI>();
    }

    public void Stop()
    {
        movement.Stop();
    }

    public void MoveUpToDown(Vector3 start, Vector3 end)
    {
        swipeText.rectTransform.anchoredPosition = new Vector2(120, 0);
        textMesh.text = "¡é";
        movement.Play(start, end, true, false, 1.0f);
    }

    public void MoveLeftToRight(Vector3 start, Vector3 end)
    {
        swipeText.rectTransform.anchoredPosition = new Vector2(15, -70);
        textMesh.text = "¡æ";
        movement.Play(start, end, true, false, 1.0f);
    }
}

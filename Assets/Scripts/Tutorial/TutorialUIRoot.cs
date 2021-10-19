using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUIRoot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI firstLineText;
    [SerializeField]
    private TextMeshProUGUI secondLineText;

    [SerializeField]
    private ArrowTextUI arrow;

    public void ShowUpDownArrow(Vector3 start, Vector3 end)
    {
        arrow.gameObject.SetActive(true);
        arrow.Stop();
        arrow.MoveUpToDown(start, end);
    }

    public void ShowLeftToRightArrow(Vector3 start, Vector3 end)
    {
        arrow.gameObject.SetActive(true);
        arrow.Stop();
        arrow.MoveLeftToRight(start, end);
    }

    public void HideArrow()
    {
        arrow.Stop();
        arrow.gameObject.SetActive(false);
    }

    public void ShowExplaination(string firstLine, string secondLine)
    {
        firstLineText.gameObject.SetActive(true);
        secondLineText.gameObject.SetActive(true);

        firstLineText.text = firstLine;
        secondLineText.text = secondLine;
    }
}

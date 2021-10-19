using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialTile : MonoBehaviour
{
    public bool canMove { get; private set; }
    public int type { get; private set; }

    [SerializeField]
    private TextMeshPro limitTimeHUD;
    [SerializeField]
    private Sprite colorType1;
    [SerializeField]
    private Sprite colorType2;
    [SerializeField]
    private Sprite spriteDisable;

    private SpriteRenderer spriteRenderer;
    private Sprite spriteNormal;
    private Twinkle2D twinkle;

    private bool releaseStop;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteNormal = spriteRenderer.sprite;
        canMove = true;
        twinkle = GetComponent<Twinkle2D>();
    }

    public void Release()
    {
        canMove = true;
        spriteRenderer.sprite = spriteNormal;
        limitTimeHUD.gameObject.SetActive(false);
        twinkle.StopAndRestoration();
    }

    public void SetColorTileNoTimer()
    {
        spriteRenderer.sprite = colorType1;
        limitTimeHUD.gameObject.SetActive(false);
        twinkle.Play(0.5f);
    }

    public void SetColorTile()
    {
        spriteRenderer.sprite = colorType2;
        limitTimeHUD.gameObject.SetActive(true);
        twinkle.Play(0.5f);

        StartCoroutine(TimeLimitLoop());
    }

    IEnumerator TimeLimitLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        int period = 5;
        limitTimeHUD.alpha = 1.0f;

        while (period >= 0)
        {
            limitTimeHUD.text = string.Format("{0}", period);
            yield return wait;
            if (releaseStop == true)
                period--;
        }
        twinkle.StopAndRestoration();
        spriteRenderer.sprite = spriteNormal;
        limitTimeHUD.gameObject.SetActive(false);
    }

    public void SetImpassableTile()
    {
        canMove = false;
        spriteRenderer.sprite = spriteDisable;
        limitTimeHUD.text = string.Format("{0}", 3);
        limitTimeHUD.gameObject.SetActive(true);

        StartCoroutine(ImpassableTileLoop());
    }

    IEnumerator ImpassableTileLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        int period = 3;

        limitTimeHUD.alpha = 0.3f;

        releaseStop = false;

        while (period >= 0)
        {
            limitTimeHUD.text = string.Format("{0}", period);
            yield return wait;
            if(releaseStop == true)
                period--;
        }
        spriteRenderer.sprite = spriteNormal;
        limitTimeHUD.gameObject.SetActive(false);
        canMove = true;
    }

    public void ReleaseStop()
    {
        releaseStop = true;
    }
}
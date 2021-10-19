using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class Tile : MonoBehaviour
{
    private readonly int ImpassableTilePeriod = 10;

    public Action MissedTile;
    public bool isSelected { get; private set; }
    public bool canMove { get; private set; }
    public int type { get; private set; }

    [SerializeField]
    private TextMeshPro limitTimeHUD;
    [SerializeField]
    private Sprite spriteDisable;

    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;
    private Sprite spriteNormal;
    
    private Twinkle2D twinkle;
    private Coroutine coroutine;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteNormal = spriteRenderer.sprite;
        twinkle = GetComponent<Twinkle2D>();
        coroutine = null;
        canMove = true;
    }

    public void SetColorTile(int type, int timeLimit, Sprite sprite)
    {
        isSelected = true;
        this.type = type;
        spriteRenderer.sprite = sprite;
        boxCollider.enabled = true;
        limitTimeHUD.gameObject.SetActive(true);
        twinkle.Play(0.5f);

        coroutine = StartCoroutine(TimeLimitLoop(timeLimit));
    }

    public void Release()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        isSelected = false;
        canMove = true;
        spriteRenderer.sprite = spriteNormal;
        boxCollider.enabled = false;
        limitTimeHUD.gameObject.SetActive(false);
        twinkle.StopAndRestoration();
    }

    IEnumerator TimeLimitLoop(int timeLimit)
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);

        limitTimeHUD.alpha = 1.0f;

        while (timeLimit >= 0)
        {
            limitTimeHUD.text = string.Format("{0}", timeLimit);
            yield return wait;
            timeLimit--;
        }
        Release();
        MissedTile?.Invoke();
    }

    public void SetImpassableTile()
    {
        canMove = false;
        spriteRenderer.sprite = spriteDisable;
        limitTimeHUD.text = string.Format("{0}", ImpassableTilePeriod);
        limitTimeHUD.gameObject.SetActive(true);

        coroutine = StartCoroutine(ImpassableTileLoop());
    }


    IEnumerator ImpassableTileLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        int period = ImpassableTilePeriod + UnityEngine.Random.Range(0, 11);

        limitTimeHUD.alpha = 0.3f;

        while (period >= 0)
        {
            limitTimeHUD.text = string.Format("{0}", period);
            yield return wait;
            period--;
        }
        spriteRenderer.sprite = spriteNormal;
        limitTimeHUD.gameObject.SetActive(false);
        canMove = true;
    }
}
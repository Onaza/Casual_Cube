using System;
using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private readonly Quaternion UpQuaternion = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
    private readonly Quaternion DownQuaternion = Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f));
    private readonly Quaternion RightQuaternion = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
    private readonly Quaternion LeftQuaternion = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));

    [SerializeField]
    private Transform Cube;
    [SerializeField]
    private Transform UpPivot;
    [SerializeField]
    private Transform DownPivot;
    [SerializeField]
    private Transform RightPivot;
    [SerializeField]
    private Transform LeftPivot;

    private Transform currentPivot;
    private Quaternion toQuternion;
    private bool canRotate;


    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentPivot = null;
        toQuternion = Quaternion.identity;
        canRotate = true;
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }

    public void Rotate(Direction direction)
    {
        if (!canRotate)
            return;

        switch (direction)
        {
            case Direction.UP:
                currentPivot = UpPivot;
                toQuternion = UpQuaternion;
                break;
            case Direction.DOWN:
                currentPivot = DownPivot;
                toQuternion = DownQuaternion;
                break;
            case Direction.RIGHT:
                currentPivot = RightPivot;
                toQuternion = RightQuaternion;
                break;
            case Direction.LEFT:
                currentPivot = LeftPivot;
                toQuternion = LeftQuaternion;
                break;
            default:
                break;
        }
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate()
    {
        canRotate = false;
        Cube.SetParent(currentPivot);
        float amount = 0.0f;
        audioSource.Play();
        while (true)
        {
            amount += (Time.deltaTime * 10.0f);
            currentPivot.rotation = Quaternion.Slerp(Quaternion.identity, toQuternion, amount);
            if (amount >= 1)
                break;
            yield return null;
        }
        yield return new WaitForSeconds(0.02f);
        Cube.SetParent(transform.parent);
        currentPivot.rotation = Quaternion.identity;
        transform.position = new Vector3(Cube.position.x, Cube.position.y, 0.0f);
        canRotate = true;
    }
}
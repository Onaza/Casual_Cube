using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particles;

    private static EffectManager instance;

    public static EffectManager Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = FindObjectOfType<EffectManager>();
                if (instance == null)
                    return instance = new EffectManager();
            }

            return instance;
        }
    }

    public void SetAndPlay(Transform transform, int index)
    {
        if (particles[index].gameObject.activeSelf == true)
            particles[index].gameObject.SetActive(false);

        particles[index].transform.position = transform.position;
        particles[index].gameObject.SetActive(true);
    }

    public void SetAndPlay(int row, int col, int index)
    {
        if (particles[index].gameObject.activeSelf == true)
            particles[index].gameObject.SetActive(false);

        particles[index].transform.position = new Vector3(row, col, 0.0f);
        particles[index].gameObject.SetActive(true);
    }
}
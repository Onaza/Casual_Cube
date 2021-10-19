using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField] private int bestRecord;
    [SerializeField] private int chance;

    public SaveData(int chance, int bestRecord)
    {
        this.chance = chance;
        this.bestRecord = bestRecord;
    }
}

using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // 16byte Private Key
    private readonly string privateKey = "dhks1tlr2gp2fla7";

    public void SaveToFile<T>(T data)
    {
        string dataString = JsonUtility.ToJson(data);
        string encryptString = Encrypt(dataString);
        File.WriteAllText(GetPath(), encryptString);
    }

    public T LoadFromFile<T>()
    {
        string dataString = File.ReadAllText(GetPath());
        string decryptString = Decrypt(dataString);
        T result = JsonUtility.FromJson<T>(decryptString);

        return result;
    }

    public void SaveToPlayerprefs(string key, int value)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = Encrypt(value.ToString());
        PlayerPrefs.SetString(encryptKey, encryptValue);
    }

    public void SaveToPlayerprefs(string key, float value)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = Encrypt(value.ToString());
        PlayerPrefs.SetString(encryptKey, encryptValue);
    }

    public void SaveToPlayerprefs(string key, string value)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = Encrypt(value);
        PlayerPrefs.SetString(encryptKey, encryptValue);
    }

    public int LoadFromPlayerprefGetInt(string key)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = PlayerPrefs.GetString(encryptKey, "0");
        if (encryptValue.Equals("0"))
            return 0;
        int.TryParse(Decrypt(encryptValue), out int decryptValue);
        return decryptValue;
    }

    public float LoadFromPlayerprefGetFloat(string key)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = PlayerPrefs.GetString(encryptKey, "0");
        if (encryptValue.Equals("0"))
            return 0.0f;
        float.TryParse(Decrypt(encryptValue), out float decryptValue);
        return decryptValue;
    }

    public string LoadFromPlayerprefGetString(string key)
    {
        string encryptKey = Encrypt(key);
        string encryptValue = PlayerPrefs.GetString(encryptKey, "0");
        if (encryptValue.Equals("0"))
            return "0";

        return Decrypt(encryptValue);
    }

    private string Encrypt(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] result = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(result, 0, result.Length);
    }

    private string Decrypt(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] result = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(result);
    }

    private RijndaelManaged CreateRijndaelManaged()
    {
        RijndaelManaged result = new RijndaelManaged();

        byte[] pKey = System.Text.Encoding.UTF8.GetBytes(privateKey);
        byte[] key = new byte[16];
        System.Array.Copy(pKey, 0, key, 0, 16);

        result.Key = key;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;

        return result;
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData.ws");
    }
}

using System.Collections;
using UnityEngine;
using TMPro;
public class UI3D : MonoBehaviour
{
    public TMP_Text TextScoreAndShields;
    public TMP_Text TextAurekBash;

    private string characters = "a b d e f g H J K L O Y I 2 3 4 5 6 7 8 9 0";

    private void Awake()
    {
        StartCoroutine(FillAurekBashTextField());        
    }

    private IEnumerator FillAurekBashTextField()
    {
        while (true)
        {
            TextAurekBash.text = GenerateRndText(30);
            yield return new WaitForSeconds(1f);
        }
    }

    private string GenerateRndText(int length)
    {
        string rndText = "";
        for (int i = 0; i < length; i++)        
            rndText += characters[Random.Range(0, characters.Length -1)];
        
        return rndText;
    }
}

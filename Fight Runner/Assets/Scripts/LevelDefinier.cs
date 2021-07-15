using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LevelDefinier : MonoBehaviour
{
    private string _textToShow = "SLIDE \nTO START";
    private void Awake()
    {
        TextMeshProUGUI tMPText = GetComponent<TextMeshProUGUI>();
        tMPText.text = _textToShow + "\n\n\n" + SceneManager.GetActiveScene().name;
    }
}

using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class RevardCounter : MonoBehaviour
{
    private bool _isLevelComplete = false;

    [SerializeField]
    private TextMeshProUGUI _coinText;

    [SerializeField]
    private GameObject _endLevelTextObject;

    private string _enemyLevelMultiplierName = "EnemyLevelMultiplier";
    private string _winText = "\n\nYou won ";
    private string _secondWinText = "TAP TO START \nNEXT LEVEL";
    private int _revardEmount;
    private bool _isRevardTaken = false;

    private void Update()
    {
        if (_isLevelComplete && Input.touchCount > 0)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index + 1 == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
                PlayerPrefs.SetInt(_enemyLevelMultiplierName, PlayerPrefs.GetInt(_enemyLevelMultiplierName) + 3);
            }
            else
            {
                SceneManager.LoadScene(index += 1);
            }
        }
    }

    public void GetRevard(int index)
    {
        if (!_isRevardTaken)
        {
            CoinCounter coinCounter = GameObject.Find("Interface/Coins").GetComponent<TextMeshProUGUI>().GetComponent<CoinCounter>();
            _revardEmount = index;
            coinCounter.GetRevard(_revardEmount);
            _winText = SceneManager.GetActiveScene().name + " finished " + _winText + _revardEmount.ToString() + " coins !"
                + "\n\n\n" + _secondWinText;
            _isRevardTaken = true;
            StartCoroutine(EndLevel());
        }
    }


    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1.5f);
        _endLevelTextObject = Instantiate(_endLevelTextObject, new Vector3(540, 950), _endLevelTextObject.transform.rotation, GameObject.Find("Interface").transform);
        _endLevelTextObject.GetComponentInChildren<TextMeshProUGUI>().text = _winText;
        _isLevelComplete = true;
    }
}

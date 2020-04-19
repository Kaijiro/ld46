using UnityEngine;

public class TestQTEController : MonoBehaviour
{
    public GameObject level1Helps;
    public GameObject level2Helps;
    public GameObject level3Helps;
    
    private int _playerLevel = 1;

    private void Start()
    {
        Debug.Log("Let's go !");
        UpdateActionListPanel();
    }

    private void UpdateActionListPanel()
    {
        switch (_playerLevel)
        {
            case 1: level1Helps.SetActive(true); break;
            case 2: level2Helps.SetActive(true); break;
            case 3: level3Helps.SetActive(true); break;
        }
    }
}
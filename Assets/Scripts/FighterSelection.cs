using UnityEngine;

public class FighterSelection : MonoBehaviour
{
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject buttonPrefab;

    private void OnEnable()
    {
        var fighterList = PlayerManager.Instance.FighterList;

        foreach (var fighter in fighterList)
        {
            var fighterButton = Instantiate(buttonPrefab, buttonParent);
            fighterButton.GetComponent<FighterButton>().ButtonSetup(fighter);
        }
    }
    
    public void HideWindow()
    {
        foreach (Transform transf in buttonParent)
        {
            Destroy(transf.gameObject);
        }
        
        gameObject.SetActive(false);
    }

    
}

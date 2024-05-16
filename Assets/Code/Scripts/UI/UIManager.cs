using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button BtnPause, BtnResume;
    public Canvas PauseUI;
    public Canvas HUD;
    public Canvas Lose;
    public Canvas Win;
    public Canvas Upgrade;
    public Canvas Tutorial1;
    public Canvas Tutorial2;

    public List<Sprite> UpgradeCard;
    public GameObject PrefabCard;

    // Start is called before the first frame update
    void Start()
    {
        HUDCanvas();
        BtnPause.onClick.AddListener(PauseCanvas);
        BtnResume.onClick.AddListener(HUDCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PauseCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
        PauseUI.enabled = true;
    }

    private void HUDCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
    }

    private void LoseCanvas()
    {
        HideAllCanvases();
        Lose.enabled = true;
    }

    private void WinCanvas()
    {
        HideAllCanvases();
        Win.enabled = true;
    }

    private void UpgradeCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
        Upgrade.enabled = true;
        UpgradeManager.Instance.SetUpgradeOptions(3); //akses upgarde manager
        List<GameObject> upgradeCard;
        for(int i=0; i<UpgradeManager.Instance.UpgradesToChoose.Count; i++)
        {
            GameObject card = Instantiate(PrefabCard);
            card.GetComponent<UpgradeCardController>().dataKartu = UpgradeManager.Instance.UpgradesToChoose[i];
            card.GetComponent<SpriteRenderer>().sprite = UpgradeCard[UpgradeManager.Instance.UpgradesToChoose[i].id-1];
        }
    }

    private void Tutorial1Canvas()
    {
        HideAllCanvases();
        Tutorial1.enabled = true;
    }

    private void Tutorial2Canvas()
    {
        HideAllCanvases();
        Tutorial2.enabled = true;
    }

    void HideAllCanvases()
    {
        PauseUI.enabled = false;
        HUD.enabled = false;
        Lose.enabled = false;
        Win.enabled = false;
        Upgrade.enabled = false;
        Tutorial1.enabled = false;
        Tutorial2.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI player1Name;
    [SerializeField]
    private TextMeshProUGUI player2Name;
    [SerializeField]
    private Transform player1Parent;
    [SerializeField]
    private Transform player2Parent;
    [SerializeField]
    private GameObject gameWinPanel;
    [SerializeField]
    private TextMeshProUGUI playerWinText;

    private void Start()
    {
        gameWinPanel.SetActive(false);
    }
    public void InitializeUI(PlayerModel player)
    {
        if (player.playerCount == 1)
        {
            player1Name.text = player.playerName;
        }

        if (player.playerCount == 2)
        {
            player2Name.text = player.playerName;
        }
    
        
    }


    public void UpdateCurrentPlayer(PlayerModel player)
    {
        ResetCurrentPlayer();

        if (player.playerCount == 1)
        {
            player1Name.transform.parent.GetComponent<Image>().color=Color.green;
        }
        if (player.playerCount == 2)
        {
            player2Name.transform.parent.GetComponent<Image>().color = Color.green;
        }

    }

    void ResetCurrentPlayer()
    {
        player1Name.transform.parent.GetComponent<Image>().color = Color.white;
        player2Name.transform.parent.GetComponent<Image>().color = Color.white;
    }

    public void GameWin(PlayerModel player)
    {
        gameWinPanel.SetActive(true);
        gameWinPanel.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1f), 1f);
        playerWinText.text = player.playerName + " wins!!";
    }

    public void LoadSceneAt(int ind)
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(ind).name);
    }

}

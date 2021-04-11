using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int totalPlayers;
    [SerializeField]
    private Transform player1Parent;
    [SerializeField]
    private Transform player2Parent;
    [SerializeField]
    private Transform playedDeckParent;
    [SerializeField]
    private UIView uIManager;
    [SerializeField]
    private PlayerModel playedDeck;

    [SerializeField]
    private float turnGap = 3f;

    List<PlayerModel> players = new List<PlayerModel>();

    GameObject playedCard;
    public static PlayerModel currentPlayer;

    int currentPlayerCount = 1;
    int prevCardValidChances = 0;
    float cardZ = 0f;

    bool counteringSpecialCard = false;
    bool cardPlaying = false;
    bool gameEnded = false;
    //called from gameListener
    public void InitializePlayer(PlayerModel player)
    {
        players.Add(player);
        Debug.Log("Added " + player.playerName);

        for (int i = 0; i < CardPool.instance.poolCount / totalPlayers; i++)
        {
            if (player.playerCount == 1)
                player.Enqueue_Front(CardPool.instance.SpawnFromPool(player1Parent));

            if (player.playerCount == 2)
            {
                player.Enqueue_Front(CardPool.instance.SpawnFromPool(player2Parent));

            }
        }
        Debug.Log(player.dequeueCount);


    }

    public void InitializeGameValues()
    {
        currentPlayer = players.ElementAt(0);
        prevCardValidChances = 0;

        //since we want computer to play first
        currentPlayerCount = 1;
        Invoke(nameof(NextPlayer), turnGap);

    }

    public void PlayTurn(PlayerModel player)
    {
        if (currentPlayer != player)
            return;

        if (cardPlaying || gameEnded)
            return;

        cardPlaying = true;
       
        playedCard = player.Deque_Front();

        CheckForWin(player);

        playedCard.transform.position = new Vector3(playedCard.transform.position.x, playedCard.transform.position.y, cardZ);
        cardZ -= 0.1f;
        

        playedCard.GetComponent<CardModel>().PlayCard(delegate{
           
           CheckForCounteringSpecialCard(player);
            
        });

        playedCard.transform.parent = playedDeckParent;
       playedDeck.Enqueue_Front(playedCard);

       
    }

    void CheckForCounteringSpecialCard(PlayerModel player)
    {
       
        if (prevCardValidChances > 0)
        {
            counteringSpecialCard=true;
            CheckCounterCard(player);
            return;
        }

        else
        {
            Debug.Log("calling next player");

            Invoke(nameof(NextPlayer), turnGap);

        }

    }

    void CheckForWin(PlayerModel player)
    {
        
        if (playedCard == null)
        {
            gameEnded = true;
            if (!counteringSpecialCard)
            //you win the game
            {
                WinGame(player);
               
            }
            else
            {
                //opponent wins the game
                WinGame(players.ElementAt((currentPlayerCount + 1) / players.Count));
            }
            
        }
        gameEnded = false;
       
    }

   
    void CheckCounterCard(PlayerModel player)
    {
       
        prevCardValidChances--;
        Debug.Log("Valid chances left " + prevCardValidChances);

        //opponent was successfully able to counter player
        //if he has thrown some special card
        if (playedCard.GetComponent<CardModel>().Chances>0)
        {
            counteringSpecialCard = false;
            Debug.Log("calling next player ");
            Invoke(nameof(NextPlayer), turnGap);
            return;
        }
        //if opponent couldn't counter in given chances
        else if (prevCardValidChances <= 0 && counteringSpecialCard)
        {
            OnFaildetoCounter(player);
            return;
        }

        Invoke(nameof(CheckForComputerPlayer), turnGap);
    }

    void OnFaildetoCounter(PlayerModel player)
    {
        Debug.Log("pushing played deck");
        GameObject rootCard;
        Transform parent;

        if (player.playerCount == 1)
            parent = player1Parent;
        else
        {
           parent = player2Parent;

        }

        while (playedDeck.dequeueCount>1)
        {
        Debug.Log("played deck count " + playedDeck.dequeueCount);

            rootCard = playedDeck.Deque_last();
            rootCard.transform.parent = parent;
            player.Enque_Back(rootCard);
            rootCard.GetComponent<CardModel>().StackCard();
        }

        rootCard=playedDeck.Deque_last();
        player.Enque_Back(rootCard);
       
        rootCard.transform.parent = parent;
        rootCard.GetComponent<CardModel>().StackCard(delegate { 
            Invoke(nameof(CheckForComputerPlayer), turnGap);
            cardPlaying = false;
        });
        cardZ = 0;
            
    }

    void WinGame(PlayerModel player)
    {
        Debug.Log("Yippeee won the  game");
        uIManager.GameWin(player);

    }

    void NextPlayer()
    {
       
        if (playedCard!=null)
            prevCardValidChances = playedCard.GetComponent<CardModel>().Chances;

        counteringSpecialCard = false;
       
        currentPlayerCount = (currentPlayerCount + 1);
        if (currentPlayerCount > 2)
            currentPlayerCount = 1;

        currentPlayer = players.ElementAt(currentPlayerCount - 1);
        uIManager.UpdateCurrentPlayer(currentPlayer);
        CheckForComputerPlayer();
    }

   
    void CheckForComputerPlayer()
    {
        cardPlaying = false;
        Debug.Log(" current player name from check computer player " + currentPlayer.playerName);
       
        if (currentPlayer.playerName == "Computer")
        {
            
           PlayTurn(currentPlayer);
        }
    }
}

# Beggar-Your-Neighbour
Beggar Your Neighbor is an easy-to-learn card game that can be enjoyed by two or more players.  Eliminate your opponent’s cards to win this game!
You can find the original video here 
<div align="center">
      <a href="https://youtu.be/WAq0-5lTVMk">
     <img 
      src="https://youtu.be/WAq0-5lTVMk/0.jpg" 
          alt = "working"
      style="width:100%;">
      </a>
    </div>

<p>
<img src="https://github.com/khushboogupta17/Beggar-Your-Neighbour/blob/main/Builds/SS_1.png" width="20%" height="20%">
<img src="https://github.com/khushboogupta17/Beggar-Your-Neighbour/blob/main/Builds/SS_2.png" width="20%" height="20%">
</p>

# Test
Find the apk [here](https://github.com/khushboogupta17/Beggar-Your-Neighbour/blob/main/Builds/BYN.apk).
It works on any android device.

# Technologies Used
Unity 2019.4, Visual Studio, DoTween

# My Take
Beggar your neighbour project has a mixture of Scriptable objects and monobehaviour. Game starts with instantiation of 52 cards in CardPool script and each card has a script CardModel associated with it to define the card it is and the chances allowed for that card. Then these cards are divided into number of players here 2 via GameController and the non-dealer i.e computer starts to play here.It’s a turn based game so user can interact on his turn only via clicking on screen or tapping it once. Each player i.e computer and “you” has their own scriptable object called PlayerModel which contains a deque data structure primarily and keeps track of all the cards added and removed.The played cards/deck is another playerModel here which keeps track of all the cards played. So whenever a player couldn’t counter the card the played deck is pushed into that player’s deque via GameController and game continues. To avoid coupling in scripts I tried to use scriptable objects called gameEvent and GameEventListener which takes care of notifying other scripts of any events in the scene. In the end to show whose chance is this we have UIView which takes info from GameController and populates the screen.It also sets the screen in the beginning by getting the name of each player from their playerModel. I tried to  implement MVC structure and their naming convention as much I could grasp in the weekend.The cardModel and PlayerModel holds all the data and UIView is responsible for putting it on screen and GameController is handling the main logic. One better way of implemeneting this could be using assest bundles to fetch card sprites instead of linking them manually and using state machine design pattern to shift between various states.

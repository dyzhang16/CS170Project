using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkip : MonoBehaviour
{
    public GameManager manager;

    public void skipStreet(){
        manager.previousScene = "Tutorial";

        manager.flowerPuzzle = 1;
    }

    public void skipCoffee(){
        manager.previousScene = "StreetIntro";

        manager.flowerPuzzle = 1;
    }

    public void skipOffice(){
        manager.previousScene = "CityOffice";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.visitedCoffee = 1;
        manager.readRecipeBook = 1;
        manager.addedCoffeeMachineItem = 1;
    }

    public void skipApartment(){
        manager.previousScene = "CityArcade";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.visitedCoffee = 1;
        manager.readRecipeBook = 1;
        manager.addedCoffeeMachineItem = 1;
        manager.followFriendinOffice = 1;
        manager.officePuzzle = 1;
    }

    public void skipArcade(){
        manager.previousScene = "CityArcade";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.visitedCoffee = 1;
        manager.readRecipeBook = 1;
        manager.addedCoffeeMachineItem = 1;
        manager.followFriendinOffice = 1;
        manager.officePuzzle = 1;
        manager.firstApartment = 1;
        manager.cleanedRoom = 1;
        manager.timeCapsule = 1;
        manager.firstDateDia = 1;
    }
}

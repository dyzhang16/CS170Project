using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkip : MonoBehaviour
{
    public GameManager manager;

    public void skipStreet(){
        manager.previousScene = "Tutorial";

        manager.flowerPuzzle = 1;

        manager.firstFriendMeeting = 0;
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.visitedCoffee = 0;
        manager.readRecipeBook = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        manager.firstApartment = 0;
        manager.cleanedRoom = 0;
        manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        manager.arcadeFirstVisit = 0;
        manager.arcadeFirstCrane = 0;
        manager.arcadeFirstDance = 0;
        manager.arcadeNoCraneDirs = 0;
        manager.arcadeNoDanceDirs = 0;
    }

    public void skipCoffee(){
        manager.previousScene = "StreetIntro";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 1;
        
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.visitedCoffee = 0;
        manager.readRecipeBook = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        manager.firstApartment = 0;
        manager.cleanedRoom = 0;
        manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        manager.arcadeFirstVisit = 0;
        manager.arcadeFirstCrane = 0;
        manager.arcadeFirstDance = 0;
        manager.arcadeNoCraneDirs = 0;
        manager.arcadeNoDanceDirs = 0;
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
       
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        manager.firstApartment = 0;
        manager.cleanedRoom = 0;
        manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        manager.arcadeFirstVisit = 0;
        manager.arcadeFirstCrane = 0;
        manager.arcadeFirstDance = 0;
        manager.arcadeNoCraneDirs = 0;
        manager.arcadeNoDanceDirs = 0;
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
        manager.officeDeskPuzzle = 1;
        manager.firstApartment = 1;

        manager.cleanedRoom = 0;
        manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        manager.arcadeFirstVisit = 0;
        manager.arcadeFirstCrane = 0;
        manager.arcadeFirstDance = 0;
        manager.arcadeNoCraneDirs = 0;
        manager.arcadeNoDanceDirs = 0;
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
        manager.officeDeskPuzzle = 1;
        manager.firstApartment = 1;
        manager.cleanedRoom = 1;
        manager.timeCapsule = 1;
        manager.firstDateDia = 1;

        manager.arcadeFirstVisit = 0;
        manager.arcadeFirstCrane = 0;
        manager.arcadeFirstDance = 0;
        manager.arcadeNoCraneDirs = 0;
        manager.arcadeNoDanceDirs = 0;
    }
}

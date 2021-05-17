using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkip : MonoBehaviour
{
    public GameManager manager;

    public void resetVariables(){
        manager.flowerPuzzle = 0;
        manager.firstFriendMeeting = 0;
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.visitedCoffee = 0;
        manager.openLeftDoor = 0;
        manager.openMidDoor = 0;
        manager.openRightDoor = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.documentNeeded = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        
        // manager.firstApartment = 0;
        // manager.walkedToApartment = 0;
        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        GameManager.instance.deleteItems();
        GameManager.loadItems();
    }
    
    public void playNormal(){
        manager.flowerPuzzle = 0;
        manager.firstFriendMeeting = 0;
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.visitedCoffee = 0;
        manager.openLeftDoor = 0;
        manager.openMidDoor = 0;
        manager.openRightDoor = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        // manager.firstApartment = 0;
        // manager.walkedToApartment = 0;
        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        GameManager.instance.deleteItems();
        GameManager.loadItems();
    }

    public void skipStreet(){
        manager.previousScene = "Tutorial";

        manager.flowerPuzzle = 1;

        manager.firstFriendMeeting = 0;
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.wrongCoffee = 0;
        manager.visitedCoffee = 0;
        manager.openLeftDoor = 0;
        manager.openMidDoor = 0;
        manager.openRightDoor = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        // manager.firstApartment = 0;
        // manager.walkedToApartment = 0;
        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;
        
        // id-related vars default
        manager.idPickedUp = 0;
        manager.idNeeded = 0;
    }

    public void skipCoffee(){
        manager.previousScene = "StreetIntro";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 1;
        
        manager.hasReceipt = 0;
        manager.gaveDrink = 0;
        manager.visitedAfterCoffee = 0;
        manager.wrongCoffee = 0;
        manager.visitedCoffee = 0;
        manager.openLeftDoor = 0;
        manager.openMidDoor = 0;
        manager.openRightDoor = 0;
        manager.addedCoffeeMachineItem = 0;
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        // manager.firstApartment = 0;
        // manager.walkedToApartment = 0;
        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        // id-related vars default
        manager.idPickedUp = 0;
        manager.idNeeded = 0;
    }

    public void skipOffice(){
        manager.previousScene = "CityOffice";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.wrongCoffee = 0;
        manager.visitedCoffee = 1;
        manager.openLeftDoor = 1;
        manager.openMidDoor = 1;
        manager.openRightDoor = 1;
        manager.addedCoffeeMachineItem = 1;
       
        manager.followFriendinOffice = 0;
        manager.officePuzzle = 0;
        manager.officeDeskPuzzle = 0;
        // manager.firstApartment = 0;
        // manager.walkedToApartment = 0;
        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved if the player is at the office
        manager.idPickedUp = 1;
        manager.idNeeded = 2;
    }

    public void skipApartment(){
        manager.previousScene = "CityArcade";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.wrongCoffee = 0;
        manager.visitedCoffee = 1;
        manager.openLeftDoor = 1;
        manager.openMidDoor = 1;
        manager.openRightDoor = 1;
        manager.addedCoffeeMachineItem = 1;
        manager.followFriendinOffice = 1;
        manager.officePuzzle = 1;
        manager.officeDeskPuzzle = 1;
        // manager.firstApartment = 1;
        // manager.walkedToApartment = 2;

        // manager.cleanedRoom = 0;
        // manager.timeCapsule = 0;
        manager.firstDateDia = 0;
        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved at this stage
        manager.idPickedUp = 1;
        manager.idNeeded = 2;
    }

    public void skipArcade(){
        manager.previousScene = "CityArcade";

        manager.flowerPuzzle = 1;
        manager.firstFriendMeeting = 3;
        manager.hasReceipt = 1;
        manager.gaveDrink = 1;
        manager.visitedAfterCoffee = 1;
        manager.wrongCoffee = 0;
        manager.visitedCoffee = 1;
        manager.openLeftDoor = 1;
        manager.openMidDoor = 1;
        manager.openRightDoor = 1;
        manager.addedCoffeeMachineItem = 1;
        manager.followFriendinOffice = 1;
        manager.officePuzzle = 1;
        manager.officeDeskPuzzle = 1;
        // manager.firstApartment = 1;
        // manager.walkedToApartment = 2;
        // manager.cleanedRoom = 1;
        // manager.timeCapsule = 1;
        manager.firstDateDia = 1;

        // manager.arcadeFirstVisit = 0;
        // manager.arcadeFirstCrane = 0;
        // manager.arcadeFirstDance = 0;
        // manager.arcadeNoCraneDirs = 0;
        // manager.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved at this stage
        manager.idPickedUp = 1;
        manager.idNeeded = 2;
    }
}

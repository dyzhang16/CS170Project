using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkip : MonoBehaviour
{
    void Start(){
        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }

    public void resetVariables(){
        GameManager.instance.flowerPuzzle = 0;
        GameManager.instance.friendRanIntoPlayerCutscene = 0;
        GameManager.instance.firstFriendMeeting = 0;
        GameManager.instance.hasReceipt = 0;
        GameManager.instance.gaveDrink = 0;
        GameManager.instance.visitedAfterCoffee = 0;
        GameManager.instance.visitedCoffee = 0;
        GameManager.instance.openLeftDoor = 0;
        GameManager.instance.openMidDoor = 0;
        GameManager.instance.openRightDoor = 0;
        GameManager.instance.addedCoffeeMachineItem = 0;
        GameManager.instance.followFriendinOffice = 0;
        GameManager.instance.documentNeeded = 0;
        GameManager.instance.officePuzzle = 0;
        GameManager.instance.officeDeskPuzzle = 0;
        
        // GameManager.instance.firstApartment = 0;
        // GameManager.instance.walkedToApartment = 0;
        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        GameManager.instance.clearInventory = true;
    }
    
    public void playNormal(){
        GameManager.instance.flowerPuzzle = 0;
        GameManager.instance.friendRanIntoPlayerCutscene = 0;
        GameManager.instance.firstFriendMeeting = 0;
        GameManager.instance.hasReceipt = 0;
        GameManager.instance.gaveDrink = 0;
        GameManager.instance.visitedAfterCoffee = 0;
        GameManager.instance.visitedCoffee = 0;
        GameManager.instance.openLeftDoor = 0;
        GameManager.instance.openMidDoor = 0;
        GameManager.instance.openRightDoor = 0;
        GameManager.instance.addedCoffeeMachineItem = 0;
        GameManager.instance.followFriendinOffice = 0;
        GameManager.instance.officePuzzle = 0;
        GameManager.instance.officeDeskPuzzle = 0;
        // GameManager.instance.firstApartment = 0;
        // GameManager.instance.walkedToApartment = 0;
        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        GameManager.instance.clearInventory = true;
    }

    public void skipStreet(){
        GameManager.instance.previousScene = "Tutorial";

        GameManager.instance.flowerPuzzle = 1;
        GameManager.instance.friendRanIntoPlayerCutscene = 0;

        GameManager.instance.firstFriendMeeting = 0;
        GameManager.instance.hasReceipt = 0;
        GameManager.instance.gaveDrink = 0;
        GameManager.instance.visitedAfterCoffee = 0;
        GameManager.instance.wrongCoffee = 0;
        GameManager.instance.visitedCoffee = 0;
        GameManager.instance.openLeftDoor = 0;
        GameManager.instance.openMidDoor = 0;
        GameManager.instance.openRightDoor = 0;
        GameManager.instance.addedCoffeeMachineItem = 0;
        GameManager.instance.followFriendinOffice = 0;
        GameManager.instance.officePuzzle = 0;
        GameManager.instance.officeDeskPuzzle = 0;
        // GameManager.instance.firstApartment = 0;
        // GameManager.instance.walkedToApartment = 0;
        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;
        
        // id-related vars default
        GameManager.instance.idPickedUp = 0;
        GameManager.instance.idNeeded = 0;

        GameManager.instance.clearInventory = true;
    }

    public void skipCoffee(){
        GameManager.instance.previousScene = "StreetIntro";

        GameManager.instance.flowerPuzzle = 1;
        GameManager.instance.friendRanIntoPlayerCutscene = 1;
        GameManager.instance.firstFriendMeeting = 1;
        
        GameManager.instance.hasReceipt = 0;
        GameManager.instance.gaveDrink = 0;
        GameManager.instance.visitedAfterCoffee = 0;
        GameManager.instance.wrongCoffee = 0;
        GameManager.instance.visitedCoffee = 0;
        GameManager.instance.openLeftDoor = 0;
        GameManager.instance.openMidDoor = 0;
        GameManager.instance.openRightDoor = 0;
        GameManager.instance.addedCoffeeMachineItem = 0;
        GameManager.instance.followFriendinOffice = 0;
        GameManager.instance.officePuzzle = 0;
        GameManager.instance.officeDeskPuzzle = 0;
        // GameManager.instance.firstApartment = 0;
        // GameManager.instance.walkedToApartment = 0;
        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        // id-related vars default
        GameManager.instance.idPickedUp = 0;
        GameManager.instance.idNeeded = 0;

        GameManager.instance.clearInventory = true;
    }

    public void skipOffice(){
        GameManager.instance.previousScene = "CityOffice";

        GameManager.instance.flowerPuzzle = 1;
        GameManager.instance.friendRanIntoPlayerCutscene = 1;
        GameManager.instance.firstFriendMeeting = 3;
        GameManager.instance.hasReceipt = 1;
        GameManager.instance.gaveDrink = 1;
        GameManager.instance.visitedAfterCoffee = 1;
        GameManager.instance.wrongCoffee = 0;
        GameManager.instance.visitedCoffee = 1;
        GameManager.instance.openLeftDoor = 1;
        GameManager.instance.openMidDoor = 1;
        GameManager.instance.openRightDoor = 1;
        GameManager.instance.addedCoffeeMachineItem = 1;
       
        GameManager.instance.followFriendinOffice = 0;
        GameManager.instance.officePuzzle = 0;
        GameManager.instance.officeDeskPuzzle = 0;
        // GameManager.instance.firstApartment = 0;
        // GameManager.instance.walkedToApartment = 0;
        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved if the player is at the office
        GameManager.instance.idPickedUp = 1;
        GameManager.instance.idNeeded = 2;

        GameManager.instance.clearInventory = true;
    }

    public void skipApartment(){
        GameManager.instance.previousScene = "CityArcade";

        GameManager.instance.flowerPuzzle = 1;
        GameManager.instance.firstFriendMeeting = 3;
        GameManager.instance.hasReceipt = 1;
        GameManager.instance.gaveDrink = 1;
        GameManager.instance.visitedAfterCoffee = 1;
        GameManager.instance.wrongCoffee = 0;
        GameManager.instance.visitedCoffee = 1;
        GameManager.instance.openLeftDoor = 1;
        GameManager.instance.openMidDoor = 1;
        GameManager.instance.openRightDoor = 1;
        GameManager.instance.addedCoffeeMachineItem = 1;
        GameManager.instance.followFriendinOffice = 1;
        GameManager.instance.officePuzzle = 1;
        GameManager.instance.officeDeskPuzzle = 1;
        // GameManager.instance.firstApartment = 1;
        // GameManager.instance.walkedToApartment = 2;

        // GameManager.instance.cleanedRoom = 0;
        // GameManager.instance.timeCapsule = 0;
        GameManager.instance.firstDateDia = 0;
        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved at this stage
        GameManager.instance.idPickedUp = 1;
        GameManager.instance.idNeeded = 2;

        GameManager.instance.clearInventory = true;
    }

    public void skipArcade(){
        GameManager.instance.previousScene = "CityArcade";

        GameManager.instance.flowerPuzzle = 1;
        GameManager.instance.firstFriendMeeting = 3;
        GameManager.instance.hasReceipt = 1;
        GameManager.instance.gaveDrink = 1;
        GameManager.instance.visitedAfterCoffee = 1;
        GameManager.instance.wrongCoffee = 0;
        GameManager.instance.visitedCoffee = 1;
        GameManager.instance.openLeftDoor = 1;
        GameManager.instance.openMidDoor = 1;
        GameManager.instance.openRightDoor = 1;
        GameManager.instance.addedCoffeeMachineItem = 1;
        GameManager.instance.followFriendinOffice = 1;
        GameManager.instance.officePuzzle = 1;
        GameManager.instance.officeDeskPuzzle = 1;
        // GameManager.instance.firstApartment = 1;
        // GameManager.instance.walkedToApartment = 2;
        // GameManager.instance.cleanedRoom = 1;
        // GameManager.instance.timeCapsule = 1;
        GameManager.instance.firstDateDia = 1;

        // GameManager.instance.arcadeFirstVisit = 0;
        // GameManager.instance.arcadeFirstCrane = 0;
        // GameManager.instance.arcadeFirstDance = 0;
        // GameManager.instance.arcadeNoCraneDirs = 0;
        // GameManager.instance.arcadeNoDanceDirs = 0;

        // the ID would have been already retrieved at this stage
        GameManager.instance.idPickedUp = 1;
        GameManager.instance.idNeeded = 2;

        GameManager.instance.clearInventory = true;
    }
}

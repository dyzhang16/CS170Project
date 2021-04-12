using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class CoffeeYarnCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public Drink cloneItem;
    public Drink Coffee1;
    public Drink Coffee2;
    public Drink Coffee3;
    public Drink Coffee4;
    public Drink Coffee5;
    public Drink BizarreCoffee;
    private Drink so;
    Dictionary<(int, int), Drink> RecipeBook = new Dictionary<(int, int), Drink>();
    

    private void Awake()
    {
        RecipeBook.Add((0, 0), Coffee1);
        RecipeBook.Add((1, 2), Coffee2);
        RecipeBook.Add((3, 4), Coffee3);
        RecipeBook.Add((7, 6), Coffee4);
        RecipeBook.Add((10, 10), Coffee5);
        so = Instantiate(BizarreCoffee);
        so.itemName = "Bizarre Coffee";
        so.itemDescription = "A suspicious cup of coffee… Did something go wrong? This isn’t on the menu!";

        dialogueRunner.AddCommandHandler(
        "AddCoffee",     // the name of the command
        AddCoffeetoInventory // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Check",     // the name of the command
        CheckCoffee // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "CheckCup",     // the name of the command
        CheckCup // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Sleeve",     // the name of the command
        CoffeeSleeve // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Lid",     // the name of the command
        CoffeeLid // the method to run
        );

    }
    [YarnCommand("AddBrewedCoffee")]
    public void AddBrewedCoffee()
    {
        Debug.Log("Adding Drink!");
        var so = Instantiate(cloneItem);
        so.itemName = "Random Coffee";
        so.itemDescription = "One could possibly put cream and sugar in this…";
        Inventory.instance.AddItem(so);
    }
    public void CheckCoffee(string[] parameters)
    {
        string Object = string.Join(" ", parameters);
        if (Inventory.instance.CheckItem(Object))
        {
            Debug.Log("Found item");
            CustomVariableStorage.SetValue("$DrinkExists", 1);
        }
        else
        {
            Debug.Log("Didnt find Item");
            CustomVariableStorage.SetValue("$DrinkExists", 0);
        }
        
    }
    public void CheckCup(string[] parameters)
    {
        string Object = string.Join("", parameters);
        if (Inventory.instance.CheckItem(Object))
        {
            Debug.Log("Found item");
            CustomVariableStorage.SetValue("$CupExists", 1);
        }
        else
        {
            Debug.Log("Didn't find Item");
            CustomVariableStorage.SetValue("$CupExists", 0);
        }
    }
    private void CoffeeSleeve(string[] parameters)
    {
        string Object = string.Join(" ", parameters);
        Item item = Inventory.instance.FindItem(Object);
        var boa = item as Drink;
        (int, int) received = (boa.Cream, boa.Sugar);
        Drink creation = RecipeBook.ContainsKey(received) ? RecipeBook[received] : so;
        if (boa.lidOn)
        {
            boa.icon = boa.sleeveAndLidIcon;
            boa.sleeveOn = true;
            Inventory.instance.AddItem(creation);
            dialogueRunner.StartDialogue(creation.name);

        }
        else
        {
            boa.icon = boa.sleeveOnIcon;
            boa.sleeveOn = true;
            boa.itemDescription = "A dressed up cup of coffee! It’s still susceptible to spills, though. Hmn...";
            Inventory.instance.AddItem(boa);
        }
        Inventory.instance.RemoveItem(item);
    }
    private void CoffeeLid(string[] parameters)
    {
        string Object = string.Join(" ", parameters);
        Item item = Inventory.instance.FindItem(Object);
        var boa = item as Drink;
        (int, int) received = (boa.Cream, boa.Sugar);
        Drink creation = RecipeBook.ContainsKey(received) ? RecipeBook[received] : so;
        if (boa.sleeveOn)
        {
            boa.icon = boa.sleeveAndLidIcon;
            boa.lidOn = true;
            Inventory.instance.AddItem(creation);
            dialogueRunner.StartDialogue(creation.name);
        }
        else
        {
            boa.icon = boa.lidOnIcon;
            boa.lidOn = true;
            boa.itemDescription = "Can’t spill it now! Holding it is still a test of endurance, though. Hmn…";
            Inventory.instance.AddItem(boa);
        }
        Inventory.instance.RemoveItem(item);
    }

    private void AddCoffeetoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        Item boa = PotentialObject.GetComponent<CoffeeAssignment>().droppedCoffee as Drink;
        Inventory.instance.AddItem(boa);
    }


}

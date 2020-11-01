using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
   public float speed;
   public Rigidbody2D rb;

   
   public void FixedUpdate()
   {
      float h = Input.GetAxis("Horizontal");
      float v = Input.GetAxis("Vertical");

      Vector3 tempVect = new Vector3(h, v, 0);
      tempVect = tempVect.normalized * speed * Time.deltaTime;
      rb.MovePosition(rb.transform.position + tempVect);
      Menu();
   }

   void Menu()
   {  
      if(Input.GetKeyDown(KeyCode.Tab))
      {
         GameObject ip = Inventory.instance.inventoryPanel;

         if(!ip.activeSelf)
         {
            ip.SetActive(true);
         }
         else
         {
             ip.SetActive(false);
         }
      }

   }
   /*public Vector3 targetPosition;               //https://forum.unity.com/threads/2d-mouse-point-click-movement-system-quick-tutorial.217886/
   void Update () {
 
        if(Input.GetKeyDown(KeyCode.Mouse0))
           {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10);
   }*/
}




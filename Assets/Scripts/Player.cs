using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
   public float speed;
   public Rigidbody2D rb;
   public float mouseSpeed;
   private bool invActive;
   
   public void Update()
   {
      if(Input.GetKey(KeyCode.Mouse0) && !invActive) {
         Mouse();
      }

      Keyboard();

      if(Input.GetKeyDown(KeyCode.Tab)){
         Menu();
      }
   }

   void Mouse()
   {
      Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      var direction = targetPosition - transform.position;

      rb.AddForce(direction * Time.deltaTime * this.mouseSpeed);
   }

   void Keyboard()
   {
      float h = Input.GetAxis("Horizontal");
      float v = Input.GetAxis("Vertical");

      Vector3 movement = new Vector3(h, v, 0) * this.speed * Time.deltaTime;
      rb.MovePosition(transform.position + movement);
   }

   void Menu()
   {  
      GameObject ip = Inventory.instance.inventoryPanel;

      switch(ip.activeSelf) {
         case false:
            ip.SetActive(true);
            invActive = true;
            break;
         case true:
            ip.SetActive(false);
            invActive = false;
            break;
      }
   }
}




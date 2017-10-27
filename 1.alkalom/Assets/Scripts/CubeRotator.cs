using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Ennek a scriptnek a segítségével azt a GameObject-et, amit hozzácsatoltunk mindig a kurzorunk írányába fog nézni.
*/
public class CubeRotator : MonoBehaviour {

    
    //Ez a funkció a Unity által meg van hívva minden egyes frame-ben
	void Update () {
	    //A kurzor poziciója a képernyőn(ScreenSpace-ben) 
	    // Ez lehetne akár 2D-s vektor is 
	    //Vector2 mousePositionOnScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	    // Ezt majd a tranformációnál ki kell egésziteni, hogy 3D-s vektor kapjunk
	    Vector3 mousePositionOnScreen =new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
	       
	    
	    //A kurzor pozicójának áttransformálása WorldSpace-be.
	    //Abban az esetben ha a 2D-s vektor használtuk volna akkor így nézett volna ki:
	    Vector3 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
	    //Vector3 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionOnScreen.x,mousePositionOnScreen.y,Camera.main.transform.position.y));
	    
	    // Debug funkció, hogy viuzálisan is megjelenjen a vektor, amerre forgatni akarjuk a kockát
	    Debug.DrawRay(transform.position,mousePositionInWorldSpace+ Vector3.up *transform.position.y, Color.red);
	  
	   
	    //A kocka tényleges elforgatása itt történik
	    //a transform LookAt az a cél szolgálja, hogy a test et úgy forgatja el, hogy a forward vektorja a paraméterként megadott írányba nézzen
	    transform.LookAt(mousePositionInWorldSpace+ Vector3.up *transform.position.y);
	}
}

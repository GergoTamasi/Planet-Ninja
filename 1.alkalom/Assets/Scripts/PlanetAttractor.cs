using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Egy gömb alakú testen gravitációs mező szimulálására használható
public class PlanetAttractor : MonoBehaviour {

    //Gravitációs erő nagysága
    public float Gravitiy = 9.8f;

    //Ennek a metódus felelős, hogy a testhez vonzódjanak a megfelelő GameObjectek, amik rendelkeznek Rigidbody componenttel
    public void Attract(Rigidbody body) {
        
        //A vonzó test középpontjától a vonzott test középpontjába muató vektor 
        Vector3 gravityUp = (body.position - transform.position).normalized;
        //A vonzott test haladási irányával merőleges vektor
        // A test koordináta rendszerében a (0,1,0) vektor
        var localUp = body.transform.up;

        //A vonzott testre erőt(vonzást) helyezünk, ami a vonzó testen fogja tartani a vonzott testet 
        body.AddForce(gravityUp * -Gravitiy);

        //A vonzott testet ezután úgy kell elforgatni, hogy vonzott test haladási irányára merőleges vektor(localUp)
        //megegyezzen a gravitációs erő vonzásának irányával
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }

}

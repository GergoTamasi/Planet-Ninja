using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
//A PlanetAttractor gravitáció mezejével kölcsönhatásba kerülő objecktek szimulálására használható

//Függővé tesszük ezt a script-et attól a GameObjecten lévő RigidBody-tól
//Megköveteljük, hogy legyen a GameObject-en Rigidbody Componen
[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

        //A vonzó test referenciája
        private PlanetAttractor _planet;

        //A RigidBody referenciája
        private Rigidbody _rbody;



        //Inicializálása használatos
        //Akkor fut le,amikor az a GameObject létrejön, amihez csatoltuk ezt a scriptet
        //A GameObject akkor jön létre, ha belépünk arra a Scene-re, amihez tartozik vagy más mondon hozzuk létre a GameObjectet(Instantiate())
        void Awake() {
            //Az aktív Scene-en megkeressük a PlanetAttractor típusú objektumot és elmentjük a refenciáját
            _planet = FindObjectOfType<PlanetAttractor>();
            //A GameObject-en lévő Rigidbody component referenciájának lementése
            _rbody = GetComponent<Rigidbody>();
            //Mivel saját gravitációs erőt szimulálunk, ezért ne használja a Unity saját gravitációs rendszerét
            _rbody.useGravity = false;
            //A saját rendszerünk elforgatja megfelelően a vonzott testeket
            _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //A fixedUpdate és az Update közötti különbség, hogy az FixedUpdate nem függ az FPS-től
        //így egy biztosan loop-ot kapunk
        //Főleg fizikai számításokhoz ahasználják
        //Fizikai Loop nak is nevezik
        void FixedUpdate() {
            //itt lépünk kölcsönhatásba a vonzó testtel
            //Ezáltal minden FixedUpdate hívásban a vonzó test elvégzi ezen a test-en a megfelelő számíátsokat, amivel szimulálja a gravitációs vonzást
            _planet.Attract(_rbody);
        }
}
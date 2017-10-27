using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [RequireComponent(typeof(GravityBody))]
    public class BodyController : MonoBehaviour {

        public float MoveSpeed = 2f;
        
        private Rigidbody _rbody;


      
        private Vector3 _smoothMoveVelocity;

        private Vector3 _smoothMoveAmount;


        //Inicializálása használatos
        //Akkor fut le,amikor az a GameObject létrejön, amihez csatoltuk ezt a scriptet
        //A GameObject akkor jön létre, ha belépünk arra a Scene-re, amihez tartozik vagy más mondon hozzuk létre a GameObjectet(Instantiate())
        void Awake() {
            //A GameObject-en lévő Rigidbody component referenciájának lementése
           _rbody = GetComponent<Rigidbody>();

        }
        //Frame-enként egyszer van meghívva ez a metódus
        void Update() {
            //A Unity által lekezelt játékos Input-ot használjuk és kérjük le az elmozdulás írányát
            //Unity-ban a project Settings-ben az Input alatt vannak beállítva, milyen inputokat kezeljen le
            Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        
            //Meg van az elmozdulás egység vektorra és ezt kell megszorozni a mértékével, ami a sebesség
            // Így kapjuk meg a tényleges elmozudást vektorát
            Vector3 calculatedMoveAmount = moveDirection * MoveSpeed;
            
            //Smootholjuk a mozgás, hogy ne legyen darabos
            //A függvény lényege, hogy a két vektor között kiszámol egy "átmenet" vektor és ezzel teszi simábá a mozgást
            _smoothMoveAmount =Vector3.SmoothDamp(_smoothMoveAmount, calculatedMoveAmount, ref _smoothMoveVelocity, .15f);
         
        }

        
        //A fixedUpdate és az Update közötti különbség, hogy az FixedUpdate nem függ az FPS-től
        //így egy biztosan loop-ot kapunk
        //Főleg fizikai számításokhoz ahasználják
        //Fizikai Loop nak is nevezik
        private void FixedUpdate() {
            //LocalSpace-ből áttranszformáljuk az elmozdulást 
            var localMove = transform.TransformDirection(_smoothMoveAmount) * Time.fixedDeltaTime;
            
            //Miután áttranszformáltuk az elmozdulás vektor, hozzákell adni a mostani poziciónkhoz és abba az pontba elmozdítani a testet
            _rbody.MovePosition(_rbody.position + localMove);
        }
    }

using System.Collections.Generic;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Action.Actor;
using U4K.Utils;
using UnityEngine;
using U4K.BehaviourTemplate;
using UnityEngine.UI;

namespace U4K.BaseScripts
{
    public class ShootBase : MonoBehaviour
    {
        private ShootTemplate template;
        private GameObject bullet;
        private SphereCollider bulletCollider;
        private Rigidbody bulletRigidbody;    
        public float fireRate = 0.3f;     
        float nextFire;  
        public List<BasicActor> BasicActors = new List<BasicActor>();
        public List<BasicAction> BasicActions = new List<BasicAction>();
        public int bulletcount = 30;
        public bool reload = false;
        public float timeleft;
        public Text Score_UIText; 
        public Text Score_UIText2; 
        public Text Score_UIText3; 
        public Text Score_UIText4; 
        public Text Score_UIText5; 

        public GameObject reloading;

        //public sounds sound;

       // AudioSource snd;
       // public AudioClip shoot;
       // public AudioClip ;
        private void Start()
        {
            
            timeleft = 1;
            //sound = gameObject.GetComponent<sounds>();
            template = gameObject.GetComponent<ShootTemplate>();
            // init bullet
            bullet = new GameObject();
            bullet.transform.forward = gameObject.transform.forward;
            bulletRigidbody = bullet.AddComponent<Rigidbody>();
            bulletRigidbody.useGravity = false;
            bulletCollider = bullet.AddComponent<SphereCollider>();
            bulletCollider.isTrigger = true;
            bulletCollider.radius = 0.05f;

            var ps = bullet.AddComponent<ParticleSystem>();
            var psMainSettings = ps.main;
            psMainSettings.startDelay = 0;
            psMainSettings.startLifetime = 0.1f;
            psMainSettings.startSpeed = 1.5f;
            psMainSettings.startSize = 0.2f;
            psMainSettings.simulationSpace = ParticleSystemSimulationSpace.World;
            psMainSettings.startColor = Color.yellow;

            var psEmissionSettings = ps.emission;
            psEmissionSettings.rateOverTime = 0;
            psEmissionSettings.rateOverDistance = 50;

            var psShapeSettings = ps.shape;
            psShapeSettings.shapeType = ParticleSystemShapeType.Sphere;
            psShapeSettings.radius = 0.02f;
            psShapeSettings.radiusThickness = 0;
            
            var mat = Resources.Load<Material>("part_glow_mat");
            ps.GetComponent<ParticleSystemRenderer>().material = mat;
        }

        void Update()
        {
            
            //sideways button on oculus controlers
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.GetActiveController()) > 0.5)
            {
                reload = true;
                bulletcount = 30;
               // snd.PlayOneShot(realoadsound);
            }

            //if the gun is in reloading state
            if (reload)
            {
                reloading.SetActive(true);
                reloading.GetComponent<Text>().text = "Reloading!";
                Score_UIText.text = "reloading";
                Score_UIText2.text = "reloading";
                Score_UIText3.text = "reloading";
                Score_UIText4.text = "reloading";
                timeleft -= Time.deltaTime;
                if (timeleft < 0) { reload = false; timeleft = 1; }
            }
            else
            {
                if(bulletcount == 0 && !reload)
                {
                    reloading.SetActive(true);
                    reloading.GetComponent<Text>().text = "OUT OF AMMO";
                    Score_UIText.text = "0";
                    Score_UIText2.text = "0";
                    Score_UIText3.text = "0";
                    Score_UIText4.text = "0";
                }
                else { 
                //otherwise dont show reloading and show ammo count
                if (reloading != null)
                    reloading.SetActive(false);
                if (Score_UIText != null)
                    Score_UIText.text = bulletcount.ToString();
                if (Score_UIText2 != null)
                    Score_UIText2.text = bulletcount.ToString();
                if (Score_UIText3 != null)
                    Score_UIText3.text = bulletcount.ToString();
                if (Score_UIText4 != null)
                    Score_UIText4.text = bulletcount.ToString();
                }
                
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.GetActiveController()) > 0.5)
            {
                bulletcount = 30;
                reload = true;
            }

            OVRInput.Update();


            var gop = gameObject.transform.position;
            var co = gameObject.GetComponent<CustomObjectComponent>();
            if (co != null)
            {
                gop = co.CenterPosition();
            }

            // the index trigger butoon on the oulus controllers
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.GetActiveController()) && Time.time > nextFire && LevelStart.isActive || Input.GetKeyDown("right"))
            {
                //GameObject currentcontroller = OVRInput.GetActiveController();
                if (reload == false)
                {
                    if (bulletcount > 0)
                    {
                        //sound.check = true;
                        //snd.PlayOneShot(shoot);
                        //fires a bullet foward from the gun, take away a bullet from bullet count 
                        nextFire = Time.time + fireRate * template.fireRate;
                        for (int i = 0; i < template.bulletNumber; ++i) {


                            bulletcount = bulletcount - 1;
                            Score_UIText.text = bulletcount.ToString();
                            var bulletInstantiate = Instantiate(bullet, gop + new Vector3(0.1f * i, 0.1f * i, 0), Quaternion.identity);
                            bulletInstantiate.transform.forward = transform.forward;
                            var dt = bulletInstantiate.AddComponent<BulletDestroy>();
                            dt.shooter = gameObject;
                            dt.startPosition = gop;
                            dt.BasicActions = BasicActions;
                            dt.BasicActors = BasicActors;
                            bulletInstantiate.GetComponent<Rigidbody>().velocity = bulletInstantiate.transform.forward * 25;
                        }
                    }
                    //else
                    //{
                    //    sound.check = false;
                    //}
                      
                }
            }
        }
    }

    public class BulletDestroy : MonoBehaviour
    {
        public float lifetime = 5;
        public GameObject shooter;
        public Vector3 startPosition;
        public List<BasicActor> BasicActors = new List<BasicActor>();
        public List<BasicAction> BasicActions = new List<BasicAction>();
        private Material expMat;
        void Start ()
        {
            expMat = Resources.Load<Material>("part_blast_mat");
            Destroy(gameObject, lifetime);
        }
        private void Update()
        {
            transform.position += transform.forward * 5 * Time.deltaTime;
        }
        private void OnTriggerEnter(Collider other)
        {
            var shooterParent = shooter.transform.parent;
            bool isCollison = other.gameObject != shooter;
            if (shooterParent != null)
                isCollison = isCollison && other.gameObject != shooterParent.gameObject;
            if (isCollison)
            {
                RaycastHit[] hits = Physics.RaycastAll(startPosition, transform.position - startPosition, Vector3.Distance(startPosition, transform.position));
                if (hits != null && hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject != shooter)
                        {
                            if (shooterParent != null)
                            {
                                if (hit.collider.gameObject != shooterParent.gameObject)
                                {
                                    InitExplosion(hit.point);
                                    break;
                                }
                            }
                            else
                            {
                                InitExplosion(hit.point);
                                break;
                            }
                        }
                    }
                }
                for (int i = 0; i < BasicActors.Count; i++)
                {
                    var shooted = BasicActors[i];
                    var action = BasicActions[i];
                    var filteredGo = CommonUtil.GetExistedParentGameObject(other.gameObject);
                    if (shooted.basicActorType == BasicActorType.Single 
                        && filteredGo == shooted.singleActor)
                    {
                        ExecuteAction(action, filteredGo);
                    }
                    //Debug.Log(other.name);
                    if (shooted.basicActorType == BasicActorType.Tagged &&
                        filteredGo.CompareTag(shooted.tagContent))
                    {
                        ExecuteAction(action, filteredGo);
                    }
                }
                Destroy(gameObject);
            }
        }

        private void ExecuteAction(BasicAction action, GameObject actor)
        {
            if (action != null)
            {
                if (action.UseDefaultActor)
                    action.Actor = gameObject;
                if (action.IsRuntimeActor)
                    action.Actor = actor;
                if (action.Actor != null){
                    GameManager.dinosShot++;
                    //Debug.Log("Oh no I died!!!"+GameManager.dinosShot);
                    action.Execute();
                }
            }
        }

        private void InitExplosion(Vector3 position)
        {
            // init explosion
            var explosion = new GameObject();
            explosion.transform.position = position;
            explosion.AddComponent<ExplosionDestroy>();
            var ps = explosion.AddComponent<ParticleSystem>();
            var psMainSettings = ps.main;
            psMainSettings.startDelay = 0;
            psMainSettings.startLifetime = 0.05f;
            psMainSettings.startSpeed = 2f;
            psMainSettings.startSize = 0.08f;

            var psEmissionSettings = ps.emission;
            psEmissionSettings.rateOverTime = 0;
            psEmissionSettings.rateOverDistance = 0;
            psEmissionSettings.burstCount = 1;
            psEmissionSettings.SetBurst(0, new ParticleSystem.Burst(0, 15, 15, 1, 0));

            var psShapeSettings = ps.shape;
            psShapeSettings.shapeType = ParticleSystemShapeType.Sphere;
            psShapeSettings.radius = 0.3f;
            psShapeSettings.radiusThickness = 0;

            ps.GetComponent<ParticleSystemRenderer>().material = expMat;
        }
    }

    public class ExplosionDestroy : MonoBehaviour
    {
        public float lifetime = 2;
        
        void Start ()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PowerUpSpawner : SerializedMonoBehaviour {

    private enum modifierTypes
    {
        upDamage,
        upSpeed,
        upHealth,
        downDamage,
        downSpeed,
        downHealth
    }

    [Title("Spawn Properties")]
    [SerializeField] private float _ratio = 10;
    [SerializeField] private float _maxRatioVariation = 5;
    [SerializeField] private float _radius;
    [SerializeField] private int _powerUpRadius = 2;
    [SerializeField] private LayerMask _invalidSpawnLayers;
    [Title("PowerUp Properties")]
    [SerializeField] private int maxAbsDamage = 1;
    [SerializeField] private int maxAbsSpeed = 1;
    [SerializeField] private int maxAbsHealth = 2;
    [Title("Modifiers Prefabs")]
    [SerializeField] private Dictionary<modifierTypes, GameObject> powerUps;

    private bool placeFounded = false;
    private Vector3 position;
    private GameObject auxiliar;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnPowerUp());
	}
	
	IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(_ratio + Random.Range(-5, 5));
        //Debug.Log("Spawn");
        while (!placeFounded)
        {
            FindPosition();
            yield return null;
        }
        placeFounded = false;
        InstanciatePowerUp();
        StartCoroutine(SpawnPowerUp());
    }

    private void FindPosition()
    {
        Vector3 _right = transform.right * Random.Range(-_radius, _radius);
        Vector3 _left = transform.forward * Random.Range(-_radius, _radius);
        //position.x = transform.position.x + Random.Range(-_radius, _radius);
        //position.y = 1;
        //position.z = transform.position.z + Random.Range(-_radius, _radius);
        position = (_right + _left) + transform.position;

        if (Physics.OverlapSphere(position, 1, _invalidSpawnLayers).Length == 0)
        {
            placeFounded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position,new Vector3(_radius * 2,1,_radius * 2));
        Gizmos.DrawLine(transform.position + (transform.forward * _radius + transform.right * _radius), transform.position + (-transform.forward * _radius + transform.right * _radius));
        Gizmos.DrawLine(transform.position + (-transform.forward * _radius + transform.right * _radius), transform.position + (-transform.forward * _radius - transform.right * _radius));
        Gizmos.DrawLine(transform.position + (-transform.forward * _radius - transform.right * _radius), transform.position + (transform.forward * _radius - transform.right * _radius));
        Gizmos.DrawLine(transform.position + (transform.forward * _radius - transform.right * _radius), transform.position + (transform.forward * _radius + transform.right * _radius));


        //Gizmos.DrawWireMesh(Cube)
    }

    private void InstanciatePowerUp()
    {
        int selector = Random.Range(0, 3) + Random.Range(0, 2) * 3;
        modifierTypes selectedPowerUp = (modifierTypes)selector;
        auxiliar = Instantiate(powerUps[selectedPowerUp], position, Quaternion.identity);
        PowerUp myPowerUp = auxiliar.GetComponent<PowerUp>();
        switch (selectedPowerUp)
        {
            case modifierTypes.upDamage:
                myPowerUp.SetValue(Random.Range(1,maxAbsDamage+1));
                break;
            case modifierTypes.upSpeed:
                myPowerUp.SetValue(Random.Range(1, maxAbsSpeed+1));
                break;
            case modifierTypes.upHealth:
                myPowerUp.SetValue(Random.Range(1, maxAbsHealth+1));
                break;
            case modifierTypes.downDamage:
                myPowerUp.SetValue(Random.Range(-maxAbsDamage, 0));
                break;
            case modifierTypes.downSpeed:
                myPowerUp.SetValue(Random.Range(-maxAbsSpeed, 0));
                break;
            case modifierTypes.downHealth:
                myPowerUp.SetValue(Random.Range(-maxAbsHealth, 0));
                break;
        }
        auxiliar = null;
    }
}

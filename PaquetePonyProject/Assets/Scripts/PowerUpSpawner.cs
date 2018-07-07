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
    [SerializeField] private Vector3 _mapSize;
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
        position.x = Random.Range(0, (int)_mapSize.x + 1);
        position.y = 0;
        position.z = Random.Range(0, (int)_mapSize.z + 1);
        position += transform.position;

        if(Physics.OverlapSphere(position, 1, _invalidSpawnLayers).Length == 0)
        {
            placeFounded = true;
        }
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

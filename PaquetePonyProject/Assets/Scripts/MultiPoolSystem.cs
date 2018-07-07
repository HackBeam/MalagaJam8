using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPoolSystem : MonoBehaviour
{
	[SerializeField] private List<GameObject> prefabList;
	private List<List<GameObject>> pooledObjects;

	private void Awake()
	{
//		Services.instance.poolingSystem = this;
		pooledObjects = new List<List<GameObject>>();

		for (int i = 0; i < prefabList.Count; i++)
		{
			pooledObjects.Add(new List<GameObject>());
		}
	}

	/// <summary>
	/// Instantiates a new GameObject stored on the prefabList at the given index.
	/// </summary>
	/// <param name="index">Index qhere the prefab is stored</param>
	/// <returns>The new GameObject instantiated.</returns>
	public GameObject CreateElement(int index)
	{
		GameObject newElement = Instantiate(prefabList[index], Vector3.zero, Quaternion.identity, this.transform);
		pooledObjects[index].Add(newElement);
		newElement.SetActive(false);
		return newElement;
	}

	/// <summary>
	/// Gets a GameObject of type T from the pool or creates a new one if all pulled objects are in use.
	/// </summary>
	/// <returns>The selected GameObject. This GameObject is not Active in hiearchy by default. Returns null if there is no prefab with the component T</returns>
	public GameObject GetFreeObject<T>()
	{
		for (int i = 0; i < prefabList.Count; i++)
		{
			if (prefabList[i].GetComponent<T>() != null)
			{
				for (int j = 0; j < pooledObjects[i].Count; j++)
				{
					if (!pooledObjects[i][j].activeInHierarchy)
						return pooledObjects[i][j];
				}

				return CreateElement(i);
			}
		}

		return null;
	}
/*
	private void OnDestroy() 
	{
		Services.instance.poolingSystem = null;
	}
*/
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//more objects in the pool needed? https://www.kodeco.com/847-object-pooling-in-unity
class ObjectPooler : MonoBehaviour {

	public static ObjectPooler sharedInstance;
	public List<GameObject> pooledObjects;
	public List<ObjectPoolItem> itemsToPool;


	//
	Transform pos;
	Transform rot;

	public GameObject getPooledObject(string tag) {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
				return pooledObjects[i];
			}
		}
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					GameObject obj = (GameObject)Instantiate(item.objectToPool);
					obj.SetActive(false);
					pooledObjects.Add(obj);
					return obj;
				}
			}
		}
		return null;
	}

	void Awake() {
		sharedInstance = this;
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive(false);
				pooledObjects.Add(obj);
			}
		}
	}
	
}
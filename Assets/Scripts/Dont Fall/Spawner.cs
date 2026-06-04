using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject spawn;

    public void spawnObject(int num)
    {
        GameObject spawnObject = Instantiate(spawn, this.transform.position, this.transform.rotation);

        int scaleSize = Random.Range(7, 18);
        Vector3 localScale = spawnObject.transform.localScale;
        localScale.x = scaleSize;
        spawnObject.transform.localScale = localScale;

        int displacement = Random.Range(-7, 7);
        Vector3 localTransform = spawnObject.transform.localPosition;
        if (num % 2 == 0)
            localTransform.z += displacement;
        else
            localTransform.x += displacement;
        localTransform.y += 0.5f;
        spawnObject.transform.localPosition = localTransform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject cube;

    private float angle = 0;

    public int rows = 50;
    public int columns = 50;
    public float speed = (float)0.08;

    private List<GameObject> goInt = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                var pos = new Vector3(x - columns / 2, 0, z - rows / 2);
                var go = Instantiate(cube, pos, Quaternion.identity);

                goInt.Add(go);
            }
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var go in goInt)
        {
            var d = new Vector3(go.transform.localPosition.x, 0, go.transform.localPosition.z).magnitude;
            var offset = map(d, 0, rows / 2, -Mathf.PI, Mathf.PI);
            var a = angle + offset;
            var h = Mathf.Floor(map(Mathf.Sin(a), -1, 1, 10, 30));

            go.transform.localScale = new Vector3(1, h/20, 1);

        }

        angle -= speed;
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}

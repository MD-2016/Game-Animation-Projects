using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject preyfab, predator;
    public GameObject maker;
    public List<GameObject> preyobjects;
    public int size;

    // Start is called before the first frame update
    void Start()
    {
        createScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createScene()
    {
        preyobjects = new List<GameObject>();
        size = 10;
        int i = 1;

        while(i <= size)
        {
           
           
            maker = Instantiate(preyfab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity) as GameObject;
            maker.name = "Prey";
            maker.transform.parent = this.gameObject.transform;
            preyobjects.Add(maker);
            i++;
        }

        maker = Instantiate(predator, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        maker.name = "Predator";
        maker.transform.parent = this.gameObject.transform;
    }

}

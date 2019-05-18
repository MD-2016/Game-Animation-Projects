using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class used to instantiate a single cell with the following properties.
public class Cell : MonoBehaviour
{
    //checks if current cell is visited
    public bool cellVisited = false;

    //parts of a cell
    public GameObject west;
    public GameObject east;
    public GameObject north;
    public GameObject south;
    public GameObject floor;

}
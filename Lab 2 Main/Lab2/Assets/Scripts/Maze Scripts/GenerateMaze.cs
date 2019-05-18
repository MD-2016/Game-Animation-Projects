using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{

    //iterates through the array of cells and gets the input from the user in the inspector
    public int rows;
    public int columns;

    //the walls and floor of each cell
    public GameObject cellWall;
    public GameObject cellFloor;


    //size of each cell
    public float cellSize = 2f;

    //array used to hold each cell
    private Cell[,] cells;

    //speed of turn when rotating scene
    public float turn = 45f;


    //build the maze and run the algorithm to make the maze paths 
    private void Start()
    {
        CreatePreMaze();

        MazeAlg maze = new MazeAlg(cells);
        maze.ConstructMaze();

    }

    //rotates the scene
    private void Update()
    {

       if(Input.GetKey(KeyCode.Alpha0))
        {
            transform.Rotate(Vector3.down * turn * Time.deltaTime);
        }
    }

    //Creates the maze which appears as a bunch of cells before the algorithm generates the maze paths 
    private void CreatePreMaze()
    {

        //empty array of cells that need to be filled
        cells = new Cell[rows, columns];

        //loops through each row and column to create the floor and walls 
        //this code creates East and South walls for column and row not being zero and West and North walls otherwise
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {

                //a cell at position row and col index
                cells[row, col] = new Cell();

                //build the cell floor, make it the child of the Maze Manager, give it a name, and rotate it 90 degrees to the right
                cells[row, col].floor = Instantiate(cellFloor, new Vector3(row * cellSize, -(cellSize / 2f), col * cellSize), Quaternion.identity) as GameObject;
                cells[row, col].floor.name = "Floor " + row + "," + col;
                cells[row, col].floor.transform.parent = this.gameObject.transform;
                cells[row, col].floor.transform.Rotate(new Vector3(1, 0, 0), 90f);

                //since row is 0 then create the north wall and make Maze Manager its parent
                if (row == 0)
                {
                    cells[row, col].north = Instantiate(cellWall, new Vector3((row * cellSize) - (cellSize / 2f), 0, col * cellSize), Quaternion.identity) as GameObject;
                    cells[row, col].north.name = "North " + row + "," + col;
                    cells[row, col].north.transform.parent = this.gameObject.transform;
                    cells[row, col].north.transform.Rotate(new Vector3(0, 1, 0) * 90f);


                }

                //zero column means we encounter the west wall, so create the west wall and set its parent to Maze Manager
                if (col == 0)
                {
                    cells[row, col].west = Instantiate(cellWall, new Vector3(row * cellSize, 0, (col * cellSize) - (cellSize / 2f)), Quaternion.identity) as GameObject;
                    cells[row, col].west.name = "West " + row + "," + col;
                    cells[row, col].west.transform.parent = this.gameObject.transform;
                }

                //Build the east wall without checking the column index and make Maze Manager its parent
                cells[row, col].east = Instantiate(cellWall, new Vector3(row * cellSize, 0, (col * cellSize) + (cellSize / 2f)), Quaternion.identity) as GameObject;
                cells[row, col].east.name = "East " + row + "," + col;
                cells[row, col].east.transform.parent = this.gameObject.transform;



                //create the south wall regardless of the index and make Maze Manager its parent
                cells[row, col].south = Instantiate(cellWall, new Vector3((row * cellSize) + (cellSize / 2f), 0, col * cellSize), Quaternion.identity) as GameObject;
                cells[row, col].south.name = "South " + row + "," + col;
                cells[row, col].south.transform.parent = this.gameObject.transform;
                cells[row, col].south.transform.Rotate(new Vector3(0, 1, 0) * 90f);


            }
        }
    }
}
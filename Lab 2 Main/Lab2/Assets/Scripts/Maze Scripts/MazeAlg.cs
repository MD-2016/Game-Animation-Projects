using UnityEngine;
using System.Collections;

public class MazeAlg
{
    //used to iterate through array of cells
    private int rows;
    private int columns;

    //global array of cells of the maze
    private Cell[,] cells;

    //current position in the cell array
    private int currentR = 0;
    private int currentC = 0;

    //cardinal directions to make algorithm easier to follow
    public enum Direction
    {
        North=1,
        South=2,
        East=3,
        West=4
    };

    //checks if the algorithm is completed
    private bool complete = false;

    //constructor for setting the array up and getting the size of each row and column part of the 2d cell array
    public MazeAlg(Cell[,] cellArray)
    {
        cells = cellArray;

        rows = cells.GetLength(0);
        columns = cells.GetLength(1);
    }


    //constructs the maze after all walls and floor are generated
    public void ConstructMaze()
    {


        while (!complete)
        {
            //run the walk phase then search phase
            WalkPhase();
            SearchPhase();
        }
    }

    //walk phase starts off by checking if paths remain and removing walls to form paths
    //It acheives this by doing a random walk from current cell until dead end with already visited cells 
    //mainly creates south and east walls since north and west walls are outer walls
    private void WalkPhase()
    {
        //while paths exist at the current part of the maze, destroy walls based on the generated value
        //go until stuck at a dead end
        while (PathsLeft(currentR, currentC))
        {
            int seed = 5;
            int directionchoice = Random.Range(1, seed);


            if (directionchoice == (int)Direction.North && AvailableCell(currentR - 1, currentC))
            {
                // Found North wall
                RemoveWall(cells[currentR, currentC].north);
                RemoveWall(cells[currentR - 1, currentC].south);
                currentR--;
            }

            else if (directionchoice == (int)Direction.South && AvailableCell(currentR + 1, currentC))
            {
                // Found south wall
                RemoveWall(cells[currentR, currentC].south);
                RemoveWall(cells[currentR + 1, currentC].north);
                currentR++;
            }
            else if (directionchoice == (int)Direction.East && AvailableCell(currentR, currentC + 1))
            {
                // Found east wall
                RemoveWall(cells[currentR, currentC].east);
                RemoveWall(cells[currentR, currentC + 1].west);
                currentC++;
            }
            else if (directionchoice == (int)Direction.West && AvailableCell(currentR, currentC - 1))
            {
                // found west wall
                RemoveWall(cells[currentR, currentC].west);
                RemoveWall(cells[currentR, currentC - 1].east);
                currentC--;
            }

            //cells are visited
            cells[currentR, currentC].cellVisited = true;
        }
    }

    //search for other visited cells at position x,y for having unvisited neighbors
    //remove any neighboring walls
    private void SearchPhase()
    {
        complete = true;


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (!cells[row, col].cellVisited && OtherVisitedCells(row, col))
                {
                    complete = false; 
                    currentR = row;
                    currentC = col;
                    RemoveNeighborWall(currentR, currentC);
                    cells[currentR, currentC].cellVisited = true;
                    return; 
                }
            }
        }
    }

    //finds paths in the maze that were not visited 
    private bool PathsLeft(int r, int c)
    {
        //get the number of paths, and check if any exist based on the current indicies 
        bool pathsExist = false;
        int paths = 0;

        //rows exist and the previous south wall is visited
        if (r > 0 && !cells[r - 1, c].cellVisited)
        {
            paths += 1;
        }

        //rows are less than second to last and the next wall is visited
        if (r < rows - 1 && !cells[r + 1, c].cellVisited)
        {
            paths += 1;
        }

        //columns exist and previous column wall is visited (east)
        if (c > 0 && !cells[r, c - 1].cellVisited)
        {
            paths += 1;
        }

        //columns are second to last and the next column has a visited cell (west)
        if (c < columns - 1 && !cells[r, c + 1].cellVisited)
        {
            paths += 1;
        }

        //do paths exist?
        pathsExist = paths > 0;

        return pathsExist;
    }

    //Do any cells remain
    private bool AvailableCell(int r, int col)
    {
        //checks for available cells
        bool check = r >= 0 && r < rows && col >= 0 && col < columns && !cells[r, col].cellVisited;

        //result returned from the function
        bool result = false;

        //if rows remain and columns remain and there are unvisited cells remaining, then there are cells left
        if (check)
        {
            result = true;
        }

        //every cell has been visited
        else
        {
            result = false;
        }

        return result;
    }

    //remove walls 
    private void RemoveWall(GameObject wall)
    {
        if (wall != null)
        {
            Object.Destroy(wall);
        }
    }

    //Check for other cells not visited 
    private bool OtherVisitedCells(int r, int col)
    {
        bool result = false;

        int visitedCells = 0;

        //check north one row if up 1 or more for adjacent visited cell
        if (r > 0 && cells[r - 1, col].cellVisited)
        {
            visitedCells += 1;
        }

        //check south one row if near last row or not  for adjacent visited cell
        if (r < (rows - 2) && cells[r + 1, col].cellVisited)
        {
            visitedCells += 1;
        }

        //check west if column >= 1 for visited cell
        if (col > 0 && cells[r, col - 1].cellVisited)
        {
            visitedCells += 1;
        }

        //check east if near last column for visited cell
        if (col < (columns - 2) && cells[r, col + 1].cellVisited)
        {
            visitedCells += 1;
        }

        // return true if there are any adjacent cellVisited cells to this one
        result = visitedCells > 0;

        return result;
    }

    //remove adjacent (neighboring) walls
    private void RemoveNeighborWall(int row, int column)
    {
        bool isWallDestroyed = false;

        //while adjacent walls remain to a visited cell, generate a random value to destroy the walls surrounding those visited cells
        while (!isWallDestroyed)
        {
            int seed = 5;
            int directionchoice = Random.Range (1, seed);

            //north direction is chosen, rows remain, and the previous south cell was visited. Remove those walls
            if (directionchoice == (int)Direction.North && row > 0 && cells[row - 1, column].cellVisited)
            {
                RemoveWall(cells[row, column].north);
                RemoveWall(cells[row - 1, column].south);
                isWallDestroyed = true;
            }

            //south direction is chosen, rows remain, and the previous north cell was visited. Remove those walls
            else if (directionchoice == (int)Direction.South && row < (rows - 2) && cells[row + 1, column].cellVisited)
            {
                RemoveWall(cells[row, column].south);
                RemoveWall(cells[row + 1, column].north);
                isWallDestroyed = true;
            }

            //east direction is chosen, columns remain, and the previous west cell was visited. Remove those walls
            else if (directionchoice == (int)Direction.East && column > 0 && cells[row, column - 1].cellVisited)
            {
                RemoveWall(cells[row, column].west);
                RemoveWall(cells[row, column - 1].east);
                isWallDestroyed = true;
            }

            //west direction is chosen, columns remain, and the previous east cell was visited. Remove those walls
            else if (directionchoice == (int)Direction.West && column < (columns - 2) && cells[row, column + 1].cellVisited)
            {
                RemoveWall(cells[row, column].east);
                RemoveWall(cells[row, column + 1].west);
                isWallDestroyed = true;
            }
        }

    }

}

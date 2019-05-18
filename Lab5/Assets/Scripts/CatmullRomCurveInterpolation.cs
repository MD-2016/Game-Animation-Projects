using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatmullRomCurveInterpolation : MonoBehaviour {

	const int NumberOfPoints = 8;
	Vector3[] controlPoints;
    GameObject[] cubes;

    float[] parametrics;
    float[] arcLengths;
    Vector3[] positions;
    const int Samples = 200;
    int pos;
    float distance1;
    float distance2;
    float speed;
	
	const int MinX = -5;
	const int MinY = -5;
	const int MinZ = 0;

	const int MaxX = 5;
	const int MaxY = 5;
	const int MaxZ = 5;
	
	double time = 0;
	const double DT = 0.01;

    public int segNum;

    float uMain = 0;
	
	/* Returns a point on a cubic Catmull-Rom/Blended Parabolas curve
	 * u is a scalar value from 0 to 1
	 * segment_number indicates which 4 points to use for interpolation
	 */
	Vector3 ComputePointOnCatmullRomCurve(double u, int segmentNumber)
	{

        //convert u to float
        uMain = (float)u;

        //tension
        const float t = 0.5f;

        //point on curve
		Vector3 point = new Vector3();

        //coefficents
        Vector3 c0, c1, c2, c3;

        //p values used to get point on curve
        Vector3 pi, pi1, pimin1, pimin2;

        //find the values for p

        //current point
        pi = controlPoints[segNum];


        //check if plus p_i+1 is outside of array bounds, otherwise use the next segment
        if (segNum == NumberOfPoints - 1)
            pi1 = controlPoints[0];
        else
            pi1 = controlPoints[segNum + 1];

        //check if the segment is within bounds of the array, otherwise move back a point
        if (segNum == 0)
            pimin1 = controlPoints[NumberOfPoints - 1];
        else
            pimin1 = controlPoints[segNum - 1];

        //check if the segment is within the bounds of the array, otherwise move back two points
        if (segNum < 2)
            pimin2 = controlPoints[NumberOfPoints - 2];
        else
            pimin2 = controlPoints[segNum - 2];

        //get the coefficent values
        c0 = pimin1;
        c1 = (-t) * pimin2 + (t * pi);
        c2 = (2 * t * pimin2) + (t - 3) * pimin1 + (3 - 2 * t) * pi + (-t * pi1);
        c3 = (-t * pimin2) + (2 - t) * pimin1 + (t - 2) * pi + (t * pi1);



        //calculated point
        point = c3 * Mathf.Pow(uMain, 3f) + c2 * Mathf.Pow(uMain, 2f) + c1 * uMain + c0; 

		
		// TODO - compute and return a point as a Vector3		
		// Hint: Points on segment number 0 start at controlPoints[0] and end at controlPoints[1]
		//		 Points on segment number 1 start at controlPoints[1] and end at controlPoints[2]
		//		 etc...
		
		return point;
	}

    public float easeInSin(float t)
    {
        float s = ((Mathf.Sin((t * Mathf.PI) - (Mathf.PI / 2))) + 1) / 2;

        return s;
    }

    void arcLengthParametricFunc(int segNumber)
    {
        parametrics = new float[Samples];
        arcLengths = new float[Samples];
        positions = new Vector3[Samples];
        Vector3 previousPoint = new Vector3(0, 0, 0);
        Vector3 nextPoint = new Vector3(0, 0, 0);
        for (int j = 0; j < Samples; j++)
        {
            double temp = ((double)j) / (double) Samples;
            previousPoint = nextPoint;
            nextPoint = ComputePointOnCatmullRomCurve(temp, segNumber);
            positions[j] = nextPoint;
            parametrics[j] = (float) temp;
            if (j == 0)
            {
                arcLengths[j] = 0f;
            } else
            {
                arcLengths[j] = arcLengths[j - 1] + Mathf.Sqrt(Mathf.Pow(nextPoint.x - previousPoint.x, 2f) + Mathf.Pow(nextPoint.y - previousPoint.y, 2f) + Mathf.Pow(nextPoint.z - previousPoint.z, 2f));
            }
        }
        float maxLength = arcLengths[arcLengths.Length - 1]; 
        for (int i = 0; i < Samples; i++)
        {
            parametrics[i] = i / Samples * NumberOfPoints;
            arcLengths[i] = arcLengths[i] / maxLength;
        }
    }
	
	void GenerateControlPointGeometry()
	{
        cubes = new GameObject[NumberOfPoints];
		for(int i = 0; i < NumberOfPoints; i++)
		{
			cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubes[i].transform.localScale -= new Vector3(0.8f,0.8f,0.8f);
			cubes[i].transform.position = controlPoints[i];
		}	
	}

    // Use this for initialization
    void Start () {

		controlPoints = new Vector3[NumberOfPoints];
		
		// set points randomly...
		controlPoints[0] = new Vector3(0,0,0);
		for(int i = 1; i < NumberOfPoints; i++)
		{
			controlPoints[i] = new Vector3(Random.Range(MinX,MaxX),Random.Range(MinY,MaxY),Random.Range(MinZ,MaxZ));
		}
		/*...or hard code them for testing
		controlPoints[0] = new Vector3(0,0,0);
		controlPoints[1] = new Vector3(0,0,0);
		controlPoints[2] = new Vector3(0,0,0);
		controlPoints[3] = new Vector3(0,0,0);
		controlPoints[4] = new Vector3(0,0,0);
		controlPoints[5] = new Vector3(0,0,0);
		controlPoints[6] = new Vector3(0,0,0);
		controlPoints[7] = new Vector3(0,0,0);
		*/
		
		GenerateControlPointGeometry();
        arcLengthParametricFunc(0);
        pos = 0;
        distance1 = 0;
        distance2 = 0;
        speed = 0;
	}
	
	// Update is called once per frame
	void Update () {

        pos++;
        if (pos > positions.Length - 1)
        {
            pos = 0;
            distance2 = 0;
            distance1 = 0;
            segNum += 1;
            if (segNum == NumberOfPoints)
            {
                segNum = 0;
            }
            arcLengthParametricFunc(segNum);
        }
        if (segNum == 0)
        {
            distance1 = distance2;
            distance2 = easeInSin((float)pos / positions.Length / 2f) * 2f;
            speed = distance2 - distance1;
        } else if (segNum == 7)
        {
            distance1 = distance2;
            distance2 = easeInSin(((float)pos / positions.Length / 2f) + .5f) * 2f;
        }
        else
        {
            distance1 = distance2;
            distance2 = speed + distance1;
            if(distance2 >= 1f)
            {
                distance2 = .99f;
                pos = positions.Length - 1;
            }
        }
        transform.position = positions[pos];
        bool found = false;
        int temp = 0;
        while (!found && temp < arcLengths.Length)
        {
            if (arcLengths[temp] > distance2)
            {
                transform.position = positions[temp];
                found = true;
            }
            else
                temp++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controlPoints[0] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[0].transform.position = controlPoints[0];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controlPoints[1] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[1].transform.position = controlPoints[1];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controlPoints[2] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[2].transform.position = controlPoints[2];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            controlPoints[3] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[3].transform.position = controlPoints[3];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            controlPoints[4] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[4].transform.position = controlPoints[4];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            controlPoints[5] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[5].transform.position = controlPoints[5];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            controlPoints[6] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[6].transform.position = controlPoints[6];
            arcLengthParametricFunc(segNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            controlPoints[7] = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
            cubes[7].transform.position = controlPoints[7];
            arcLengthParametricFunc(segNum);
        }
    }
}

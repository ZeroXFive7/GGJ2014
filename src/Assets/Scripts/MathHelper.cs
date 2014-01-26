using UnityEngine;
using System.Collections;

static public class MathHelper : object
{
    static public Vector3 ProjectVectorToPlane(Vector3 vec, Vector3 planeNormal)
    {
        return vec - Vector3.Dot(vec, planeNormal) * planeNormal;
    }

    static public Vector3 ProjectVectorToVector(Vector3 source, Vector3 destination)
    {
        return Vector3.Dot(source, destination) * destination;
    }
}

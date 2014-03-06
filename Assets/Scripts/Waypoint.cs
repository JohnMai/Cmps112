using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
    public string nameOfImage;
    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, nameOfImage, true);
    }
}

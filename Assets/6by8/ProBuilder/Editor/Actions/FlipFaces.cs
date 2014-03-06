using UnityEngine;
using UnityEditor;
using System.Collections;

public class FlipFaces : Editor {

	// [MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Geometry/Flip Object Normals")]
	// public static void FlipObjectNormals()
	// {
	// 	foreach(pb_Object pb in pbUtil.GetComponents<pb_Object>(Selection.transforms))
	// 		pb.ReverseWindingOrder();
	// }

	[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Geometry/Flip Face Normals &n", false, 100+5)]
	public static void FlipFaceNormals()
	{
		foreach(pb_Object pb in pbUtil.GetComponents<pb_Object>(Selection.transforms))
			pb.ReverseWindingOrder(pb.selected_faces);
	}	
}

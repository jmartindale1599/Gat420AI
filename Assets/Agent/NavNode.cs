using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour{

	[SerializeField] public List<NavNode> neighbors;

	[SerializeField, Range(1, 10)] public float radius = 1;

	public static NavNode[] GetNodes(){

		return FindObjectsOfType<NavNode>();

	}

	public static NavNode GetRandomNode(){

		var nodes = GetNodes();

		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];

	}

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavNode : MonoBehaviour{

	[SerializeField] public List<NavNode> neighbors;

	[SerializeField, Range(1, 10)] public float radius = 1;

	public NavNode parent { get; set; } = null;
	public bool visited { get; set; } = false;
	public float cost { get; set; } = float.MaxValue;

	public static NavNode[] GetNodes(){

		return FindObjectsOfType<NavNode>();

	}

	public static NavNode GetRandomNode(){

		var nodes = GetNodes();

		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];

	}

	public static void ShowNodes(){

		var nodes = GetNodes();
		
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = true);

	}

	public static void HideNodes(){

		var nodes = GetNodes();
		
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = false);
	
	}

	public static void ResetNodes(){

		var nodes = GetNodes();
		
		nodes.ToList().ForEach(node =>{

			node.visited = false;
			
			node.parent = null;
			
			node.cost = float.MaxValue;
		
		});

	}

}

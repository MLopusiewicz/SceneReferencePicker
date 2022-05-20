using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonoBehaviourBase {
	GameObject gameObject { get; }
	Transform transform { get; }  
}

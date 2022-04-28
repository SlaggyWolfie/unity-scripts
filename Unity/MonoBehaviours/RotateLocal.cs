using UnityEngine;

public class RotateLocal : MonoBehaviour {

#pragma warning disable 0649
    [SerializeField] private Vector3 _axis;
    [SerializeField] private int _speed;
#pragma warning restore 0649

    void Start() {
        _axis = _axis.normalized;
    }

	// Update is called once per frame
	void Update () {
        this.transform.Rotate(_axis, _speed * Time.deltaTime);	
	}
}

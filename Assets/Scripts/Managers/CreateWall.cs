using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;


public class CreateWall : MonoBehaviour
{
    [Header("Wall Creation")]
    [SerializeField] private Transform _wall;
    [SerializeField, Range(1, 10)] private int _rowCount = 5;
    [SerializeField, Range(1, 10)] private int _columnCount = 5;
    [SerializeField] private string _targetTag = "Shootable";
    [SerializeField] private Vector3 _wallOffset = Vector3.zero;
    [Header("Wall Panting")]
    [SerializeField] private Material exampleMat;

    [HideInInspector]
    public int WallSize = 1;

    private Color _randomColor = new Color();
    private Functions _functions;

    private void OnValidate()
    {
        WallSize = _rowCount * _columnCount;
        if (_functions == null) _functions = FindObjectOfType<Functions>();

        WallUpdate();

        _wall.position = new Vector3(-(_columnCount / 2), -(_rowCount / 2), 0) + _wallOffset;
    }

    //--------------------

    private void WallUpdate()
    {
        WallReset();
        if (_wall == null) return;
        for (int rowNo = 0; rowNo < _rowCount; rowNo++)
        {
            for (int columnNo = 0; columnNo < _columnCount; columnNo++)
            {
                GameObject brick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brick.transform.SetParent(_wall);
                brick.transform.localPosition = Vector3.right * columnNo + Vector3.up * rowNo;
                brick.tag = _targetTag;
                Material instantiatedMat = Material.Instantiate(exampleMat);
                instantiatedMat.color = _randomColor.Randomize();

                brick.GetComponent<MeshRenderer>().sharedMaterial = instantiatedMat;
            }
        }
    }
    private void WallReset() => _wall.Clear(_functions.DestroyGO);

}

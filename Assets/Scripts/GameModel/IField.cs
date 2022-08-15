using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public interface IField
    {
        FieldVector3 WorldPositionToField(Vector3 position);
        Vector3 FieldPositionToWorld(FieldVector3 fieldPosition);
        T[] GetComponentsInChildren<T>();
    }
}
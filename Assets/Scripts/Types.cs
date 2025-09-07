using System;
using UnityEngine;

namespace IdleArcade
{
    public enum ResourceType
    {
        Wood = 0
    }

    public enum ActorType
    {
        Worker = 0,
        Resource = 1,
        Storage = 2,
        Material = 3,
        Inventory = 4,
    }

    [Serializable]
    public struct Material
    {
        public ResourceType Type => _type;
        public int Count => _count;

        private ResourceType _type;
        private int _count;

        public Material(ResourceType type, int count)
        {
            _type = type;
            _count = count;
        }
    }

    [Serializable]
    public struct InventorySize
    {
        [SerializeField]
        private int _row;
        [SerializeField]
        private int _column;

        [SerializeField]
        private float _rowOffset;
        [SerializeField]
        private float _columnOffset;
        [SerializeField]
        private float _heightOffset;

        public int Row => _row;
        public int Column => _column;

        public float RowOffset => _rowOffset;
        public float ColumnOffset => _columnOffset;
        public float HeightOffset => _heightOffset;

        public Vector3 GetPoint(int currentIndex)
        {
            float maxRowWidth = (Row - 1) * RowOffset;
            float maxColumnWidth = (Column - 1) * ColumnOffset;
            int columnIndex = currentIndex % Column;
            int rowIndex = currentIndex / Column % Row;
            int heightIndex = currentIndex / (Row * Column);
            Vector3 up = Vector3.up * (HeightOffset * heightIndex);
            Vector3 right = Vector3.right * (columnIndex * ColumnOffset - maxColumnWidth / 2f);
            Vector3 forward = Vector3.forward * (rowIndex * RowOffset - maxRowWidth / 2f);
            Vector3 targetPos = up + right + forward;
            return targetPos;
        }
    }
}

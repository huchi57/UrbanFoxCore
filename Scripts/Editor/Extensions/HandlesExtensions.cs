using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

namespace UrbanFox.Editor
{
    public static partial class HandlesExtensions
    {
        public struct MatrixScope : IDisposable
        {
            private readonly Matrix4x4 _preScopeMatrix;

            public MatrixScope(Matrix4x4 newMatrix)
            {
                _preScopeMatrix = Handles.matrix;
                Handles.matrix = newMatrix;
            }

            public void Dispose()
            {
                Handles.matrix = _preScopeMatrix;
            }
        }

        public struct ColorScope : IDisposable
        {
            private readonly Color _preScopeColor;

            public ColorScope(Color newColor)
            {
                _preScopeColor = Handles.color;
                Handles.color = newColor;
            }

            public void Dispose()
            {
                Handles.color = _preScopeColor;
            }
        }

        public struct ZTestScope : IDisposable
        {
            private readonly CompareFunction _preScopeZTestValue;

            public ZTestScope(CompareFunction newZTestValue)
            {
                _preScopeZTestValue = Handles.zTest;
                Handles.zTest = newZTestValue;
            }

            public void Dispose()
            {
                Handles.zTest = _preScopeZTestValue;
            }
        }
    }
}

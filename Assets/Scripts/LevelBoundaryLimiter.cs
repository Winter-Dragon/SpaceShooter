using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ �������. �������� � ������ �� �������� LevelBoundary, ���� ����� ������� �� �����.
    /// �������� �� ������, ������� ���������� ����������.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {

        #region Unity Events

        private void FixedUpdate()
        {
            // ��������, ���� �� �� ����� ������� ������.
            if (LevelBoundary.Instance == null) return;

            // ��������� ��������� ���������� ������� ������� � � �������.
            var levelBoundary = LevelBoundary.Instance;
            var radius = levelBoundary.Radius;

            // ��������, ����� ������ ������� �� �������.
            if (transform.position.magnitude > radius)
            {
                switch (levelBoundary.LimitMode)
                {
                    // �����������: ������ ������� �� ������� �����.
                    case LevelBoundary.Mode.Limit:

                        transform.position = transform.position.normalized * radius;
                        break;

                    // ������������: ������ ��������������� �� ��������������� �������.
                    case LevelBoundary.Mode.Teleport:

                        transform.position = -transform.position.normalized * radius;
                        break;
                }
            }
        }

        #endregion

    }
}
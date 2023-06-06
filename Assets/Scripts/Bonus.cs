using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �������� ����� �������.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Bonus : MonoBehaviour
    {

        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ���� ���������� SpaceShip - ���������� ��� � ���������� ship.
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            // �������� �� null � ������.
            if (ship == null || Player.Instance.ActiveShip == false) return;

            OnPickedUp(ship);

            // ���������� �����.
            Destroy(gameObject);
        }

        #endregion


        #region Protected API

        /// <summary>
        /// �����, ������������� ����� ������� ��������� �����.
        /// </summary>
        /// <param name="ship">�������, ����������� �����.</param>
        protected abstract void OnPickedUp(SpaceShip ship);

        #endregion

    }
}
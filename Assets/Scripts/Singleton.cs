using UnityEngine;

/// <summary>
/// �����, ������������� ������� ������������ ��������� �� ����� � ��������������� ���������� ����� ������� � �������.
/// </summary>
/// <typeparam name="T">�������� ������.</typeparam>
[DisallowMultipleComponent]
public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// ����� �� ������ ���� ��������� ��� ������������ �����.
    /// </summary>
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    /// <summary>
    /// ������ �� �������� ������� ������ T.
    /// </summary>
    public static T Instance { get; private set; }

    #endregion


    #region Unity Events

    protected virtual void Awake()
    {
        // ���� ����� ������ ��� ���� �� �����, ���������� ��������� ������.
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        // ������������ ������ � ���� T.
        Instance = this as T;

        // ���� ������ �� ������ ������������ ��� ������������ �����, ��������� ��� �������.
        if (m_DoNotDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    #endregion

}

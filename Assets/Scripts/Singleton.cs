using UnityEngine;

/// <summary>
/// Класс, гарантирующий объекту единственный экземпляр на сцене и предоставляющий глобальную точку доступа к объекту.
/// </summary>
/// <typeparam name="T">Название класса.</typeparam>
[DisallowMultipleComponent]
public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// Может ли объект быть уничтожен при перезагрузке сцены.
    /// </summary>
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    /// <summary>
    /// Ссылка на свойство объекта класса T.
    /// </summary>
    public static T Instance { get; private set; }

    #endregion


    #region Unity Events

    protected virtual void Awake()
    {
        // Если такой объект уже есть на сцене, уничтожает созданный объект.
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        // Приравнивает объект к типу T.
        Instance = this as T;

        // Если объект не должен уничтожаться при перезагрузке сцены, назначает его таковым.
        if (m_DoNotDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    #endregion

}

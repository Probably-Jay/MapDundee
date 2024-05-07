using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.SceneManagement;
using ExtraUtility;

public static class MonoExtentions
{
    /// <summary>
    /// Calls <see cref="GameObject.GetComponent{T}"/> and assigns the result to <paramref name="componentVariable"/>. Debug logs if component cannot be found.
    /// </summary>
    /// <param name="componentVariable">The variable to hold the component</param>
    /// <typeparam name="T">The type of the component to be found</typeparam>
    //// <exception cref="NullReferenceException">Throws if component cannot be found.</exception>
    public static MonoBehaviour GetComponentAndAssignTo<T>(this MonoBehaviour gameObject, out T componentVariable) where T : Component
    {
        componentVariable = gameObject.GetComponent<T>();

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }
        
    
    public static GameObject GetComponentAndAssignTo<T>(this GameObject gameObject, out T componentVariable) where T : Component
    {
        componentVariable = gameObject.GetComponent<T>();

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }

   

    /// <summary>
    /// Calls <see cref="GameObject.GetComponentInChildren{T}()"/> and assigns the result to <paramref name="componentVariable"/>. Debug logs if component cannot be found.
    /// </summary>
    /// <param name="componentVariable">The variable to hold the component</param>
    /// <typeparam name="T">The type of the component to be found</typeparam>
    //// <exception cref="NullReferenceException">Throws if component cannot be found.</exception>
    public static MonoBehaviour GetComponentInChildrenAndAssignTo<T>(this MonoBehaviour gameObject, out T componentVariable, bool includeInactive = false) where T : Component
    {     
        componentVariable = gameObject.GetComponentInChildren<T>(includeInactive);

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }
        
    
    public static GameObject GetComponentInChildrenAndAssignTo<T>(this GameObject gameObject, out T componentVariable, bool includeInactive = false) where T : Component
    {     
        componentVariable = gameObject.GetComponentInChildren<T>(includeInactive);

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }

    /// <summary>
    /// Calls <see cref="GameObject.GetComponentInParent{T}()"/> and assigns the result to <paramref name="componentVariable"/>. Debug logs if component cannot be found.
    /// </summary>
    /// <param name="componentVariable">The variable to hold the component</param>
    /// <typeparam name="T">The type of the component to be found</typeparam>
    //// <exception cref="NullReferenceException">Throws if component cannot be found.</exception>
    public static MonoBehaviour GetComponentInParentAndAssignTo<T>(this MonoBehaviour gameObject, out T componentVariable, bool includeInactive = false) where T : Component
    {     
        componentVariable = gameObject.GetComponentInParent<T>(includeInactive);

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }    
    
    public static GameObject GetComponentInParentAndAssignTo<T>(this GameObject gameObject, out T componentVariable, bool includeInactive = false) where T : Component
    {     
        componentVariable = gameObject.GetComponentInParent<T>(includeInactive);

        gameObject.AssertFieldNotNull(componentVariable);
        return gameObject;
    }

    /// <summary>
    /// Debug logs if component is null.
    /// </summary>
    [System.Obsolete("Please use " + nameof(AssertFieldNotNull) +" instead.", error: false)]
    public static MonoBehaviour AssertConponentNotNull<T>(this MonoBehaviour gameObject, T componentVariable) where T : Component 
        => gameObject.AssertFieldNotNull(componentVariable);

    /// <summary>
    /// Debug logs if field is null.
    /// </summary>
    public static MonoBehaviour AssertFieldNotNull<T>(this MonoBehaviour gameObject, T variable, Type type = null) where T : UnityEngine.Object
    {
        if (variable != null && !variable.Equals(null))
            return gameObject;

        type ??= typeof(T);
        Debug.LogError($"Field {type.BaseType} \"{type.Name}\" is null in {gameObject.name} ({gameObject.gameObject.GetFullyQualifiedSceneName()})", gameObject);
        return gameObject;
    }   
    
    public static GameObject AssertFieldNotNull<T>(this GameObject gameObject, T variable, Type type = null) where T : UnityEngine.Object
    {
        if (variable != null && !variable.Equals(null))
            return gameObject;

        type ??= typeof(T);
        Debug.LogError($"Field {type.BaseType} \"{type.Name}\" is null in {gameObject.name} ({gameObject.gameObject.GetFullyQualifiedSceneName()})", gameObject);
        return gameObject;
    }

    /// <summary>
    /// Debug logs collection is null or empty
    /// </summary>
    public static MonoBehaviour AssertCollectionNotEmpty<T>(this MonoBehaviour gameObject, IEnumerable<T> collection, Type type = null) 
    {
        if (!collection.IsNullOrEmpty())
            return gameObject;

        type ??= typeof(T);
        Debug.LogError($"Collection {type.BaseType} \"{type.Name}\" is null or empty in {gameObject.name} ({gameObject.gameObject.GetFullyQualifiedSceneName()})", gameObject);
        return gameObject;
    }

    /// <summary>
    /// Debug logs collection is null or empty
    /// </summary>
    public static IEnumerable<T> AssertCollectionNotEmpty<T>(this IEnumerable<T> collection, MonoBehaviour gameObject, Type type = null)
    {
        gameObject.AssertCollectionNotEmpty(collection, type);
        return collection;
    }



    public static string GetFullyQualifiedSceneName(this GameObject gameObject)
    {
        var names = new List<string>
        {
            gameObject.name
        };

        var parent = gameObject.transform.parent;

        while (parent != null)
        {
            names.Add(parent.gameObject.name);
            parent = parent.parent;
        }

        names.Reverse();

        var name = new StringBuilder();

        name.Append(SceneManager.GetActiveScene().name);
        names.ForEach(n => name.Append($".{n}"));

        return name.ToString();
    }
}

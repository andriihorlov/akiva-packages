using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ABCDExtensions
{
    public enum ComponentSearchScope
    {
        Object,
        Parent,
        Child,
        None
    }

    public enum ObjectSearchScope
    {
        All,
        ActiveOnly,
        None
    }

    public static class MonoBehaviourExtensions
    {
        public static void NullComponentGuard<T>(this MonoBehaviour behaviour, ref T component,
            ComponentSearchScope searchScope = ComponentSearchScope.Object,
            bool disableOnFalse = false) where T : MonoBehaviour
        {
            if (component != null) return;

            switch (searchScope)
            {
                case ComponentSearchScope.Object:
                    component = behaviour.GetComponent<T>();
                    break;
                case ComponentSearchScope.Parent:
                    component = behaviour.GetComponentInParent<T>();
                    break;
                case ComponentSearchScope.Child:
                    component = behaviour.GetComponentInChildren<T>();
                    break;
                case ComponentSearchScope.None:
                    break;
            }

            // TODO: think about behaviour in case of missed component - maybe the error is more than enough
            if (component == null)
            {
                Debug.LogError(
                    $"[{nameof(NullComponentGuard)}] Null component of type {typeof(T).Name} on {behaviour.GetType().Name} behaviour. Disabling behavior...");
                behaviour.gameObject.SetActive(!disableOnFalse);
            }
        }

        public static void NullObjectGuard<T>(this MonoBehaviour behaviour, T obj, bool disableOnFalse = true)
            where T : Object
        {
            if (obj == null)
            {
                Debug.LogError(
                    $"[{nameof(NullObjectGuard)}] {typeof(T).Name} is null on {behaviour.GetType().Name} behaviour. ",
                    behaviour.gameObject);
                behaviour.gameObject.SetActive(!disableOnFalse);
            }
        }

        public static void NullTransformGuard(this MonoBehaviour behaviour, ref Transform transform,
            bool disableOnFalse)
        {
            if (transform == null)
            {
                Debug.LogError(
                    $"[{nameof(NullTransformGuard)}] Linked transform is null on {behaviour.GetType().Name} behaviour. Disabling behavior...");
                behaviour.gameObject.SetActive(!disableOnFalse);
            }
        }
    }

    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
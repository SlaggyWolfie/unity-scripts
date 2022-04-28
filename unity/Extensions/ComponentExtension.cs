using UnityEngine;

namespace Slaggy.Unity.Extensions
{
    public static class ComponentExtension
    {
        public static void MultiType_SetEnabled(this Component component, bool value = true)
        {
            if (component == null)
            {
                Debug.LogError("This component is null!");
                return;
            }

            switch (component)
            {
                case Behaviour behaviour: behaviour.enabled = value; break;
                case Collider collider: collider.enabled = value; break;
                case Renderer renderer: renderer.enabled = value; break;
                default:
                    Debug.LogError("Unrecognized Component sub-type: Failed to enable "
                                   + component.GetType().Name);
                    break;
            }
        }

        public static bool MultiType_IsEnabled(this Component component)
        {
            if (component == null)
            {
                Debug.LogError("This component is null!");
                return false;
            }

            bool value = false;

            switch (component)
            {
                case Behaviour behaviour: value = behaviour.enabled; break;
                case Collider collider: value = collider.enabled; break;
                case Renderer renderer: value = renderer.enabled; break;
                default:
                    Debug.LogWarning("Unrecognized Component sub-type: Failed to find if "
                                     + component.GetType().Name + " is enabled.");
                    break;
            }

            return value;
        }
    }
}
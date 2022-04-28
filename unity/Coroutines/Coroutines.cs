using System;
using System.Collections;
using UnityEngine;

namespace Slaggy.Unity.CoroutineUtility
{
    public static partial class Coroutines
    {
        public static IEnumerator WaitOneFrame(Action action)
        {
            yield return null;
            action();
        }

        public static IEnumerator WaitXFrames(int frames, Action action)
        {
            for (int i = frames; i > 0; i--)
                yield return null;
            action();
        }

        public static IEnumerator WaitForEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }

        public static IEnumerator Wait(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }

        public static IEnumerator Wait<T>(float seconds, Action<T> action, T parameter)
            where T : class
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke(parameter);
        }

        public static IEnumerator DoWhile(Func<bool> pred, Action @while, Action after = null, bool updateEveryFrame = true)
        {
            while (pred != null && pred())
            {
                if (updateEveryFrame) yield return null;
                @while?.Invoke();
            }

            after?.Invoke();
        }

        public static IEnumerator DoUntil(Func<bool> pred, Action until, Action after = null, bool updateEveryFrame = true)
        {
            while (pred != null && !pred())
            {
                if (updateEveryFrame) yield return null;
                until?.Invoke();
            }

            after?.Invoke();
        }

        public static IEnumerator WaitUntil(Func<bool> predicate, Action action)
        {
            yield return new WaitUntil(predicate);
            action();
        }

        public static IEnumerator WaitWhile(Func<bool> predicate, Action action)
        {
            yield return new WaitWhile(predicate);
            action();
        }
    }
}
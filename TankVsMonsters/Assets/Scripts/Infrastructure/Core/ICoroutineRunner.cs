using System.Collections;
using UnityEngine;

namespace Infrastructure.Core
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
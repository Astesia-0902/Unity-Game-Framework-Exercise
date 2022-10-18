using System;
using UnityEngine;

namespace Tutorial
{
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
            InitCustomDebuggers();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

namespace Bomberman_client.GameClasses
{
    class ScriptEngine
    {
        public delegate void OnEndFunc(object sender);
        public delegate void OnChangeFunc();
        public void StartSript(PhysicalObject obj, Image sprite, OnEndFunc onEndFunc, int delay, int countStates)
        {

            ScriptState states = new ScriptState(obj, sprite, onEndFunc, countStates, delay);            
        }
        public void StartExplosion()
        { }
    }
}

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
        public void StartSimpleScript(PhysicalObject obj, Image sprite, OnEndFunc onEndFunc, int delay, int countStates)
        {
            SimpleScript states = new SimpleScript(obj, sprite, onEndFunc, countStates, delay);
            states.currTimer.Enabled = true;
        }
        public void StartExplosion(Explosion explosion, OnEndFunc onEndFunc, int delay, int countStates)
        {
            List<SimpleScript> statesExplosion = new List<SimpleScript>();
            for (int i = 0; i < explosion.partsExplosionBottom.Count; i++)
            {
                statesExplosion.Add(new SimpleScript(explosion.partsExplosionBottom[i], onEndFunc, countStates, delay));
            }
            for (int i = 0; i < explosion.partsExplosionUp.Count; i++)
            {
                statesExplosion.Add(new SimpleScript(explosion.partsExplosionUp[i], onEndFunc, countStates, delay));
            }
            for (int i = 0; i < explosion.partsExplosionLeft.Count; i++)
            {
                statesExplosion.Add(new SimpleScript(explosion.partsExplosionLeft[i], onEndFunc, countStates, delay));
            }
            for (int i = 0; i < explosion.partsExplosionRight.Count; i++)
            {
                statesExplosion.Add(new SimpleScript(explosion.partsExplosionRight[i], onEndFunc, countStates, delay));
            }
            statesExplosion.Add(new SimpleScript(explosion.partExplosionCenter, onEndFunc, countStates, delay));

            for (int i = 0; i < statesExplosion.Count; i++)
            {
                statesExplosion[i].currTimer.Enabled = true;
            }
        }
    }
}

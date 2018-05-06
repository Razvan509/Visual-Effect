using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisualEffect
{
    class Handler
    {
        public List<GameObject> objects;
        public List<Bar> bars;

        public Handler()
        {
            objects = new List<GameObject>();
            bars = new List<Bar>();
        }

        public void add(GameObject o)
        {
            objects.Add(o);
        }

        public void addBar(Bar b)
        {
            bars.Add(b);
        }

        public void delete(GameObject o)
        {
            objects.Remove(o);
        }

        public void deleteBar(Bar b)
        {
            bars.Remove(b);
        }

        public void deleteAll()
        {
            objects.Clear();
        }

        public void raiseUp(int x)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].VelY += x;
            }
        }

        public void tick()
        {            
            for (int i = 0; i < objects.Count; ++i) objects[i].tick();
            for (int i = 0; i < bars.Count; ++i) bars[i].tick();
        }

        public void render(Graphics g)
        {
            for (int i = 0; i < objects.Count; ++i) objects[i].render(g);
        }

    }
}

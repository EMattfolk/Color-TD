﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class Dot
    {
        private static Bitmap image = new Bitmap("..\\..\\Black_dot.png"); //TODO: move to subclasses or make into an array with identifiers

        private int speed, size;
        private float distance;
        private PointF position;

        public Dot (int speed, int size, float distance)
        {
            this.speed = speed;
            this.size = size;
            this.distance = distance;
            position = new PointF();
        }

        public float UpdateDistance (float deltaTime)
        {
            distance += deltaTime * speed;
            return distance;
        }

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Size
        {
            get { return size; }
        }

        public static Bitmap Image
        {
            get { return image; }
        }
    }
}

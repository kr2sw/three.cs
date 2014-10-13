﻿namespace ThreeCs.Lights
{
    using System.Drawing;

    using ThreeCs.Core;

    
    public class Light : Object3D
    {
        #region Fields

        
        public Color color;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public Light(Color color)
        {
            this.color = color;
        }

        /// <summary>
        ///     Copy Constructor
        /// </summary>
        protected Light(Light other)
            : base(other)
        {
            this.color = other.color;
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Light(this);
        }

        #endregion
    }
}
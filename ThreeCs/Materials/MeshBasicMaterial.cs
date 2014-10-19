﻿namespace ThreeCs.Materials
{
    using System.Collections;
    using System.Drawing;

    using ThreeCs.Textures;

    public class MeshBasicMaterial : Material, IWireframe, IMap
    {

        // IMap

        public Texture Map { get; set; }

        public Texture AlphaMap { get; set; }

        public Texture SpecularMap { get; set; }

        public Texture NormalMap { get; set; } // TODO: not in ThreeJs, just to be an IMap. Must be NULL

        public Texture BumpMap { get; set; } // TODO: not in ThreeJs, just to be an IMap.  Must be NULL



        public Texture LightMap;



        public Texture EnvMap;

        public Color Color;

        public int Combine;

        public float Reflectivity;

        public float RefractionRatio;

        public bool Fog;

        public int Shading;

        // IWireFrameable

        public bool Wireframe { get; set; }

        public float WireframeLinewidth { get; set; }

        //

        public string WireframeLinecap;

        public string WireframeLinejoin;

        public Color[] VertexColors;

        public bool Skinning;

        public bool MorphTargets;

        public int NumSupportedMorphTargets;

        /// <summary>
        /// 
        /// </summary>
        public MeshBasicMaterial(Hashtable parameters = null)
        {
	        this.Color = Color.White; // emissive

	        this.Map = null;

	        this.LightMap = null;

	        this.SpecularMap = null;

	        this.AlphaMap = null;

	        this.EnvMap = null;
	        this.Combine = Three.MultiplyOperation;
	        this.Reflectivity = 1;
	        this.RefractionRatio = 0.98f;

	        this.Fog = true;

            this.Shading = Three.SmoothShading;

            // IWireFrameable
            this.Wireframe = false;
            this.WireframeLinewidth = 1;
            
            this.WireframeLinecap = "round";
	        this.WireframeLinejoin = "round";

	        this.Skinning = false;
	        this.MorphTargets = false;

            this.SetValues(parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        protected MeshBasicMaterial(MeshBasicMaterial other) : base(other)
        {
            
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new MeshBasicMaterial(this);
        }
    }
}

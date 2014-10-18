﻿namespace Demo.WebGL
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using Examples;

    using ThreeCs.Cameras;
    using ThreeCs.Core;
    using ThreeCs.Extras.Geometries;
    using ThreeCs.Materials;
    using ThreeCs.Math;
    using ThreeCs.Objects;
    using ThreeCs.Scenes;

    [Example("webgl_materials_wireframe", ExampleCategory.OpenTK, "materials", 0.4f)]
    class webgl_materials_wireframe : Example
    {
        private PerspectiveCamera camera;

        private Scene scene;

        private Mesh meshLines;

        private Mesh meshTris;

        private Mesh meshMixed;

        private string vertex_shader = @"			
            attribute vec3 center;
			varying vec3 vCenter;

			void main() {

				vCenter = center;
				gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );

			}";

        private string fragment_shader = @"
			#extension GL_OES_standard_derivatives : enable

			varying vec3 vCenter;

			float edgeFactorTri() {

				vec3 d = fwidth( vCenter.xyz );
				vec3 a3 = smoothstep( vec3( 0.0 ), d * 1.5, vCenter.xyz );
				return min( min( a3.x, a3.y ), a3.z );

			}

			void main() {

				gl_FragColor.rgb = mix( vec3( 1.0 ), vec3( 0.2 ), edgeFactorTri() );
				gl_FragColor.a = 1.0;
			}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public override void Load(Control control)
        {
            base.Load(control);

            camera = new PerspectiveCamera(40, control.Width / (float)control.Height, 1, 2000);
            this.camera.Position.Z = 800;

            scene = new Scene();
 
        	const int size = 150;

			var geometryLines = new BoxGeometry( size, size, size );
			var geometryTris  = new BoxGeometry( size, size, size );

			// wireframe using gl.LINES

			var materialLines = new MeshBasicMaterial() { wireframe = true };

			meshLines = new Mesh( geometryLines, materialLines );
			meshLines.Position.X = -150;
			scene.Add( meshLines );

			// wireframe using gl.TRIANGLES (interpreted as triangles)

            var attributesTris = new Hashtable();
            attributesTris.Add("center", new Hashtable { { "type", "v3" }, { "boundTo", "faceVertices" }, { "value", new List<List<Vector3>>() } });
            var valuesTris = (List<List<Vector3>>)((Hashtable)attributesTris["center"])["value"];

			SetupAttributes( geometryTris, valuesTris );

            var materialTris = new ShaderMaterial() { attributes = attributesTris, vertexShader = vertex_shader, fragmentShader= fragment_shader };
            
            meshTris = new Mesh( geometryTris, materialTris );
            meshTris.Position.X = 150;
            scene.Add( meshTris );

            // wireframe using gl.TRIANGLES (mixed triangles and quads)

            var mixedGeometry = new SphereGeometry( size / 2.0f, 32, 16 );

            var attributesMixed = new Hashtable();
            attributesMixed.Add("center", new Hashtable { { "type", "v3" }, { "boundTo", "faceVertices" }, { "value", new List<List<Vector3>>() } });
            var valuesMixed = (List<List<Vector3>>)((Hashtable)attributesMixed["center"])["value"];

            SetupAttributes( mixedGeometry, valuesMixed );

            var materialMixed = new ShaderMaterial() { attributes = attributesMixed, vertexShader = vertex_shader, fragmentShader = fragment_shader };

            meshMixed = new Mesh( mixedGeometry, materialMixed );
            meshMixed.Position.X = -150;
   //         scene.Add( meshMixed );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="values"></param>
        private static void SetupAttributes(Geometry geometry, List<List<Vector3>> values)
        {
            for( var f = 0; f < geometry.Faces.Count; f ++ )
            {
                var vs = new List<Vector3>();
                vs.Add(new Vector3(1, 0, 0));
                vs.Add(new Vector3(0, 1, 0));
                vs.Add(new Vector3(0, 0, 1));

                values.Add(vs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientSize"></param>
        public override void Resize(Size clientSize)
        {
            Debug.Assert(null != this.camera);
            Debug.Assert(null != this.renderer);

            this.camera.Aspect = clientSize.Width / (float)clientSize.Height;
            this.camera.UpdateProjectionMatrix();

            this.renderer.size = clientSize;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Render()
        {
            if (null != meshLines)
            {
                meshLines.Rotation.X += 0.005f;
                meshLines.Rotation.Y += 0.01f;
            }

            if (null != meshTris)
            {
                meshTris.Rotation.X += 0.005f;
                meshTris.Rotation.Y += 0.01f;
            }

            if (null != meshMixed ) {

				meshMixed.Rotation.X += 0.005f;
				meshMixed.Rotation.Y += 0.01f;
			}

            renderer.Render(scene, camera);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Unload()
        {
            this.scene.Dispose();
            this.camera.Dispose();

            base.Unload();
        }
    }
}
